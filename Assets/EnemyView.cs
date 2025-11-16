using System;
using UnityEngine;
using Zenject;

namespace ShootEmUpZenject
{
    public class EnemyView : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private CircleCollider2D _collider;
        private Enemy _enemy;
        private IMemoryPool _pool;

        public Vector3 Position
        {
            get => transform.position;
            set => transform.position = value;
        }

        public Rigidbody2D Rigidbody => _rigidbody2D;
        public CircleCollider2D Collider => _collider;

        public event Action<Collision> OnCollisionEntered;

        public void OnSpawned(Vector3 spawnPosition, Vector3 attackPosition, IMemoryPool pool)
        {
            _pool = pool;
            _enemy.OnSpawned(spawnPosition, attackPosition, pool);
        }

        private void OnCollisionEnter(Collision collision)
        {
            OnCollisionEntered?.Invoke(collision);
        }
    }
}
