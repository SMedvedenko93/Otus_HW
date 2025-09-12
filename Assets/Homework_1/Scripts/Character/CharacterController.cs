using UnityEngine;

namespace ShootEmUp
{
    [RequireComponent(typeof(MoveComponent))]
    [RequireComponent(typeof(ShootComponent))]
    [RequireComponent(typeof(HitPointsComponent))]
    public sealed class CharacterController : MonoBehaviour, IGameStartListener, IGamePauseListener, IGameResumeListener, IGameFinishListener
    {
        [SerializeField] private GameObject character;
        [SerializeField] private GameManager gameManager;
        [SerializeField] private InputManager inputManager;
        private MoveComponent moveComponent => character.GetComponent<MoveComponent>();
        private ShootComponent shootComponent => character.GetComponent<ShootComponent>();
        private HitPointsComponent hitPointsComponent => character.GetComponent<HitPointsComponent>();

        private bool isActive;
        
        private void OnCharacterDeath(GameObject _)
        {
            this.gameManager.FinishGame();
        }

        private void OnFire()
        {
            if (isActive)
            {
                var weapon = this.character.GetComponent<WeaponComponent>();
                this.shootComponent.Fire(weapon);
            }
        }

        private void Move(float direction)
        {
            if (isActive)
            {
                this.moveComponent.MoveByRigidbodyVelocity(new Vector2(direction, 0) * Time.fixedDeltaTime);
            }
        }

        void IGameStartListener.StartGame()
        {
            isActive = true;
            this.hitPointsComponent.OnHealthEmpted += this.OnCharacterDeath;
            this.inputManager.OnHorizontalMovement += Move;
            this.inputManager.OnFirePressed += OnFire;
        }

        void IGamePauseListener.PauseGame()
        {
            isActive = false;
        }

        void IGameResumeListener.ResumeGame()
        {
            isActive = true;
        }

        void IGameFinishListener.FinishGame()
        {
            isActive = false;
            this.hitPointsComponent.OnHealthEmpted -= this.OnCharacterDeath;
            this.inputManager.OnHorizontalMovement -= Move;
            this.inputManager.OnFirePressed -= OnFire;
        }
    }
}