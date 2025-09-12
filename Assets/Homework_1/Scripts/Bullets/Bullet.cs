using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class Bullet : MonoBehaviour, IGamePauseListener
    {
        public event Action<Bullet, Collision2D> OnCollisionEntered;

        [NonSerialized] public bool isPlayer;
        [NonSerialized] public int damage;

        [SerializeField] private new Rigidbody2D rigidbody2D;
        [SerializeField] private SpriteRenderer spriteRenderer;

        Vector2 baseVelocity;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            this.OnCollisionEntered?.Invoke(this, collision);
        }

        public void SetVelocity(Vector2 velocity)
        {
            this.rigidbody2D.linearVelocity = velocity;
        }

        public void SetPhysicsLayer(int physicsLayer)
        {
            this.gameObject.layer = physicsLayer;
        }

        public void SetPosition(Vector3 position)
        {
            this.transform.position = position;
        }

        public void SetColor(Color color)
        {
            this.spriteRenderer.color = color;
        }

        public void Initialize(BulletSystem.Args args)
        {
            this.SetPosition(args.position);
            this.SetColor(args.color);
            this.SetPhysicsLayer(args.physicsLayer);
            this.damage = args.damage;
            this.isPlayer = args.isPlayer;
            this.SetVelocity(args.velocity);
            baseVelocity = args.velocity;
        }

        public void PauseGame()
        {
            this.SetVelocity(Vector2.zero);
        }

        public void ResumeGame()
        {
            this.SetVelocity(baseVelocity);
        }
    }
}