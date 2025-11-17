using UnityEngine;
using Zenject;

namespace ShootEmUpZenject
{
    public class EnemySpawner : ITickable
    {
        readonly EnemyFacade.Factory _enemyFactory;
        private readonly EnemySpawnerConfig _enemySpawnerConfig;
        private float _timer;
        int _enemyCount;

        [Inject]
        public EnemySpawner(
            EnemySpawnerConfig enemySpawnerConfig,
            EnemyFacade.Factory enemyFactory
            )
        {
            _enemyFactory = enemyFactory;
            _enemySpawnerConfig = enemySpawnerConfig;
        }

        public void Tick()
        {
            if (_enemyCount >= _enemySpawnerConfig.EnemyCount)
                return;

            _timer += Time.deltaTime;
            if (_timer >= _enemySpawnerConfig.respawnTime)
            {
                _timer = 0f;
                var spawnPosition = RandomSpawnPosition();
                var attackPosition = RandomAttackPosition();
                var enemyFacade = _enemyFactory.Create(spawnPosition, attackPosition);
                enemyFacade.EnemyDeath += OnEnemyKilled;
                _enemyCount++;
            }
        }

        void OnEnemyKilled()
        {
            _enemyCount--;
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