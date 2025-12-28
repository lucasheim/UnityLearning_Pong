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

    public void CheckCollision()
    {
        float ballX = transform.position.x;
        float ballY = transform.position.y;
        float ballZ = transform.position.z;

        if (ballY >= topBoundary)
        {
            speedVec.y = -speedVec.y;
            transform.position = new Vector3(ballX, topBoundary, ballZ);
        }
        if (ballY <= bottomBoundary)
        {
            speedVec.y = -speedVec.y;
            transform.position = new Vector3(ballX, bottomBoundary, ballZ);
        }
        if (ballX >= rightBoundary)
        {
            speedVec.x = -speedVec.x;
            transform.position = new Vector3(rightBoundary, ballY, ballZ);
        }
        if (ballX <= leftBoundary)
        {
            speedVec.x = -speedVec.x;
            transform.position = new Vector3(leftBoundary, ballY, ballZ);
        }
    }
}
