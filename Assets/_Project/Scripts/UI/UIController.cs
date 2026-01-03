using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private TextMeshPro scoreText;

    [SerializeField]
    private TextMeshPro gameOverText;

    [SerializeField]
    private TextMeshPro gameOverPointsText;

    [SerializeField]
    private TextMeshPro countdownText;

    public void UpdateScore(int newScore)
    {
        scoreText.SetText("{0}", newScore);
    }

    public void ShowCountdownTick(int seconds)
    {
        int START_COUNTDOWN_SECONDS = 3;
        if (seconds == START_COUNTDOWN_SECONDS)
        {
            countdownText.gameObject.SetActive(true);
            scoreText.gameObject.SetActive(true);
        }
        scoreText.SetText("{0}", seconds);
    }

    public void ShowGameOver(int finalScore)
    {
        gameOverText.SetText("Game Over");
        gameOverText.gameObject.SetActive(true);
        gameOverPointsText.SetText("{0} points", finalScore);
        gameOverPointsText.gameObject.SetActive(true);
        scoreText.gameObject.SetActive(false);

    }

    public void HideCountdownTick()
    {
        countdownText.gameObject.SetActive(false);        
    }

    public void HideGameOver()
    {
        gameOverText.gameObject.SetActive(false);
        gameOverPointsText.gameObject.SetActive(false);
    }
}
