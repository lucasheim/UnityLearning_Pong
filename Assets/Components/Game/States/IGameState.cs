public interface IGameState
{
    StateType StateType { get; }

    void Enter(GameContext context);
    void Update(GameContext context);
    void Exit(GameContext context);
}
