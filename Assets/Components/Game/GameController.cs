using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private Ball ball;

    [SerializeField]
    private Wall 
        topWall, 
        bottomWall, 
        leftWall, 
        rightWall;

    [SerializeField]
    private Paddle
        topPaddle,
        bottomPaddle;

    [SerializeField]
    private GameStateMachine stateMachine;

    [SerializeField]
    private UIController uiController;

    private GameContext gameContext;

    private void Awake()
    {
        // Calculate boundaries for ball and paddle movement
        float topBoundary = topWall.CalculateVerticalBoundary();
        float bottomBoundary = bottomWall.CalculateVerticalBoundary();
        float leftBoundary = leftWall.CalculateHorizontalBoundary();
        float rightBoundary = rightWall.CalculateHorizontalBoundary();

        ball.SetMovementBoundaries(topBoundary, bottomBoundary, leftBoundary, rightBoundary);
        ball.Hide();

        IInputProvider playerInput = new PlayerInputProvider();

        topPaddle.Initialize(playerInput);
        topPaddle.SetMovementBoundaries(leftBoundary, rightBoundary);

        bottomPaddle.Initialize(playerInput);
        bottomPaddle.SetMovementBoundaries(leftBoundary, rightBoundary);

        InitializeStateMachine();
    }

    private void InitializeStateMachine()
    {
        float topBoundary = topWall.CalculateVerticalBoundary();
        float bottomBoundary = bottomWall.CalculateVerticalBoundary();
        float leftBoundary = leftWall.CalculateHorizontalBoundary();
        float rightBoundary = rightWall.CalculateHorizontalBoundary();

        float halfBallSize = ball.transform.localScale.x / 2;
        CollisionDetector collisionDetector = new CollisionDetector(
            topBoundary - halfBallSize,
            bottomBoundary + halfBallSize,
            leftBoundary + halfBallSize,
            rightBoundary - halfBallSize
        );

        gameContext = new GameContext(ball, topPaddle, bottomPaddle, uiController, collisionDetector);
        stateMachine.Initialize(gameContext);
    }
}
