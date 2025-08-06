using UnityEngine;

namespace ShootEmUp
{
    public sealed class CharacterController : MonoBehaviour
    {
        [SerializeField] private GameObject character;
        [SerializeField] private GameManager gameManager;
        [SerializeField] private InputManager inputManager;
        [SerializeField] private MoveComponent _moveComponent => character.GetComponent<MoveComponent>();
        [SerializeField] private ShootComponent _shootComponent => character.GetComponent<ShootComponent>();
        
        private void OnEnable()
        {
            this.character.GetComponent<HitPointsComponent>().hpEmpty += this.OnCharacterDeath;
            this.inputManager.OnHorizontalMovement += Move;
            this.inputManager.OnFirePressed += OnFire;
        }

        private void OnDisable()
        {
            this.character.GetComponent<HitPointsComponent>().hpEmpty -= this.OnCharacterDeath;
            this.inputManager.OnHorizontalMovement -= Move;
            this.inputManager.OnFirePressed -= OnFire;
        }

        private void OnCharacterDeath(GameObject _) => this.gameManager.FinishGame();


        private void OnFire()
        {
            var weapon = this.character.GetComponent<WeaponComponent>();
            this._shootComponent.Fire(weapon);
        }

        private void Move(float direction)
        {
            this._moveComponent.MoveByRigidbodyVelocity(new Vector2(direction, 0) * Time.fixedDeltaTime);
        }
    }
}