using UnityEngine;

namespace ShootEmUp
{
    [RequireComponent(typeof(EnemyMoveAgent))]
    [RequireComponent(typeof(EnemyAttackAgent))]
    [RequireComponent(typeof(ShootComponent))]
    [RequireComponent(typeof(WeaponComponent))]
    public class EnemyController : MonoBehaviour
    {
        private EnemyAttackAgent enemyAttackAgent => GetComponent<EnemyAttackAgent>();

        private void OnEnable()
        {
            this.enemyAttackAgent.OnFire += this.OnFire;
        }

        private void OnDisable()
        {
            this.enemyAttackAgent.OnFire -= this.OnFire;
        }

        public void Initialize(Transform worldTransform, Transform enemySpawnPositions, Transform enemyAttackPositions, GameObject character, BulletSystem bulletSystem)
        {
            this.transform.SetParent(worldTransform);
            this.transform.position = enemySpawnPositions.position;
            this.GetComponent<EnemyMoveAgent>().SetDestination(enemyAttackPositions.position);
            this.GetComponent<EnemyAttackAgent>().SetTarget(character);
            this.GetComponent<ShootComponent>().SetBulletSystem(bulletSystem);
        }

        private void OnFire()
        {
            this.GetComponent<ShootComponent>().Fire(GetComponent<WeaponComponent>());
        }
    }
}