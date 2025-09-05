using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    [RequireComponent(typeof(HitPointsComponent))]
    public sealed class EnemyManager : MonoBehaviour
    {
        [SerializeField] private EnemySpawner enemySpawner;
        [SerializeField] private int respawnTime;
        private readonly HashSet<GameObject> activeEnemies = new();

        private IEnumerator Start()
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

    }
}