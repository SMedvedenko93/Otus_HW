using UnityEngine;

namespace ShootEmUp
{
    [RequireComponent(typeof(MoveComponent))]
    [RequireComponent(typeof(ShootComponent))]
    [RequireComponent(typeof(HitPointsComponent))]
    public sealed class CharacterController : MonoBehaviour, IStartGameListener, IPauseGameListener, IResumeGameListener, IFinishGameListener
    {
        [SerializeField] private GameObject character;
        [SerializeField] private GameManager gameManager;
        [SerializeField] private InputManager inputManager;
        private MoveComponent moveComponent => character.GetComponent<MoveComponent>();
        private ShootComponent shootComponent => character.GetComponent<ShootComponent>();
        private HitPointsComponent hitPointsComponent => character.GetComponent<HitPointsComponent>();

        private bool isActive;
        
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
            if (isActive)
            {
                this.moveComponent.MoveByRigidbodyVelocity(new Vector2(direction, 0) * Time.fixedDeltaTime);
            }
        }

        public void StartGame()
        {
            isActive = true;
        }

        public void PauseGame()
        {
            isActive = false;
        }

        public void ResumeGame()
        {
            isActive = true;
        }

        public void FinishGame()
        {
            isActive = false;
        }
    }
}