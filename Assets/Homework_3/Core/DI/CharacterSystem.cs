using System;
using UnityEngine;
using Zenject;

namespace ShootEmUpZenject
{
    public class CharacterSystem : IFixedTickable, IDamageable, IDisposable
    {
        private CharacterConfig _config;
        private IMovable _movement;
        private AttackSystem _attackSystem;
        private IInputService _inputService;

        [Inject]
        public CharacterSystem(
            CharacterConfig config,
            IMovable movement,
            AttackSystem attackSystem,
            IInputService inputService
            )
        {
            _config = config;
            _movement = movement;
            _attackSystem = attackSystem;
            _inputService = inputService;

            _inputService.OnFirePressed += Attack;
        }

        private void Attack()
        {
            _attackSystem.Shoot();
        }

        public void TakeDamage(int damage)
        {

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
}