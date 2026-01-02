using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateMachine : MonoBehaviour
{
    private IGameState currentState;
    private GameContext context;
    private readonly Dictionary<IGameState, IGameState> stateMap = new();

    public void Initialize(GameContext context)
    {
        this.context = context;
        IGameState countdownState = new CountdownState(this);
        IGameState playingState = new PlayingState(this);
        IGameState gameOverState = new GameOverState(this);
        stateMap.Add(countdownState, playingState);
        stateMap.Add(playingState, gameOverState);
        stateMap.Add(gameOverState, countdownState);
        
        currentState = countdownState;
        countdownState.Enter(this.context);
    }

    public void GoToNextState()
    {
        currentState?.Exit(context);
        Debug.Log(currentState);
        Debug.Log(stateMap);
        currentState = stateMap[currentState];
        currentState.Enter(context);
    }

    private void Update()
    {
        currentState?.Update(context);
    }

    public Coroutine RunCoroutine(IEnumerator coroutine)
    {
        return StartCoroutine(coroutine);
    }

    public void StopRunningCoroutine(Coroutine coroutine)
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
    }
}
