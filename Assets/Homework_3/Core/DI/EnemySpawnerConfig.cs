using UnityEngine;

namespace ShootEmUpZenject
{
    [CreateAssetMenu(fileName = "EnemySpawnerConfig", menuName = "Game/Configs/EnemySpawnerConfig")]
    public class EnemySpawnerConfig : ScriptableObject
    {
        [Header("Base Stats")]
        public int EnemyCount;
        public int respawnTime;
        public Vector3[] spawnPositions;
        public Vector3[] attackPositions;
    }
}
