using TMPro;
using UnityEngine;

public enum BallActions
{
    None = 0,
    TopPlayerGoal = 1,
    BottomPlayerGoal = 2
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
        topScore, 
        bottomScore;

    float topPlayerScore = 0;
    float bottomPlayerScore = 0;

    void Awake()
    {
        float topBoundary = CalculateVerticalBoundary(topWall);
        float bottomBoundary = CalculateVerticalBoundary(bottomWall);
        float leftBoundary = CalculateHorizontalBoundary(leftWall);
        float rightBoundary = CalculateHorizontalBoundary(rightWall);

        ball.SetMovementBoundaries(topBoundary, bottomBoundary, leftBoundary, rightBoundary);

        topPaddle.SetMovementBoundaries(leftBoundary, rightBoundary);
        bottomPaddle.SetMovementBoundaries(leftBoundary, rightBoundary);
    }

    void Update()
    {
        PaddleEdge topPaddleEdges = topPaddle.GetEdges();
        PaddleEdge bottomPaddleEdges = bottomPaddle.GetEdges();
        
        switch(ball.CheckCollision(topPaddleEdges, bottomPaddleEdges))
        {
            case BallActions.None:
                return;
            case BallActions.TopPlayerGoal:
                topPlayerScore++;
                ball.transform.position = new Vector3(0, 0, 0);
                topScore.SetText("{0}", topPlayerScore);
                break;
            case BallActions.BottomPlayerGoal:
                bottomPlayerScore++;
                ball.transform.position = new Vector3(0, 0, 0);
                bottomScore.SetText("{0}", bottomPlayerScore);
                break;
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
        return position - (scale / 2) * Mathf.Sign(position);
    }
}
