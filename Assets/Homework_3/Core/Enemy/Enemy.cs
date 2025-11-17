using System;
using UnityEngine;
using Zenject;

namespace ShootEmUpZenject
{
    public class Enemy : IMovable, IDisposable
    {
        private readonly EnemyManager _enemyManager;
        private readonly EnemySpawnerConfig _enemySpawnerConfig;
        private readonly IAttackSystem _attackSystem;
        private EnemyView _view;
        private EnemyFacade _enemyFacade;
        private IMemoryPool _pool;
        private Vector2 _destination;
        private bool _isActive;
        private bool _isReached;
        private float _magnitute;
        private float _health;
        private float _speed;
        private float _countdown;
        private float _currentTime;

        [Inject]
        public Enemy(EnemyManager enemyManager, EnemySpawnerConfig enemySpawnerConfig, IAttackSystem attackSystem)
        {
            _enemyManager = enemyManager;
            _enemySpawnerConfig = enemySpawnerConfig;
            _attackSystem = attackSystem;
        }

        public void Init(EnemyView view, EnemyFacade enemyFacade)
        {
            _view = view;
            _enemyFacade = enemyFacade;
            _health = _enemySpawnerConfig.BaseHealth;
            _magnitute = _enemySpawnerConfig.BaseMagnitute;
            _speed = _enemySpawnerConfig.BaseSpeed;
            _countdown = _enemySpawnerConfig.AttackCountDown;
            _currentTime = _countdown;
        }

        public void OnSpawned(Vector3 spawnPosition, Vector3 attackPosition)
        {
            _view.Position = spawnPosition;
            _destination = attackPosition;
            _enemyManager.AddEnemy(this);
            _isReached = false;
            _isActive = true;
        }

        public void OnDespawned()
        {
            _pool = null;
            _isActive = false;
            _enemyManager.RemoveEnemy(this);
        }

        public void TakeDamage(int damage)
        {
            _health -= damage;
            if (_health <= 0)
            {
                _enemyFacade.Dispose();
            }
        }

        public void Dispose()
        {
            _pool?.Despawn(this);
        }

        public void FixedTick()
        {
            if (_isReached && _isActive)
            {
                _currentTime -= Time.fixedDeltaTime;
                if (_currentTime <= 0)
                {
                    Fire();
                    _currentTime += _countdown;
                }
            }

            if (_isReached)
            {
                return;
            }

            var vector = _destination - (Vector2)_view.Position;
            if (vector.magnitude <= _magnitute)
            {
                _isReached = true;
                return;
            }

            var direction = vector.normalized * Time.fixedDeltaTime;
            Move(direction, _speed);
        }

        public void Move(Vector2 direction, float speed)
        {
            var nextPosition = _view.Rigidbody.position + direction * speed;
            _view.Rigidbody.MovePosition(nextPosition);
        }

        private void Fire()
        {
            _attackSystem.Shoot();
        }

    }
}
