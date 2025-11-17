using UnityEngine;

namespace ShootEmUpZenject
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private Transform _firePoint;
        public Vector3 Firepoint => _firePoint.transform.position;
    }
}