using UnityEngine;

public class Movement : MonoBehaviour
{
    Transform cameraObject;
    InputHandler inputHandler;
    Vector3 moveDirection;

    Transform _transform;
    Rigidbody _rb;
    AnimatorHandler animatorHandler;

    [SerializeField] float moveSpeed = 5;
    [SerializeField] float rotationSpeed = 10;
    
    void Start()
    {
        animatorHandler = GetComponentInChildren<AnimatorHandler>();
        animatorHandler.Intialize();
        _rb = GetComponent<Rigidbody>();
        inputHandler = GetComponent<InputHandler>();
        cameraObject = Camera.main.transform;
        _transform = transform;
    }

    public void Update()
    {
        float delta = Time.deltaTime;

        inputHandler.TickInput(delta);

        moveDirection = cameraObject.forward * inputHandler.movementInput.y;
        moveDirection += cameraObject.right * inputHandler.movementInput.x;
        moveDirection.Normalize();

        float speed = moveSpeed;
        moveDirection *= speed;

        _rb.velocity = Vector3.ProjectOnPlane(moveDirection, normalVector);

        animatorHandler.UpdateAnimatorValues(0, inputHandler.moveAmount);
        HandleRotation(delta);
    }

    #region Rotation
    Vector3 normalVector;
    private void HandleRotation(float delta)
    {
        Vector3 targetDir = cameraObject.forward * inputHandler.movementInput.y;
        targetDir += cameraObject.right * inputHandler.movementInput.x;

        targetDir.Normalize();
        targetDir.y = 0;

        if(targetDir == Vector3.zero)
        {
            targetDir = _transform.forward;
        }

        Quaternion tr = Quaternion.LookRotation(targetDir);
        Quaternion targetRotation = Quaternion.Slerp(_transform.rotation, tr, rotationSpeed * delta);
        _transform.rotation = targetRotation;
    }
    #endregion
}
