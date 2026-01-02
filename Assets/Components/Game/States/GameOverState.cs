using System.Collections;
using UnityEngine;
public class GameOverState : IGameState
{
    private readonly GameStateMachine stateMachine;
    private Coroutine gameOverCoroutine;

    public GameOverState(GameStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    public string GetStateName() => "GameOver";

    public void Enter(GameContext context)
    {
        Debug.Log("Entering GameOver State");
        context.Ball.Hide();
        context.UIController.ShowGameOver(context.Score);
        gameOverCoroutine = stateMachine.RunCoroutine(ExecuteGameOver());
    }

    private IEnumerator ExecuteGameOver()
    {
        yield return new WaitForSeconds(3);
        stateMachine.GoToNextState();
    }

    public void Update(GameContext context)
    {
        // No frame logic for this state
    }

    public void Exit(GameContext context)
    {
        Debug.Log("Exiting GameOver State");
        context.UIController.HideGameOver();
        stateMachine.StopRunningCoroutine(gameOverCoroutine);
    }
}
