using UnityEngine;

public struct PaddleEdges
{
    public float leftEdge;
    public float rightEdge;

    public PaddleEdges(float leftEdge, float rightEdge)
    {
        this.leftEdge = leftEdge;
        this.rightEdge = rightEdge;
    }
}

public class Paddle : MonoBehaviour
{
    private IInputProvider inputProvider;

    [SerializeField, Min(1f)]
    float speed = 10f;

    float
        lowerMovementBoundary,
        upperMovementBoundary;

    public void Initialize(IInputProvider input)
    {
        inputProvider = input;
    }

    public void SetMovementBoundaries(float lowerBoundary, float upperBoundary)
    {
        float halfPaddleSize = transform.localScale.x / 2;
        lowerMovementBoundary = lowerBoundary + halfPaddleSize;
        upperMovementBoundary = upperBoundary - halfPaddleSize;
    }

    void Update()
    {
        if (inputProvider == null) return;

        float horizontalInput = inputProvider.GetHorizontalAxis();
        float movementAmount = horizontalInput * speed * Time.deltaTime;

        Vector3 newPosition = transform.position;
        newPosition.x = Mathf.Clamp(newPosition.x + movementAmount, lowerMovementBoundary, upperMovementBoundary);
        transform.position = newPosition;
    }

    public PaddleEdges GetEdges()
    {
        float radius = transform.localScale.x / 2;
        return new PaddleEdges(transform.position.x - radius, transform.position.x + radius);
    }
}
