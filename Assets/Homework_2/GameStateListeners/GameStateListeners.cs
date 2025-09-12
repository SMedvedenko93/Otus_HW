public interface IGameListener
{

}

public interface IGameStartListener : IGameListener
{
    public void StartGame();
}

public interface IGamePauseListener : IGameListener
{
    public void PauseGame();
}
public interface IGameResumeListener : IGameListener
{
    public void ResumeGame();
}
public interface IGameFinishListener : IGameListener
{
    public void FinishGame();
}

public interface IGameUpdateListener : IGameListener
{
    public void CustomUpdate(float deltaTime);
}

public interface IGameFixedUpdateListener : IGameListener
{
    public void CustomFixedUpdate(float fixedDeltaTime);
}

public interface IGameLateUpdateListener : IGameListener
{
    public void CustomLateUpdate(float deltaTime);
}
