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

        BallActions action = context.Ball.CheckCollision(topEdges, bottomEdges);

        switch (action)
        {
            case BallActions.None:
                return;

            case BallActions.PaddleTouch:
                context.Score++;
                context.UIController.UpdateScore(context.Score);
                break;

            case BallActions.Goal:
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
