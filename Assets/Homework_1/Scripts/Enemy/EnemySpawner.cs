using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemySpawner : MonoBehaviour, IGameStartListener, IGamePauseListener, IGameResumeListener, IGameFinishListener
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

            enemyController.StartGame();

            return enemy;
        }

        public void UnspawnEnemy(GameObject enemy)
        {
            enemy.transform.SetParent(this.container);
            this.enemyPool.Enqueue(enemy);
        }

        void IGameStartListener.StartGame()
        {
            isActive = true;
            for (var i = 0; i < spawnCount; i++)
            {
                var enemy = Instantiate(this.prefab, this.container);
                this.enemyPool.Enqueue(enemy);
            }  
        }

        void IGamePauseListener.PauseGame()
        {
            isActive = false;
            foreach (GameObject enemy in enemyPool)
            {
                enemy.GetComponent<EnemyController>().PauseGame();
            }
        }

        void IGameResumeListener.ResumeGame()
        {
            isActive = true;
            foreach (GameObject enemy in enemyPool)
            {
                enemy.GetComponent<EnemyController>().ResumeGame();
            }
        }

        void IGameFinishListener.FinishGame()
        {
            isActive = false;
        }
    }
}