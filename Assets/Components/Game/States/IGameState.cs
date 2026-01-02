public interface IGameState
{
    string GetStateName();

    void Enter(GameContext context);
    void Update(GameContext context);
    void Exit(GameContext context);
}
