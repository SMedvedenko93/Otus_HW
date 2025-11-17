using UnityEngine;

namespace ShootEmUpZenject
{
    public class EnemyView : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private CircleCollider2D _collider;
        [SerializeField] private Transform _firePoint;
        public Vector3 Firepoint => _firePoint.transform.position;

        public Vector3 Position
        {
            get => transform.position;
            set => transform.position = value;
        }

        public Rigidbody2D Rigidbody => _rigidbody2D;
        public CircleCollider2D Collider => _collider;
    }
}
