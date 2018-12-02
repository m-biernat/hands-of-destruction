using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;

    private float xAxisMovement = 0f;
    private float zAxisMovement = 0f;

    void Start ()
    {
        animator = GetComponent<Animator>();
	}

    void Update()
    {
        xAxisMovement = Input.GetAxisRaw("Horizontal");
        zAxisMovement = Input.GetAxisRaw("Vertical");

        SetMovementDirection(xAxisMovement, zAxisMovement);
    }

    private void SetMovementDirection(float xAxisValue, float zAxisValue)
    {
        animator.SetFloat("xAxis", xAxisValue, .1f, Time.deltaTime);
        animator.SetFloat("zAxis", zAxisValue, .1f, Time.deltaTime);
    }

    public void SetMovementState(bool isSprinting, bool isWalking, bool isCrouching)
    {
        animator.SetBool("isSprinting", isSprinting);
        animator.SetBool("isWalking", isWalking);
        animator.SetBool("isCrouching", isCrouching);
    }
}
