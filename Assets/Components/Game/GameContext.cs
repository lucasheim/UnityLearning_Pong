public class GameContext
{
    public Ball Ball { get; }
    public Paddle TopPaddle { get; }
    public Paddle BottomPaddle { get; }
    public UIController UIController { get; }
    public int Score { get; set; }

    public GameContext(Ball ball, Paddle topPaddle, Paddle bottomPaddle, UIController uiController)
    {
        Ball = ball;
        TopPaddle = topPaddle;
        BottomPaddle = bottomPaddle;
        UIController = uiController;
        Score = 0;
    }
}
