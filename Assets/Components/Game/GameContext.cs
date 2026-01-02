public class GameContext
{
    public Ball Ball { get; }
    public Paddle TopPaddle { get; }
    public Paddle BottomPaddle { get; }
    public UIController UIController { get; }
    public CollisionDetector CollisionDetector { get; }
    public int Score { get; set; }

    public GameContext(Ball ball, Paddle topPaddle, Paddle bottomPaddle, UIController uiController, CollisionDetector collisionDetector)
    {
        Ball = ball;
        TopPaddle = topPaddle;
        BottomPaddle = bottomPaddle;
        UIController = uiController;
        CollisionDetector = collisionDetector;
        Score = 0;
    }
}
