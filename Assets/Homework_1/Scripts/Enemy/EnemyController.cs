using UnityEngine;

namespace ShootEmUp
{
    [RequireComponent(typeof(EnemyMoveAgent))]
    [RequireComponent(typeof(EnemyAttackAgent))]
    [RequireComponent(typeof(ShootComponent))]
    [RequireComponent(typeof(WeaponComponent))]

    public class EnemyController : MonoBehaviour, IGameStartListener, IGamePauseListener, IGameResumeListener, IGameFinishListener
    {
        private EnemyAttackAgent enemyAttackAgent => GetComponent<EnemyAttackAgent>();
        private bool isActive;

        public void Initialize(Transform worldTransform, Transform enemySpawnPositions, Transform enemyAttackPositions, GameObject character, BulletSystem bulletSystem)
        {
            this.transform.SetParent(worldTransform);
            this.transform.position = enemySpawnPositions.position;
            this.GetComponent<EnemyMoveAgent>().SetDestination(enemyAttackPositions.position);
            this.GetComponent<EnemyAttackAgent>().SetTarget(character);
            this.GetComponent<ShootComponent>().SetBulletSystem(bulletSystem);

            this.GetComponent<EnemyMoveAgent>().StartGame();
        }

        private void OnFire()
        {
            if (isActive)
            {
                this.GetComponent<ShootComponent>().Fire(GetComponent<WeaponComponent>());
            } 
        }

        public void StartGame()
        {
            isActive = true;
            this.enemyAttackAgent.OnFire += this.OnFire;
        }

        public void PauseGame()
        {
            isActive = false;
            this.GetComponent<EnemyMoveAgent>().PauseGame();
        }

        public void ResumeGame()
        {
            isActive = true;
            this.GetComponent<EnemyMoveAgent>().ResumeGame();
        }

        public void FinishGame()
        {
            isActive = false;
            this.enemyAttackAgent.OnFire -= this.OnFire;
        }
    }
}