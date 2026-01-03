using UnityEngine;

public class PlayingState : BaseGameState
{
    public override StateType StateType => StateType.Playing;

    public PlayingState(
        ICoroutineRunner coroutineRunner,
        IStateTransitionRequester transitionRequester)
        : base(coroutineRunner, transitionRequester)
    {
    }

    public override void Enter(GameContext context)
    {
        context.Score = 0;
        context.UIController.UpdateScore(0);
        context.Ball.ResetPosition();
        context.Ball.Show();
    }

    public override void Update(GameContext context)
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
                transitionRequester.RequestTransition(StateType.GameOver);
                break;
        }
    }

    public override void Exit(GameContext context)
    {
        context.Ball.Hide();
    }
}
