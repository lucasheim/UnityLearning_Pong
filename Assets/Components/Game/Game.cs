using UnityEngine;

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
        ball.CheckCollision();
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
