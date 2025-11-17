using ShootEmUp;
using UnityEngine;

namespace ShootEmUpZenject
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Game/Configs/PlayerConfig")]
    public class PlayerConfig : ScriptableObject
    {
        [Header("Base Stats")]
        public int BaseHealth;
        public float BaseSpeed;
        public Vector3 SpawnPosition;
        [Header("Bullet Stats")]
        public Vector2 BulletDirection;
        public float BulletSpeed;
        public int BulletDamage;
        public PhysicsLayer BulletPhysicsLayer;
        public Color BulletColor;
    }
}