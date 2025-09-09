using UnityEngine;

public class GameInstaller : MonoBehaviour
{
    void Start()
    {
        var listeners = GetComponentsInChildren<IGameEventLister>();
        var gameCycle = GetComponent<GameCycle>();

        foreach (var listener in listeners)
        {
            Debug.Log(111);
            gameCycle.AddListener(listener);
        }
    }
}
