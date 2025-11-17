using System;
using UnityEngine;
using Zenject;

namespace ShootEmUpZenject
{
    public class GameManager : IInitializable, IDisposable
    {
        private PlayerSystem _playerSystem;
        private EventBus _eventBus;

        public GameManager(EventBus eventBus)
        {
            _eventBus = eventBus;
        }


        public void FinishGame(PlayerDiedSignal _)
        {
            Debug.Log("Game over!");
            Time.timeScale = 0;
        }

        public void Initialize()
        {
            _eventBus.Subscribe<PlayerDiedSignal>(FinishGame);
        }

        public void Dispose()
        {
            _eventBus.UnSubscribe<PlayerDiedSignal>(FinishGame);
        }
    }
}