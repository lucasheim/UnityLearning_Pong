using UnityEngine;

public class PlayerInputProvider : IInputProvider
{
    private readonly InputSystem_Actions inputActions;

    public PlayerInputProvider()
    {
        inputActions = new InputSystem_Actions();
        inputActions.Player.Enable();
    }

    public float GetHorizontalAxis()
    {
        Vector2 moveValue = inputActions.Player.Move.ReadValue<Vector2>();
        return moveValue.x;
    }

    public void Dispose()
    {
        inputActions.Player.Disable();
        inputActions.Dispose();
    }
}
