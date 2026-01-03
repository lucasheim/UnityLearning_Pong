/// <summary>
/// Optional base class for game states that provides common functionality.
/// Reduces boilerplate and ensures consistent state behavior.
/// </summary>
public abstract class BaseGameState : IGameState
{
    protected readonly ICoroutineRunner coroutineRunner;
    protected readonly IStateTransitionRequester transitionRequester;

    public abstract StateType StateType { get; }

    protected BaseGameState(
        ICoroutineRunner coroutineRunner,
        IStateTransitionRequester transitionRequester)
    {
        this.coroutineRunner = coroutineRunner;
        this.transitionRequester = transitionRequester;
    }

    public abstract void Enter(GameContext context);
    public abstract void Update(GameContext context);
    public abstract void Exit(GameContext context);
}
