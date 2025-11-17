namespace ShootEmUpZenject
{
    public class EnemyAttackSystem : IAttackSystem
    {
        private Bullet.Factory _bulletFactory;
        private EnemySpawnerConfig _enemyConfig;
        private EnemyView _enemyView;

        public EnemyAttackSystem(Bullet.Factory bulletFactory, EnemySpawnerConfig enemyConfig, EnemyView enemyView)
        {
            _bulletFactory = bulletFactory;
            _enemyConfig = enemyConfig;
            _enemyView = enemyView;
        }

        public void Shoot()
        {
            var bullet = _bulletFactory.Create(
                _enemyConfig.BulletSpeed * _enemyConfig.BulletDirection,
                _enemyView.Firepoint,
                _enemyConfig.BulletDamage,
                BulletType.Player);
            bullet.SetPhysicsLayer((int)_enemyConfig.BulletPhysicsLayer);
            bullet.SetColor(_enemyConfig.BulletColor);
        }
    }
}