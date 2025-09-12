using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class LevelBackground : MonoBehaviour, IGameStartListener, IGamePauseListener, IGameResumeListener, IGameFixedUpdateListener
    {
        private float startPositionY;
        private float endPositionY;
        private float movingSpeedY;
        private float positionX;
        private float positionZ;
        private Transform myTransform;
        private bool isActive;

        [SerializeField] private Params m_params;

        public void CustomFixedUpdate(float fixedDeltaTime)
        {
            if (!isActive)
                return;

            if (this.myTransform.position.y <= this.endPositionY)
            {
                this.myTransform.position = new Vector3(
                    this.positionX,
                    this.startPositionY,
                    this.positionZ
                );
            }

            this.myTransform.position -= new Vector3(
                this.positionX,
                this.movingSpeedY * fixedDeltaTime,
                this.positionZ
            );
        }

        public void PauseGame()
        {
            isActive = false;
        }

        public void ResumeGame()
        {
            isActive = true;
        }

        public void StartGame()
        {
            this.startPositionY = this.m_params.m_startPositionY;
            this.endPositionY = this.m_params.m_endPositionY;
            this.movingSpeedY = this.m_params.m_movingSpeedY;
            this.myTransform = this.transform;
            var position = this.myTransform.position;
            this.positionX = position.x;
            this.positionZ = position.z;
            isActive = true;
        }


        [Serializable]
        public sealed class Params
        {
            [SerializeField]
            public float m_startPositionY;

            [SerializeField]
            public float m_endPositionY;

            [SerializeField]
            public float m_movingSpeedY;
        }
    }
}