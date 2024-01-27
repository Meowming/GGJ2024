using System.Collections;
using Config;
using UnityEngine;
using Platformer.Gameplay;
using static Platformer.Core.Simulation;
using Platformer.Model;
using Platformer.Core;

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
        public float maxSpeed = 7;
        /// <summary>
        /// Initial jump velocity at the start of a jump.
        /// </summary>
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
        internal Animator animator;
        readonly PlatformerModel model = Simulation.GetModel<PlatformerModel>();

        public Bounds Bounds => collider2d.bounds;

        public AnimalForm[] animalForms;
        public float formChangeTime = 5f;
        public GameObject formTransformEffectPrefab;
        
        private int currentFormIndex = -1;
        private GameObject visual;
        private Coroutine formLoopCoroutine;
        private WaitForSeconds formChangeWait;
        
        private static readonly int Grounded = Animator.StringToHash("grounded");
        private static readonly int VelocityX = Animator.StringToHash("velocityX");
        private AnimalForm CurrentForm => animalForms[currentFormIndex];

        void Awake()
        {
            health = GetComponent<Health>();
            audioSource = GetComponent<AudioSource>();
            collider2d = GetComponent<Collider2D>();
            // spriteRenderer = GetComponent<SpriteRenderer>();
            // animator = GetComponent<Animator>();
            SetForm(0);
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
                }
            }
            else
            {
                move.x = 0;
            }
            UpdateJumpState();
            base.Update();
        }
        
        private void SetForm(int index) {
            if (currentFormIndex == index) {
                return;
            }
            
            if (visual) {
                Destroy(visual);
            }
            
            currentFormIndex = index;
            visual = Instantiate(CurrentForm.prefab, transform);
            spriteRenderer = visual.GetComponent<SpriteRenderer>();
            animator = visual.GetComponent<Animator>();
            
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
                spriteRenderer.flipX = false;
            else if (move.x < -0.01f)
                spriteRenderer.flipX = true;

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