using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateMachine : MonoBehaviour, ICoroutineRunner, IStateTransitionRequester
{
    private IGameState currentState;
    private GameContext context;
    private Dictionary<StateType, IGameState> stateRegistry;

    public void Initialize(GameContext context)
    {
        this.context = context;

        stateRegistry = new Dictionary<StateType, IGameState>
        {
            { StateType.Countdown, new CountdownState(this, this) },
            { StateType.Playing, new PlayingState(this, this) },
            { StateType.GameOver, new GameOverState(this, this) }
        };

        TransitionTo(StateType.Countdown);
    }

    public void RequestTransition(StateType targetState)
    {
        if (!stateRegistry.ContainsKey(targetState))
        {
            Debug.LogError($"State not found in registry: {targetState}");
            return;
        }

        TransitionTo(targetState);
    }

    private void TransitionTo(StateType stateType)
    {
        IGameState newState = stateRegistry[stateType];
        currentState?.Exit(context);
        currentState = newState;
        currentState.Enter(context);
    }

    public Coroutine StartManagedCoroutine(IEnumerator routine)
    {
        return StartCoroutine(routine);
    }

    public void StopManagedCoroutine(Coroutine routine)
    {
        if (routine != null)
        {
            StopCoroutine(routine);
        }
    }

    private void Update()
    {
        currentState?.Update(context);
    }
}
