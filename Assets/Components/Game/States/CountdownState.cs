using System.Collections;
using UnityEngine;

public class CountdownState : IGameState
{
    private readonly GameStateMachine stateMachine;
    private Coroutine countdownCoroutine;

    public CountdownState(GameStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    public string GetStateName() => "Countdown";

    public void Enter(GameContext context)
    {
        Debug.Log("Entering Countdown State");
        context.UIController.ShowCountdownTick(3);
        countdownCoroutine = stateMachine.RunCoroutine(ExecuteCountdown(context));
    }

    private IEnumerator ExecuteCountdown(GameContext context)
    {
        yield return new WaitForSeconds(1);
        context.UIController.ShowCountdownTick(2);

        yield return new WaitForSeconds(1);
        context.UIController.ShowCountdownTick(1);

        yield return new WaitForSeconds(1);

        stateMachine.GoToNextState();
    }

    public void Update(GameContext context)
    {
        // No frame logic for this state
    }

    public void Exit(GameContext context)
    {
        Debug.Log("Exiting Countdown State");
        context.UIController.HideCountdownTick();
        stateMachine.StopRunningCoroutine(countdownCoroutine);
    }
}
