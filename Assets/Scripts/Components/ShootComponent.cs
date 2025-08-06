using System;
using UnityEngine;

namespace ShootEmUp
{
    public class ShootComponent : MonoBehaviour
    {
        [SerializeField] private BulletConfig bulletConfig;
        [SerializeField] private BulletSystem bulletSystem;
        [SerializeField] private bool isPlayer;

        public void SetBulletSystem(BulletSystem bulletSystem)
        {
            this.bulletSystem = bulletSystem;
        }

        public void Fire(WeaponComponent weapon)
        {
            this.bulletSystem.CreateBulletByArgs(new BulletSystem.Args
            {
                isPlayer = isPlayer,
                physicsLayer = (int)this.bulletConfig.physicsLayer,
                color = this.bulletConfig.color,
                damage = this.bulletConfig.damage,
                position = weapon.Position,
                velocity = weapon.Rotation * bulletConfig.direction * this.bulletConfig.speed
            });
        }

    }
}