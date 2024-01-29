using System.Collections;
using System.Collections.Generic;
using Platformer.Gameplay;
using Unity.VisualScripting;
using UnityEngine;
using static Platformer.Core.Simulation;

namespace Platformer.Mechanics
{
    /// <summary>
    /// A simple controller for enemies. Provides movement control over a patrol path.
    /// </summary>
    public class EnemyController : MonoBehaviour
    {
        public PatrolPath path;
        public AudioClip ouch;

        internal PatrolPath.Mover mover;
        internal AnimationController control;
        internal Collider2D _collider;
        internal AudioSource _audio;
        private Rigidbody2D _rigidbody2D;
        SpriteRenderer spriteRenderer;
        private Health health;
        
        private static readonly int Death = Animator.StringToHash("death");
        private static readonly int Hurt = Animator.StringToHash("hurt");

        public Bounds Bounds => _collider.bounds;

        void Awake()
        {
            control = GetComponentInChildren<AnimationController>();
            _collider = GetComponent<Collider2D>();
            _audio = GetComponent<AudioSource>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
            health = GetComponent<Health>();
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            var player = collision.gameObject.GetComponent<PlayerController>();
            if (player != null)
            {
                // var ev = Schedule<PlayerEnemyCollision>();
                // ev.player = player;
                // ev.enemy = this;
                
                player.TakeDamage(1);
            }
        }

        void Update()
        {
            if (path != null)
            {
                if (mover == null) mover = path.CreateMover(control.maxSpeed * 0.5f);
                control.move.x = Mathf.Clamp(mover.Position.x - transform.position.x, -1, 1);
            }
        }

        public void TakeDamage(int damage) {
            health.Decrement(damage);
            control.animator.SetTrigger(Hurt);
            if (!health.IsAlive) {
                control.animator.SetTrigger(Death);
                //_rigidbody2D.AddForce(new Vector2(Random.Range(20f, 100f), Random.Range(20f, 100f)));
                _collider.enabled = false;
                _rigidbody2D.bodyType = RigidbodyType2D.Static;
                _rigidbody2D.simulated = false;
            }
        }

        IEnumerator EnemyRandomMovement()
        {
            float timer = 0f;
            int direction = Random.Range(-1, 2);
            timer += Time.deltaTime;
            //TODO: remove hard coded numbers
            float timeLimit = Random.Range(1f, 10f);
            while (timer <= timeLimit)
            {
                //_rigidbody2D.MovePosition()
            }
            yield break;
        }
        
    }
}