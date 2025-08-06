using UnityEngine;

namespace ShootEmUp
{
    public class EnemyController : MonoBehaviour
    {
        private void OnEnable()
        {
            this.GetComponent<EnemyAttackAgent>().OnFire += this.OnFire;
        }

        private void OnDisable()
        {
            this.GetComponent<EnemyAttackAgent>().OnFire -= this.OnFire;
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