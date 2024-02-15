using UnityEngine;

public class AnimatorHandler : MonoBehaviour
{
    Animator animator;
    public void Intialize()
    {
        animator = GetComponent<Animator>();
    }
    public void UpdateAnimatorValues(float movementX, float movementY)
    {
        animator.SetFloat("Horizontal", movementX, 0.1f, Time.deltaTime);
        animator.SetFloat("Vertical", movementY, 0.1f, Time.deltaTime);
    }
}
