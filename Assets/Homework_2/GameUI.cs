using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour, IGameStartListener, IGameFinishListener
{

    [SerializeField] private GameCycle gameCycle;

    [SerializeField] private Button startButton;
    [SerializeField] private Button pauseButton;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI pauseButtonText;
    [SerializeField] private int timerDelay;

    [SerializeField] private string pauseButtonValue;
    [SerializeField] private string resumeButtonValue;

    private Coroutine timerCoroutine;

    private void OnEnable()
    {
        startButton.onClick.AddListener(OnPlayButtonClick);
        pauseButton.onClick.AddListener(OnPauseButtonClick);
        pauseButtonText.text = pauseButtonValue;
    }

    private void OnDestroy()
    {
        startButton.onClick.RemoveAllListeners();
        pauseButton.onClick.RemoveAllListeners();
    }

    private void OnPlayButtonClick()
    {
        timerCoroutine = StartCoroutine(StartTimer());
    }

    private IEnumerator StartTimer()
    {
        startButton.gameObject.SetActive(false);
        int remainingTime = timerDelay;

        while (remainingTime > 0)
        {
            timerText.text = remainingTime.ToString();
            yield return new WaitForSeconds(1f);
            remainingTime--;
        }

        timerText.gameObject.SetActive(false);
        pauseButton.gameObject.SetActive(true);
        gameCycle.StartGame();
    }

    private void OnPauseButtonClick()
    {
        if(gameCycle.GameState == GameState.Playing)
        {
            gameCycle.PauseGame();
            pauseButtonText.text = resumeButtonValue;
        } 
        else
        {
            gameCycle.ResumeGame();
            pauseButtonText.text = pauseButtonValue;
        }
    }



    void IGameStartListener.StartGame()
    {
        gameObject.SetActive(true);
    }

    void IGameFinishListener.FinishGame()
    {
        gameObject.SetActive(false);
    }
}
