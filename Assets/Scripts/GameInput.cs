using UnityEngine;

public class GameInput : MonoBehaviour
{
    private PlayerInput Input;
    public static GameInput Instance { get; private set; }

    // События для ввода
    public System.Action OnShootPressed;
    public System.Action OnJumpPressed;

    private void Awake()
    {
        Instance = this;
        Input = new PlayerInput();
        Input.Enable();

        // Подписка на события ввода
        Input.Player.Shoot.performed += ctx => OnShootPressed?.Invoke();
        Input.Player.Jump.performed += ctx => OnJumpPressed?.Invoke();
    }

    public Vector2 GetMovementVector()
    {
        return Input.Player.Movement.ReadValue<Vector2>();
    }

    public bool IsShooting()
    {
        return Input.Player.Shoot.ReadValue<float>() > 0;
    }

    public bool IsJumping()
    {
        return Input.Player.Jump.ReadValue<float>() > 0;
    }

    private void OnDestroy()
    {
        Input.Disable();
    }
}