using System.Collections;
using UnityEngine;

public class GameOverState : BaseGameState
{
    private Coroutine gameOverCoroutine;

    public override StateType StateType => StateType.GameOver;

    public GameOverState(
        ICoroutineRunner coroutineRunner,
        IStateTransitionRequester transitionRequester)
        : base(coroutineRunner, transitionRequester)
    {
    }

    public override void Enter(GameContext context)
    {
        context.Ball.Hide();
        context.UIController.ShowGameOver(context.Score);
        gameOverCoroutine = coroutineRunner.StartManagedCoroutine(ExecuteGameOver());
    }

    private IEnumerator ExecuteGameOver()
    {
        yield return new WaitForSeconds(3);
        transitionRequester.RequestTransition(StateType.Countdown);
    }

    public override void Update(GameContext context)
    {
        // No frame logic for this state
    }

    public override void Exit(GameContext context)
    {
        context.UIController.HideGameOver();
        coroutineRunner.StopManagedCoroutine(gameOverCoroutine);
    }
}
