using System.Collections;
using UnityEngine;

public class CountdownState : BaseGameState
{
    private Coroutine countdownCoroutine;

    public override StateType StateType => StateType.Countdown;

    public CountdownState(
        ICoroutineRunner coroutineRunner,
        IStateTransitionRequester transitionRequester)
        : base(coroutineRunner, transitionRequester)
    {
    }

    public override void Enter(GameContext context)
    {
        context.UIController.ShowCountdownTick(3);
        countdownCoroutine = coroutineRunner.StartManagedCoroutine(ExecuteCountdown(context));
    }

    private IEnumerator ExecuteCountdown(GameContext context)
    {
        yield return new WaitForSeconds(1);
        context.UIController.ShowCountdownTick(2);

        yield return new WaitForSeconds(1);
        context.UIController.ShowCountdownTick(1);

        yield return new WaitForSeconds(1);

        transitionRequester.RequestTransition(StateType.Playing);
    }

    public override void Update(GameContext context)
    {
        // No frame logic for this state
    }

    public override void Exit(GameContext context)
    {
        context.UIController.HideCountdownTick();
        coroutineRunner.StopManagedCoroutine(countdownCoroutine);
    }
}
