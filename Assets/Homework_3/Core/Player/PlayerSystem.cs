using System;
using UnityEngine;
using Zenject;

namespace ShootEmUpZenject
{
    public class PlayerSystem : IFixedTickable, IDisposable
    {
        private readonly EventBus _eventBus;
        private PlayerConfig _config;
        private IMovable _movement;
        private IAttackSystem _attackSystem;
        private IInputService _inputService;
        private float _health;
        public event Action PlayerDeath;

        [Inject]
        public PlayerSystem(PlayerConfig config, IMovable movement, IAttackSystem attackSystem, IInputService inputService, EventBus eventBus)
        {
            _eventBus = eventBus;
            _config = config;
            _movement = movement;
            _attackSystem = attackSystem;
            _inputService = inputService;

            _inputService.OnFirePressed += Attack;
            _health = _config.BaseHealth;
        }

        private void Attack()
        {
            _attackSystem.Shoot();
        }

        public void TakeDamage(int damage)
        {
            _health -= damage;
            if (_health <= 0)
            {
                _eventBus.Invoke(new PlayerDiedSignal());
            }
        }

        public void FixedTick()
        {
            var input = _inputService.GetMovementInput();
            var moveInput = new Vector2(input.x, input.y);
            _movement.Move(moveInput * Time.fixedDeltaTime, _config.BaseSpeed);
        }

        public void Dispose()
        {
            _inputService.OnFirePressed -= Attack;
        }
    }

    public struct PlayerDiedSignal { }
}