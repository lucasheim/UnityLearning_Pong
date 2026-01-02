using UnityEngine;

public class CollisionDetector
{
    private readonly float topBoundary;
    private readonly float bottomBoundary;
    private readonly float leftBoundary;
    private readonly float rightBoundary;

    public CollisionDetector(float top, float bottom, float left, float right)
    {
        topBoundary = top;
        bottomBoundary = bottom;
        leftBoundary = left;
        rightBoundary = right;
    }

    public BallCollisionResult CheckCollision(Vector3 ballPosition, PaddleEdges topPaddle, PaddleEdges bottomPaddle)
    {
        float ballX = ballPosition.x;
        float ballY = ballPosition.y;

        if (ballY >= topBoundary)
        {
            if (IsBallTouchingPaddle(ballX, topPaddle))
            {
                float offset = CalculatePaddleContactOffset(ballX, topPaddle);
                return BallCollisionResult.PaddleHit(offset);
            }
            else
            {
                return BallCollisionResult.Goal();
            }
        }
        else if (ballY <= bottomBoundary)
        {
            if (IsBallTouchingPaddle(ballX, bottomPaddle))
            {
                float offset = CalculatePaddleContactOffset(ballX, bottomPaddle);
                return BallCollisionResult.PaddleHit(offset);
            }
            else
            {
                return BallCollisionResult.Goal();
            }
        }

        if (ballX >= rightBoundary || ballX <= leftBoundary)
        {
            return BallCollisionResult.WallHit();
        }

        return BallCollisionResult.None();
    }

    private bool IsBallTouchingPaddle(float ballX, PaddleEdges paddle)
    {
        return ballX >= paddle.leftEdge && ballX <= paddle.rightEdge;
    }

    private float CalculatePaddleContactOffset(float ballX, PaddleEdges paddle)
    {
        float contactOffset = ballX - paddle.leftEdge;
        float fullPaddleWidth = paddle.rightEdge - paddle.leftEdge;
        return 2f * (contactOffset / fullPaddleWidth) - 1f;
    }
}
