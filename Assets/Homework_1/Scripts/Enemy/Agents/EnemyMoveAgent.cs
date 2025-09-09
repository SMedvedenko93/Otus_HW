using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyMoveAgent : MonoBehaviour, IStartGameListener, IPauseGameListener, IResumeGameListener, IFinishGameListener
    {
        public bool IsReached
        {
            get { return this.isReached; }
        }

        [SerializeField] private float magnitute;
        [SerializeField] private MoveComponent moveComponent;
        private Vector2 destination;
        private bool isReached;
        private bool isActive;

        public void SetDestination(Vector2 endPoint)
        {
            this.destination = endPoint;
            this.isReached = false;
        }

        private void FixedUpdate()
        {
            if (this.isReached)
            {
                return;
            }

            if (isActive)
            {
                var vector = this.destination - (Vector2)this.transform.position;
                if (vector.magnitude <= magnitute)
                {
                    this.isReached = true;
                    return;
                }

                var direction = vector.normalized * Time.fixedDeltaTime;
                this.moveComponent.MoveByRigidbodyVelocity(direction);
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