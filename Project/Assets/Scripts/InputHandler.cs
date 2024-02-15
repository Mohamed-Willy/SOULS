using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [HideInInspector] public float moveAmount;
    [HideInInspector] public Vector2 movementInput;
    [HideInInspector] public Vector2 cameraMovement;
    PlayerControls inputActions;
    private void OnEnable()
    {
        if (inputActions == null)
        {
            inputActions = new PlayerControls();
            inputActions.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
            inputActions.PlayerMovement.Camera.performed += i => cameraMovement = i.ReadValue<Vector2>();
        }
        inputActions.Enable();
    }
    private void OnDisable()
    {
        inputActions.Disable();
    }
    public void TickInput(float delta)
    {
        MoveInput(delta);
    }
    private void MoveInput(float delta)
    {
        moveAmount = Mathf.Clamp01(Mathf.Abs(movementInput.x) + Mathf.Abs(movementInput.y));
    }

}
