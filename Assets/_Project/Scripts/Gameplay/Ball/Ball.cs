using UnityEngine;

public class Ball : MonoBehaviour
{
    private const float MIN_LAUNCH_ANGLE = 0.3f;
    private const float MAX_LAUNCH_ANGLE = 0.6f;

    [SerializeField, Min(2f)]
    private float speed = 10f;

    private Vector2 velocity;
    private float topBoundary;
    private float bottomBoundary;
    private float leftBoundary;
    private float rightBoundary;

    void Awake()
    {
        ResetPosition();
    }

    void Update()
    {
        transform.position += (Vector3)velocity * Time.deltaTime;
    }

    public void SetMovementBoundaries(float top, float bottom, float left, float right)
    {
        float halfBallSize = transform.localScale.x / 2;
        topBoundary = top - halfBallSize;
        rightBoundary = right - halfBallSize;
        bottomBoundary = bottom + halfBallSize;
        leftBoundary = left + halfBallSize;
    }

    public Vector3 Position => transform.position;

    public void BounceOffPaddle(float contactOffset)
    {
        float ballY = Position.y;
        float ballZ = Position.z;
        float newYPosition = ballY >= topBoundary ? topBoundary : bottomBoundary;

        velocity.y = -velocity.y;
        velocity.x = contactOffset * speed;
        velocity = velocity.normalized * speed;

        transform.position = new Vector3(Position.x, newYPosition, ballZ);
    }

    public void BounceOffWall()
    {
        float ballX = Position.x;
        float ballY = Position.y;
        float ballZ = Position.z;
        float newXPosition = ballX >= rightBoundary ? rightBoundary : leftBoundary;

        velocity.x = -velocity.x;
        transform.position = new Vector3(newXPosition, ballY, ballZ);
    }

    public void ResetPosition()
    {
        transform.position = Vector3.zero;
        float angle = Random.Range(MIN_LAUNCH_ANGLE, MAX_LAUNCH_ANGLE);
        velocity = new Vector2(speed, speed * -angle);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
