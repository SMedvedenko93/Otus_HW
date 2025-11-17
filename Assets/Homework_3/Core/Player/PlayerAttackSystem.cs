using Zenject;

namespace ShootEmUpZenject
{
    public class PlayerAttackSystem : IAttackSystem
    {
        private Bullet.Factory _bulletFactory;
        private PlayerConfig _playerConfig;
        private PlayerView _playerView;

        [Inject]
        public PlayerAttackSystem(Bullet.Factory bulletFactory, PlayerConfig playerConfig, PlayerView player)
        {
            _bulletFactory = bulletFactory;
            _playerConfig = playerConfig;
            _playerView = player;
        }

        public void Shoot()
        {
            var bullet = _bulletFactory.Create(
                _playerConfig.BulletSpeed * _playerConfig.BulletDirection, 
                _playerView.Firepoint,
                _playerConfig.BulletDamage,
                BulletType.Player);
            bullet.SetPhysicsLayer((int)_playerConfig.BulletPhysicsLayer);
            bullet.SetColor(_playerConfig.BulletColor);
        }
    }
}