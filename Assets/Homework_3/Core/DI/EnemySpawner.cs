using ShootEmUp;
using UnityEngine;
using Zenject;

namespace ShootEmUpZenject
{
    public class EnemySpawner : ITickable
    {
        private readonly Enemy.Pool _pool;
        private readonly EnemySpawnerConfig _enemySpawnerConfig;
        private float _timer;

        [Inject]
        public EnemySpawner(Enemy.Pool pool, EnemySpawnerConfig enemySpawnerConfig)
        {
            _pool = pool;
            _enemySpawnerConfig = enemySpawnerConfig;
        }

        public void Tick()
        {
            if (_pool.NumActive >= _enemySpawnerConfig.EnemyCount)
                return;

            _timer += Time.deltaTime;
            if (_timer >= _enemySpawnerConfig.respawnTime)
            {
                _timer = 0f;
                var spawnPosition = RandomSpawnPosition();
                var attackPosition = RandomAttackPosition();
                _pool.Spawn(spawnPosition, attackPosition);
            }
        }

        private Vector3 RandomPosition(Vector3[] positions)
        {
            var index = Random.Range(0, positions.Length);
            return positions[index];
        }

        public Vector3 RandomAttackPosition()
        {
            return RandomPosition(_enemySpawnerConfig.attackPositions);
        }

        public Vector3 RandomSpawnPosition()
        {
            return RandomPosition(_enemySpawnerConfig.spawnPositions);
        }
    }
}