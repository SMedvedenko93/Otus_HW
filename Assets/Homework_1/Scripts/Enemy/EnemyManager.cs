using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    [RequireComponent(typeof(HitPointsComponent))]
    public sealed class EnemyManager : MonoBehaviour, IGameFixedUpdateListener, IGameStartListener, IGamePauseListener, IGameResumeListener, IGameFinishListener
    {
        [SerializeField] private EnemySpawner enemySpawner;
        [SerializeField] private int respawnTime;
        [SerializeField] private readonly HashSet<GameObject> activeEnemies = new();
        private Coroutine spawnCoroutine;

        private IEnumerator StartSpawn()
        {
            while (true)
            {
                yield return new WaitForSeconds(respawnTime);
                var enemy = this.enemySpawner.SpawnEnemy();
                if (enemy != null)
                {
                    if (this.activeEnemies.Add(enemy))
                    {
                        enemy.GetComponent<HitPointsComponent>().OnHealthEmpted += this.OnDestroyed;
                    }    
                }
            }
        }

        private void OnDestroyed(GameObject enemy)
        {
            if (activeEnemies.Remove(enemy))
            {
                enemy.GetComponent<HitPointsComponent>().OnHealthEmpted -= this.OnDestroyed;
                this.enemySpawner.UnspawnEnemy(enemy);
            }
        }

        void IGameStartListener.StartGame()
        {
            spawnCoroutine = StartCoroutine(StartSpawn());
        }

        void IGameFinishListener.FinishGame()
        {
            StopCoroutine(spawnCoroutine);
        }

        void IGamePauseListener.PauseGame()
        {
            foreach (GameObject enemy in activeEnemies)
            {
                enemy.GetComponent<EnemyController>().PauseGame();
            }
        }

        void IGameResumeListener.ResumeGame()
        {
            foreach (GameObject enemy in activeEnemies)
            {
                enemy.GetComponent<EnemyController>().ResumeGame();
            }
        }

        public void CustomFixedUpdate(float fixedDeltaTime)
        {
            foreach (GameObject enemy in activeEnemies)
            {
                enemy.GetComponent<EnemyMoveAgent>().CustomFixedUpdate(fixedDeltaTime);
                enemy.GetComponent<EnemyAttackAgent>().CustomFixedUpdate(fixedDeltaTime);
            }
        }
    }
}