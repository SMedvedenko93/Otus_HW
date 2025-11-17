using ShootEmUp;
using UnityEngine;

namespace ShootEmUpZenject
{
    [CreateAssetMenu(fileName = "EnemySpawnerConfig", menuName = "Game/Configs/EnemySpawnerConfig")]
    public class EnemySpawnerConfig : ScriptableObject
    {
        [Header("Base Stats")]
        public int AttackCountDown;
        public int EnemyCount;
        public int respawnTime;
        public int BaseHealth;
        public int BaseSpeed;
        public float BaseMagnitute;
        public Vector3[] spawnPositions;
        public Vector3[] attackPositions;
        [Header("Bullet Stats")]
        public Vector2 BulletDirection;
        public float BulletSpeed;
        public int BulletDamage;
        public PhysicsLayer BulletPhysicsLayer;
        public Color BulletColor;
    }
}
