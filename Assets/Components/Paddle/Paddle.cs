using UnityEngine;

public struct PaddleEdge
{
    public float leftEdge;
    public float rightEdge;

    public PaddleEdge(float leftEdge, float rightEdge)
    {
        this.leftEdge = leftEdge;
        this.rightEdge = rightEdge;
    }
}

public class Paddle : MonoBehaviour
{
    // TODO: How to decouple this from the paddle itself?
    InputSystem_Actions inputActions;

    [SerializeField, Min(1f)]
    float speed = 10f;

    float
        lowerMovementBoundary,
        upperMovementBoundary;

    void Awake()
    {
        inputActions = new InputSystem_Actions();
    }

    void OnEnable()
    {
        inputActions.Player.Enable();
    }

    void OnDisable()
    {
        inputActions.Player.Disable();
    }

    public void SetMovementBoundaries(float lowerBoundary, float upperBoundary)
    {
        float halfPaddleSize = transform.localScale.x / 2;
        lowerMovementBoundary = lowerBoundary + halfPaddleSize;
        upperMovementBoundary = upperBoundary - halfPaddleSize;
    }

    void Update()
    {
        Vector2 moveValue = inputActions.Player.Move.ReadValue<Vector2>();
        float movementAmount = moveValue.x * speed * Time.deltaTime;
        
        Vector3 newPosition = transform.position;
        newPosition.x = Mathf.Clamp(newPosition.x + movementAmount, lowerMovementBoundary, upperMovementBoundary);
        transform.position = newPosition;
    }

    public PaddleEdge GetEdges()
    {
        float radius = transform.localScale.x / 2;
        return new PaddleEdge(transform.position.x - radius, transform.position.x + radius);
    }
}
