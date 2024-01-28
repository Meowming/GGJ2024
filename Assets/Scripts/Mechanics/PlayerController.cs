using System;
using System.Collections;
using System.Collections.Generic;
using Config;
using Config.SkillActions;
using Const;
using UnityEngine;
using Platformer.Gameplay;
using static Platformer.Core.Simulation;
using Platformer.Model;
using Platformer.Core;
using Random = UnityEngine.Random;

namespace Platformer.Mechanics
{
    /// <summary>
    /// This is the main class used to implement control of the player.
    /// It is a superset of the AnimationController class, but is inlined to allow for any kind of customisation.
    /// </summary>
    public class PlayerController : KinematicObject
    {
        public AudioClip jumpAudio;
        public AudioClip respawnAudio;
        public AudioClip ouchAudio;

        /// <summary>
        /// Max horizontal speed of the player.
        /// </summary>
        [NonSerialized]
        public float maxSpeed = 7;
        /// <summary>
        /// Initial jump velocity at the start of a jump.
        /// </summary>
        [NonSerialized]
        public float jumpTakeOffSpeed = 7;

        public JumpState jumpState = JumpState.Grounded;
        private bool stopJump;
        /*internal new*/ public Collider2D collider2d;
        /*internal new*/ public AudioSource audioSource;
        public Health health;
        public bool controlEnabled = true;

        bool jump;
        Vector2 move;
        SpriteRenderer spriteRenderer;
        private bool defaultFlipX;
        internal Animator animator;
        readonly PlatformerModel model = Simulation.GetModel<PlatformerModel>();

        public Bounds Bounds => collider2d.bounds;
        public Rigidbody2D Rigidbody2D => body;

        public AnimalForm[] animalForms;
        public float formChangeTime = 5f;
        public GameObject formTransformEffectPrefab;
        
        private int currentFormIndex = -1;
        private bool isLockForm;
        private bool willChangeForm;
        private GameObject visual;
        private Coroutine formLoopCoroutine;
        private WaitForSeconds formChangeWait;

        // private SkillState skillState;
        private readonly Dictionary<int, SkillContext> runningSkillContexts = new();
        
        private static readonly int Grounded = Animator.StringToHash("grounded");
        private static readonly int VelocityX = Animator.StringToHash("velocityX");
        private static readonly int Victory = Animator.StringToHash("victory");
        private static readonly int Hurt = Animator.StringToHash("hurt");
        private AnimalForm CurrentForm => animalForms[currentFormIndex];

        void Awake()
        {
            health = GetComponent<Health>();
            audioSource = GetComponent<AudioSource>();
            collider2d = GetComponent<Collider2D>();
            
            SetForm(0);
            // skillState = SkillState.Ready;
        }

        protected override void Start() {
            base.Start();
            
            formChangeWait = new WaitForSeconds(formChangeTime);
            formLoopCoroutine = StartCoroutine(StartFormLoop());
        }

        protected override void Update()
        {
            if (controlEnabled)
            {
                move.x = Input.GetAxis("Horizontal");
                if (jumpState == JumpState.Grounded && Input.GetButtonDown("Jump"))
                    jumpState = JumpState.PrepareToJump;
                else if (Input.GetButtonUp("Jump"))
                {
                    stopJump = true;
                    Schedule<PlayerStopJump>().player = this;
                } else if (Input.GetButtonDown("Fire1"))
                {
                    CastSkill(0);
                } else if (Input.GetButtonDown("Fire2"))
                {
                    CastSkill(1);
                } else if (Input.GetButtonDown("Fire3"))
                {
                    CastSkill(2);
                }
            }
            else
            {
                move.x = 0;
            }
            UpdateJumpState();
            base.Update();

            if (willChangeForm) {
                SetRandomForm();
                willChangeForm = false;
            }

            var toRemove = new List<int>();
            foreach (var (i, skillContext) in runningSkillContexts) {
                if (skillContext.SkillState == SkillState.Finished) {
                    toRemove.Add(i);
                }
            }
            
            foreach (var i in toRemove) {
                runningSkillContexts.Remove(i);
            }
        }

        public void OnVictory() {
            animator.SetTrigger(Victory);
            controlEnabled = false;
            StopCoroutine(formLoopCoroutine);
        }

        public void CastSkill(int index) {
            if (index < 0 || index >= CurrentForm.skills.Length) {
                return;
            }
            
            if (runningSkillContexts.Count > 0) {
                return;
            }
            
            var skill = CurrentForm.skills[index];
            var context = new SkillContext {
                playerController = this,
            };
            runningSkillContexts.Add(index, context);
            skill?.Execute(context);
        }
        
        public void SetLockForm(bool value) {
            isLockForm = value;
        }
        
        public void TakeDamage(int damage) {
            health.Decrement(damage);
            animator.SetTrigger(Hurt);
        }
        
        public Vector2 GetFaceDirection() {
            return spriteRenderer.flipX ^ defaultFlipX ? Vector2.left : Vector2.right;
        }
        
        private void SetForm(int index) {
            if (currentFormIndex == index) {
                return;
            }

            if (isLockForm) {
                willChangeForm = true;
                return;
            }
            
            if (visual) {
                Destroy(visual);
            }
            
            currentFormIndex = index;
            visual = Instantiate(CurrentForm.prefab, transform);
            spriteRenderer = visual.GetComponent<SpriteRenderer>();
            animator = visual.GetComponent<Animator>();
            defaultFlipX = spriteRenderer.flipX;
            
            jumpTakeOffSpeed = CurrentForm.jumpTakeOffSpeed;
            maxSpeed = CurrentForm.maxSpeed;
            gravityModifier = CurrentForm.gravityModifier;
            if (formTransformEffectPrefab) {
                Instantiate(formTransformEffectPrefab, transform.position, Quaternion.identity);
            }
        }
        
        private void SetRandomForm() {
            if (animalForms.Length < 2) {
                return;
            }

            int index;
            do {
                index = Random.Range(0, animalForms.Length);
            } while (index == currentFormIndex);
            SetForm(index);
        }
        
        private IEnumerator StartFormLoop() {
            while (true) {
                yield return formChangeWait;
                
                SetRandomForm();
            }
            // ReSharper disable once IteratorNeverReturns
        }

        void UpdateJumpState()
        {
            jump = false;
            switch (jumpState)
            {
                case JumpState.PrepareToJump:
                    jumpState = JumpState.Jumping;
                    jump = true;
                    stopJump = false;
                    break;
                case JumpState.Jumping:
                    if (!IsGrounded)
                    {
                        Schedule<PlayerJumped>().player = this;
                        jumpState = JumpState.InFlight;
                    }
                    break;
                case JumpState.InFlight:
                    if (IsGrounded)
                    {
                        Schedule<PlayerLanded>().player = this;
                        jumpState = JumpState.Landed;
                    }
                    break;
                case JumpState.Landed:
                    jumpState = JumpState.Grounded;
                    break;
            }
        }

        protected override void ComputeVelocity()
        {
            if (jump && IsGrounded)
            {
                velocity.y = jumpTakeOffSpeed * model.jumpModifier;
                jump = false;
            }
            else if (stopJump)
            {
                stopJump = false;
                if (velocity.y > 0)
                {
                    velocity.y = velocity.y * model.jumpDeceleration;
                }
            }

            if (move.x > 0.01f)
                spriteRenderer.flipX = defaultFlipX;
            else if (move.x < -0.01f)
                spriteRenderer.flipX = !defaultFlipX;

            animator.SetBool(Grounded, IsGrounded);
            animator.SetFloat(VelocityX, Mathf.Abs(velocity.x) / maxSpeed);

            targetVelocity = move * maxSpeed;
        }

        public enum JumpState
        {
            Grounded,
            PrepareToJump,
            Jumping,
            InFlight,
            Landed
        }

        private void OnDrawGizmos() {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, 1f);
        }
    }
}