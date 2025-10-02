using UnityEngine;
using Zenject;

public class GameBootstrapper : MonoBehaviour, IInitializable
{
    public void Initialize()
    {
        //UnityEngine.Debug.Log("GameBootstrapper");
        //_saveSystem.Load();
        //_uiSystem.Init();
        //_eventBus.Publish(new GameInitializedEvent());
    }
}