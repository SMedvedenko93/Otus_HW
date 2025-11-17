using UnityEngine;
using Zenject;

namespace ShootEmUpZenject
{
    public class PlayerFacade : MonoBehaviour, IDamageable
    {
        private PlayerSystem _playerSystem;

        [Inject]
        public void Construct(PlayerSystem playerSystem, PlayerConfig config)
        {
            _playerSystem = playerSystem;
            transform.position = config.SpawnPosition;
        }

        public void TakeDamage(int damage)
        {
            _playerSystem.TakeDamage(damage);
        }
    }
}
