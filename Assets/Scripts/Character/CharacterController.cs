using UnityEngine;

namespace ShootEmUp
{
    [RequireComponent(typeof(MoveComponent))]
    [RequireComponent(typeof(ShootComponent))]
    [RequireComponent(typeof(HitPointsComponent))]
    public sealed class CharacterController : MonoBehaviour
    {
        [SerializeField] private GameObject character;
        [SerializeField] private GameManager gameManager;
        [SerializeField] private InputManager inputManager;
        private MoveComponent moveComponent => character.GetComponent<MoveComponent>();
        private ShootComponent shootComponent => character.GetComponent<ShootComponent>();
        private HitPointsComponent hitPointsComponent => character.GetComponent<HitPointsComponent>();
        
        private void OnEnable()
        {
            this.hitPointsComponent.OnHealthEmpted += this.OnCharacterDeath;
            this.inputManager.OnHorizontalMovement += Move;
            this.inputManager.OnFirePressed += OnFire;
        }

        private void OnDisable()
        {
            this.hitPointsComponent.OnHealthEmpted -= this.OnCharacterDeath;
            this.inputManager.OnHorizontalMovement -= Move;
            this.inputManager.OnFirePressed -= OnFire;
        }

        private void OnCharacterDeath(GameObject _)
        {
            this.gameManager.FinishGame();
        }


        private void OnFire()
        {
            var weapon = this.character.GetComponent<WeaponComponent>();
            this.shootComponent.Fire(weapon);
        }

        private void Move(float direction)
        {
            this.moveComponent.MoveByRigidbodyVelocity(new Vector2(direction, 0) * Time.fixedDeltaTime);
        }
    }
}