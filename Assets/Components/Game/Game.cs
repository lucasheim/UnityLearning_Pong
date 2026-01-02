using System.Collections;
using TMPro;
using UnityEngine;

public enum BallActions
{
    None = 0,
    Goal = 1,
    PaddleTouch = 2
}

public enum GameStates
{
    Countdown = 0,
    Playing = 1,
    GameOver = 2,
}

public class Game : MonoBehaviour
{
    [SerializeField]
    Ball ball;

    [SerializeField]
    Wall
        topWall,
        bottomWall,
        leftWall,
        rightWall;

    [SerializeField]
    Paddle 
        topPaddle,
        bottomPaddle;

    [SerializeField]
    TextMeshPro 
        scoreText,
        gameOverText,
        gameOverPointsText,
        countdownText;

    // TODO: review the variables and protected/public 
    float playerScore = 0;
    GameStates currentState = GameStates.Countdown;

    private readonly WaitForSeconds waitOneSecond = new WaitForSeconds(1);
    private readonly WaitForSeconds waitThreeSeconds = new WaitForSeconds(3);

    void Awake()
    {
        float topBoundary = CalculateVerticalBoundary(topWall);
        float bottomBoundary = CalculateVerticalBoundary(bottomWall);
        float leftBoundary = CalculateHorizontalBoundary(leftWall);
        float rightBoundary = CalculateHorizontalBoundary(rightWall);

        ball.SetMovementBoundaries(topBoundary, bottomBoundary, leftBoundary, rightBoundary);
        ball.gameObject.SetActive(false);

        topPaddle.SetMovementBoundaries(leftBoundary, rightBoundary);
        bottomPaddle.SetMovementBoundaries(leftBoundary, rightBoundary);
        ChangeState(GameStates.Countdown);
    }

    void Update()
    {
        if (currentState == GameStates.Playing)
        {
            ball.gameObject.SetActive(true);
            PaddleEdge topPaddleEdges = topPaddle.GetEdges();
            PaddleEdge bottomPaddleEdges = bottomPaddle.GetEdges();
        
            switch(ball.CheckCollision(topPaddleEdges, bottomPaddleEdges))
            {
                case BallActions.None:
                    return;
                case BallActions.PaddleTouch:
                    playerScore++;
                    scoreText.SetText("{0}", playerScore);
                    break;
                case BallActions.Goal:
                    ChangeState(GameStates.GameOver);
                    break;
            }
        }
    }

    private float CalculateVerticalBoundary(Wall wall)
    {
        return CalculateBoundary(wall.transform.position.y, wall.transform.localScale.y);
    }

    private float CalculateHorizontalBoundary(Wall wall)
    {
        return CalculateBoundary(wall.transform.position.x, wall.transform.localScale.x);
    }

    private float CalculateBoundary(float position, float scale)
    {
        return position - (Mathf.Sign(position) * (scale / 2));
    }

    private void ChangeState(GameStates newState)
    {
        currentState = newState;

        if (newState == GameStates.Countdown)
        {
            countdownText.gameObject.SetActive(true);
            countdownText.SetText("Starting in...");
            scoreText.gameObject.SetActive(true);
            scoreText.SetText("3");
            StartCoroutine(ExecuteCountdown());
        }
        else if (newState == GameStates.Playing)
        {
            scoreText.SetText("0");
            ball.ResetBall();
            ball.gameObject.SetActive(true);
        }
        else if (newState == GameStates.GameOver)
        {
            ball.gameObject.SetActive(false);
            gameOverText.SetText("Game Over");
            gameOverText.gameObject.SetActive(true);
            gameOverPointsText.SetText("{0} points", playerScore);
            playerScore = 0;
            gameOverPointsText.gameObject.SetActive(true);
            scoreText.gameObject.SetActive(false);
            StartCoroutine(ExecuteGameOver());
        }
    }

     private IEnumerator ExecuteCountdown()
    {
        //Print the time of when the function is first called.
        Debug.Log("Started Countdown Coroutine at timestamp : " + Time.time);

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return waitOneSecond;

        scoreText.SetText("2");

        yield return waitOneSecond;

        scoreText.SetText("1");

        yield return waitOneSecond;

        gameOverText.gameObject.SetActive(false);

        //After we have waited 5 seconds print the time again.
        Debug.Log("Finished Countdown Coroutine at timestamp : " + Time.time);

        ChangeState(GameStates.Playing);
    }

    private IEnumerator ExecuteGameOver()
    {
        //Print the time of when the function is first called.
        Debug.Log("Started Gameover Coroutine at timestamp : " + Time.time);

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return waitThreeSeconds;

        //After we have waited 5 seconds print the time again.
        Debug.Log("Finished Gameover Coroutine at timestamp : " + Time.time);

        gameOverText.gameObject.SetActive(false);
        gameOverPointsText.gameObject.SetActive(false);
        ChangeState(GameStates.Countdown);
    }
}
