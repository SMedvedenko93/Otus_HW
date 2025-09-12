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

    private List<IGameUpdateListener> gameUpdateListeners = new();
    private List<IGameFixedUpdateListener> gameFixedUpdateListeners = new();
    private List<IGameLateUpdateListener> gameLateUpdateListeners = new();

    private List<IGameStartListener> gameStartListeners = new();
    private List<IGamePauseListener> gamePauseListeners = new();
    private List<IGameResumeListener> gameResumeListeners = new();
    private List<IGameFinishListener> gameFinishListeners = new();
     
    public GameState GameState => _gameState;

    public void AddListener(IGameListener listener)
    {
        if(listener is IGameStartListener gameStartListener)
            gameStartListeners.Add(gameStartListener);

        if (listener is IGamePauseListener gamePauseListener)
            gamePauseListeners.Add(gamePauseListener);

        if (listener is IGameResumeListener gameResumeListener)
            gameResumeListeners.Add(gameResumeListener);

        if (listener is IGameFinishListener gameFinishListener)
            gameFinishListeners.Add(gameFinishListener);

        if (listener is IGameUpdateListener gameUpdateListener)
            gameUpdateListeners.Add(gameUpdateListener);

        if (listener is IGameFixedUpdateListener gameFixedUpdateListener)
            gameFixedUpdateListeners.Add(gameFixedUpdateListener);

        if (listener is IGameLateUpdateListener gameLateUpdateListener)
            gameLateUpdateListeners.Add(gameLateUpdateListener);
    }

    public void StartGame()
    {
        if (_gameState == GameState.Playing)
            return;

        _gameState = GameState.Playing;

        foreach (IGameStartListener listener in gameStartListeners)
        {
            listener.StartGame();
        }
    }

    public void PauseGame()
    {
        if (_gameState == GameState.Paused)
            return;

        _gameState = GameState.Paused;

        foreach (IGamePauseListener listener in gamePauseListeners)
        {
            listener.PauseGame();
        }
    }

    public void ResumeGame()
    {
        if (_gameState != GameState.Paused)
            return;

        _gameState = GameState.Playing;

        foreach (IGameResumeListener listener in gameResumeListeners)
        {
            listener.ResumeGame();
        }
    }

    public void FinishGame()
    {
        if (_gameState == GameState.Finished)
            return;

        _gameState = GameState.Finished;

        foreach (IGameFinishListener listener in gameFinishListeners)
        {
            listener.FinishGame();
        }
    }

    private void Update()
    {
        var deltaTime = Time.deltaTime;
        foreach (IGameUpdateListener listener in gameUpdateListeners)
        {
            listener.CustomUpdate(deltaTime);
        }
    }

    private void FixedUpdate()
    {
        var fixedDeltaTime = Time.fixedDeltaTime;
        foreach (IGameFixedUpdateListener listener in gameFixedUpdateListeners)
        {
            listener.CustomFixedUpdate(fixedDeltaTime);
        }
    }

    private void LateUpdate()
    {
        var deltaTime = Time.deltaTime;
        foreach (IGameLateUpdateListener listener in gameLateUpdateListeners)
        {
            listener.CustomLateUpdate(deltaTime);
        }
    }
}
