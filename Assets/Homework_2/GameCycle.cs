using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    None = 0,
    Playing = 1,
    Paused = 2,
    Finished = 3
}

public class GameCycle : MonoBehaviour
{
    private GameState _gameState;

    private List<IStartGameListener> startGameListeners = new();
    private List<IPauseGameListener> pauseGameListeners = new();
    private List<IResumeGameListener> resumeGameListeners = new();
    private List<IFinishGameListener> finishGameListeners = new();

    public void AddListener(IGameEventLister listener)
    {
        if(listener is IStartGameListener startGameListener)
            startGameListeners.Add(startGameListener);

        if (listener is IPauseGameListener pauseGameListener)
            pauseGameListeners.Add(pauseGameListener);

        if (listener is IResumeGameListener resumeGameListener)
            resumeGameListeners.Add(resumeGameListener);

        if (listener is IFinishGameListener finishGameListener)
            finishGameListeners.Add(finishGameListener);
    }

    [ContextMenu("Start Game")]
    public void StartGame()
    {
        if (_gameState == GameState.Playing)
            return;

        _gameState = GameState.Playing;

        foreach (IStartGameListener listener in startGameListeners)
        {
            listener.StartGame();
        }
    }

    [ContextMenu("Pause Game")]
    public void PauseGame()
    {
        if (_gameState == GameState.Paused)
            return;

        _gameState = GameState.Paused;

        foreach (IPauseGameListener listener in pauseGameListeners)
        {
            listener.PauseGame();
        }
    }

    [ContextMenu("Resume Game")]
    public void ResumeGame()
    {
        if (_gameState != GameState.Paused)
            return;

        _gameState = GameState.Playing;


        foreach (IResumeGameListener listener in resumeGameListeners)
        {
            listener.ResumeGame();
        }
    }

    [ContextMenu("Finish Game")]
    public void FinishGame()
    {
        if (_gameState == GameState.Finished)
            return;

        _gameState = GameState.Finished;


        foreach (IFinishGameListener listener in finishGameListeners)
        {
            listener.FinishGame();
        }
    }
}
