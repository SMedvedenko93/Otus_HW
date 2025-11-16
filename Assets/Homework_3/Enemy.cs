using System;
using UnityEngine;
using Zenject;

namespace ShootEmUpZenject
{
    public class Enemy : IPoolable<Vector3, Vector3, IMemoryPool>, IMovable, IDisposable
    {
        private readonly EnemyManager _enemyManager;
        private readonly EnemyView _view;
        private IMemoryPool _pool;
        private Vector2 destination;
        private bool isReached;
        private float magnitute = 0.25f;

        public Enemy(EnemyView view, EnemyManager enemyManager)
        {
            _enemyManager = enemyManager;
            _view = view;
            _view.OnCollisionEntered += OnCollisionEnter;
        }

        public void OnSpawned(Vector3 spawnPosition, Vector3 attackPosition, IMemoryPool pool)
        {
            _enemyManager.AddEnemy(this);
            _pool = pool;
            _view.Position = spawnPosition;
            SetDestination(attackPosition);
        }

        public void SetDestination(Vector2 attackPosition)
        {
            destination = attackPosition;
            isReached = false;
        }

        public void OnDespawned()
        {
            _pool = null;

        }

        public void TakeDamage(int damage)
        {
            //Dispose();
        }

        public void Dispose()
        {
            _pool?.Despawn(this);
        }

        public void FixedTick()
        {
            if (isReached)
            {
                return;
            }

            var vector = destination - (Vector2)_view.Position;
            if (vector.magnitude <= magnitute)
            {
                isReached = true;
                return;
            }

            var direction = vector.normalized * Time.fixedDeltaTime;
            Move(direction, 7);
        }

        public void Move(Vector2 direction, float speed)
        {
            var nextPosition = _view.Rigidbody.position + direction * speed;
            _view.Rigidbody.MovePosition(nextPosition);
        }

        private void OnCollisionEnter(Collision collision)
        {
            
        }

        public class Pool : MonoMemoryPool<Vector3, Vector3, EnemyView> 
        {
            protected override void Reinitialize(Vector3 spawnPosition, Vector3 attackPosition, EnemyView enemyView)
            {
                Debug.Log("Reinitialize Reinitialize Reinitialize");
                //base.Reinitialize(spawnPosition, attackPosition, item);
                //item.OnSpawned(spawnPosition, attackPosition, this);
                enemyView.OnSpawned(spawnPosition, attackPosition, this);
            }
        }
    }
}
