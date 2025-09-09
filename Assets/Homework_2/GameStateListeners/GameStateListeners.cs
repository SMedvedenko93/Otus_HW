
public interface IGameEventLister
{

}

public interface IStartGameListener : IGameEventLister
{
    public void StartGame();
}

public interface IPauseGameListener : IGameEventLister
{
    public void PauseGame();
}
public interface IResumeGameListener : IGameEventLister
{
    public void ResumeGame();
}
public interface IFinishGameListener: IGameEventLister
{
    public void FinishGame();
}
