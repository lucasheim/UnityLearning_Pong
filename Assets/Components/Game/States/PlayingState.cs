using UnityEngine;

public class PlayingState : IGameState
{
    private readonly GameStateMachine stateMachine;

    public string GetStateName() => "Playing";

    public PlayingState(GameStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    public void Enter(GameContext context)
    {
        Debug.Log("Entering Playing State");
        context.Score = 0;
        context.UIController.UpdateScore(0);
        context.Ball.ResetPosition();
        context.Ball.Show();
    }

    public void Update(GameContext context)
    {
        PaddleEdges topEdges = context.TopPaddle.GetEdges();
        PaddleEdges bottomEdges = context.BottomPaddle.GetEdges();

        BallCollisionResult collision = context.CollisionDetector.CheckCollision(
            context.Ball.Position,
            topEdges,
            bottomEdges
        );

        switch (collision.Type)
        {
            case CollisionType.None:
                return;

            case CollisionType.PaddleHit:
                context.Ball.BounceOffPaddle(collision.PaddleContactOffset);
                context.Score++;
                context.UIController.UpdateScore(context.Score);
                break;

            case CollisionType.WallHit:
                context.Ball.BounceOffWall();
                break;

            case CollisionType.Goal:
                stateMachine.GoToNextState();
                break;
        }
    }

    public void Exit(GameContext context)
    {
        Debug.Log("Exiting Playing State");
        context.Ball.Hide();
    }
}
