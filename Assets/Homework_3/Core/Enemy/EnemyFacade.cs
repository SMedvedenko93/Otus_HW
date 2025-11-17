using System;
using UnityEngine;
using Zenject;

namespace ShootEmUpZenject
{
    public class EnemyFacade : MonoBehaviour, IDisposable, IDamageable, IPoolable<Vector3, Vector3, IMemoryPool>
    {
        private EnemyView _view => GetComponent<EnemyView>();
        private Enemy _enemy;
        IMemoryPool _pool;
        public event Action EnemyDeath;

        [Inject]
        public void Construct(Enemy enemy, EnemySpawnerConfig enemySpawnerConfig)
        {
            _enemy = enemy;
            _enemy.Init(_view, this);
        }

        public void OnSpawned(Vector3 spawnPosition, Vector3 attackPosition, IMemoryPool pool)
        {
            _pool = pool;
            transform.position = spawnPosition;
            _enemy.OnSpawned(spawnPosition, attackPosition);
        }

        public void OnDespawned()
        {
            _enemy.OnDespawned();
            _pool = null;
        }

        public void TakeDamage(int damage)
        {
            _enemy.TakeDamage(damage);
        }

        public void Dispose()
        {
            EnemyDeath?.Invoke();
            _pool.Despawn(this);
        }

        public Vector3 Position
        {
            get { return _view.Position; }
            set { _view.Position = value; }
        }

        public class Factory : PlaceholderFactory<Vector3, Vector3, EnemyFacade>
        {
        }
    }
}
