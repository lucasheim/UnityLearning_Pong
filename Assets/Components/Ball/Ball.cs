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
        ResetPosition();
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
    public BallActions CheckCollision(PaddleEdges topPaddleEdges, PaddleEdges bottomPaddleEdges)
    {
        float ballX = transform.position.x;
        float ballY = transform.position.y;
        float ballZ = transform.position.z;

        if (ballY >= topBoundary)
        {
            if (TouchesPaddle(ballX, topPaddleEdges))
            {
                float offset = GetTouchOffset(ballX, topPaddleEdges);
                speedVec.y = -speedVec.y;
                speedVec.x = offset * speed;
                speedVec = speedVec.normalized * speed;
                transform.position = new Vector3(ballX, topBoundary, ballZ);
                return BallActions.PaddleTouch;    
            } 
            else
            {
                return BallActions.Goal;    
            }
        }
        else if (ballY <= bottomBoundary)
        {
            if (TouchesPaddle(ballX, bottomPaddleEdges))
            {
                float offset = GetTouchOffset(ballX, bottomPaddleEdges);
                speedVec.y = -speedVec.y;
                speedVec.x = offset * speed;
                speedVec = speedVec.normalized * speed;
                transform.position = new Vector3(ballX, bottomBoundary, ballZ);
                return BallActions.PaddleTouch;
            }
            else
            {
                return BallActions.Goal;
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

    public void ResetPosition()
    {
        transform.position = new Vector3(0, 0, 0);
        float newBallAngle = Random.Range(0.3f, 0.6f);
        speedVec = new Vector2(speed, speed * -newBallAngle);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private bool TouchesPaddle(float value, PaddleEdges paddleEdge)
    {
        return value >= paddleEdge.leftEdge && value <= paddleEdge.rightEdge;
    }

    private float GetTouchOffset(float ballX, PaddleEdges paddleEdge)
    {
        float contactOffset = ballX - paddleEdge.leftEdge;
        float fullPaddleWidth = paddleEdge.rightEdge - paddleEdge.leftEdge;
        return 2f * (contactOffset / fullPaddleWidth) - 1f;
    }
}
