using UnityEngine;

public class GameInstaller : MonoBehaviour
{
    [SerializeField] private GameCycle gameCycle;

    void Start()
    {
        var listeners = GetComponentsInChildren<IGameListener>();
        foreach (var listener in listeners)
        {
            gameCycle.AddListener(listener);
        }
    }
}
