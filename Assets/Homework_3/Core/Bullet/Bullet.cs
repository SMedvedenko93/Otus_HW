using System;
using UnityEngine;
using Zenject;

namespace ShootEmUpZenject
{
    public enum BulletType
    {
        Enemy,
        Player
    }

    public class Bullet : MonoBehaviour, IPoolable<Vector3, Vector3, int, BulletType, IMemoryPool>
    {
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        public event Action<Bullet, Collision2D> OnCollisionEntered;
        private IMemoryPool _pool;
        public int _damage;
        private BulletManager _bulletManager;
        private LevelBounds _levelBounds;

        [Inject]
        public void Construct(BulletManager bulletManager, LevelBounds levelBounds)
        {
            _bulletManager = bulletManager;
            _levelBounds = levelBounds;
        }

        public void OnSpawned(Vector3 speed, Vector3 position, int damage, BulletType type, IMemoryPool pool)
        {
            _bulletManager.AddBullet(this);
            _pool = pool;
            _damage = damage;
            transform.position = position;
            _rigidbody2D.linearVelocity = speed;
        }

        public void OnDespawned()
        {
            _pool = null;
            _bulletManager.RemoveBullet(this);
        }

        public void Dispose()
        {
            _pool?.Despawn(this);
        }

        public void SetColor(Color color)
        {
            _spriteRenderer.color = color;
        }

        public void SetPhysicsLayer(int physicsLayer)
        {
            gameObject.layer = physicsLayer;
        }

        public void OnCollisionEnter2D(Collision2D collision)
        {
            var target = collision.gameObject.GetComponent<IDamageable>();
            if (target != null)
            {
                target.TakeDamage(_damage);
            }  
            _pool.Despawn(this);
        }

        public void FixedTick()
        {
            if (!_levelBounds.InBounds(transform.position))
            {
                _pool.Despawn(this);
            }
        }

        public class Factory : PlaceholderFactory<Vector3, Vector3, int, BulletType, Bullet>
        {
        }
    }
}
