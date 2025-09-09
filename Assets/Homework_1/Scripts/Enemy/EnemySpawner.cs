using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemySpawner : MonoBehaviour, IStartGameListener, IPauseGameListener, IResumeGameListener, IFinishGameListener
    {
        [Header("Spawn")]
        [SerializeField] private EnemyPositions enemyPositions;
        [SerializeField] private GameObject character;
        [SerializeField] private Transform worldTransform;
        [SerializeField] private int spawnCount;

        [Header("Pool")]
        [SerializeField] private Transform container;
        [SerializeField] private GameObject prefab;
        [SerializeField] private BulletSystem bulletSystem;

        private readonly Queue<GameObject> enemyPool = new();
        private bool isActive;

        private void Awake()
        {
            for (var i = 0; i < spawnCount; i++)
            {
                var enemy = Instantiate(this.prefab, this.container);
                this.enemyPool.Enqueue(enemy);
            }
        }

        public GameObject SpawnEnemy()
        {
            if (!isActive)
                return null;

            if (!this.enemyPool.TryDequeue(out var enemy))
            {
                return null;
            }

            EnemyController enemyController = enemy.GetComponent<EnemyController>();

            enemyController.Initialize(
                this.worldTransform,
                this.enemyPositions.RandomSpawnPosition(),
                this.enemyPositions.RandomAttackPosition(),
                this.character,
                bulletSystem
                );

            return enemy;
        }

        public void UnspawnEnemy(GameObject enemy)
        {
            enemy.transform.SetParent(this.container);
            this.enemyPool.Enqueue(enemy);
        }

        public void StartGame()
        {
            isActive = true;
        }

        public void PauseGame()
        {
            isActive = false;
        }

        public void ResumeGame()
        {
            isActive = true;
        }

        public void FinishGame()
        {
            isActive = false;
        }
    }
}