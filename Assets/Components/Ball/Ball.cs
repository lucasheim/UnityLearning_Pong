using UnityEngine;


public class Ball : MonoBehaviour
{
    [SerializeField, Min(2f)]
    float speed = 10f;

    Vector2 speedVec;

    float
        topBoundary,
        bottomBoundary,
        leftBoundary,
        rightBoundary;

    void Awake()
    {
        // TODO: Randomize initial position and avoid magic number
        speedVec = new Vector2(speed, speed * 0.3f);
    }

    void Update()
    {
        transform.position += (Vector3)speedVec * Time.deltaTime;
    }

    public void SetMovementBoundaries(float top, float bottom, float left, float right)
    {
        float halfBallSize = transform.localScale.x / 2;
        topBoundary = top - halfBallSize;
        rightBoundary = right - halfBallSize;
        bottomBoundary = bottom + halfBallSize;
        leftBoundary = left + halfBallSize;
    }

    // TODO: should this be at the game level instead of the ball?
    // Maybe the ball just exposes methods to change the internal stuff, but shouldn't get this data from the paddle as it makes it too game-aware
    public BallActions CheckCollision(PaddleEdge topPaddleEdges, PaddleEdge bottomPaddleEdges)
    {
        float ballX = transform.position.x;
        float ballY = transform.position.y;
        float ballZ = transform.position.z;

        if (ballY >= topBoundary)
        {
            if (TouchesPaddle(ballX, topPaddleEdges))
            {
                speedVec.y = -speedVec.y;
                transform.position = new Vector3(ballX, topBoundary, ballZ);
            } 
            else
            {
                return BallActions.BottomPlayerGoal;    
            }
        }
        else if (ballY <= bottomBoundary)
        {
            if (TouchesPaddle(ballX, bottomPaddleEdges))
            {
                speedVec.y = -speedVec.y;
                transform.position = new Vector3(ballX, bottomBoundary, ballZ);
            }
            else
            {
                return BallActions.TopPlayerGoal;
            }
        }

        if (ballX >= rightBoundary)
        {
            speedVec.x = -speedVec.x;
            transform.position = new Vector3(rightBoundary, ballY, ballZ);
        }
        else if (ballX <= leftBoundary)
        {
            speedVec.x = -speedVec.x;
            transform.position = new Vector3(leftBoundary, ballY, ballZ);
        }

        return BallActions.None;
    }

    private bool TouchesPaddle(float value, PaddleEdge paddleEdge)
    {
        return value >= paddleEdge.leftEdge && value <= paddleEdge.rightEdge;
    }
}
