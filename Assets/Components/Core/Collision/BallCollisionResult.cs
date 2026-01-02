public enum CollisionType
{
    None,
    Goal,
    PaddleHit,
    WallHit
}

public struct BallCollisionResult
{
    public CollisionType Type { get; }
    public float PaddleContactOffset { get; }

    public BallCollisionResult(CollisionType type, float paddleContactOffset = 0f)
    {
        Type = type;
        PaddleContactOffset = paddleContactOffset;
    }

    public static BallCollisionResult None() => new(CollisionType.None);
    public static BallCollisionResult Goal() => new(CollisionType.Goal);
    public static BallCollisionResult WallHit() => new(CollisionType.WallHit);
    public static BallCollisionResult PaddleHit(float contactOffset) => new(CollisionType.PaddleHit, contactOffset);
}
