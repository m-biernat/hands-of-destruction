using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Animator), typeof(NetworkAnimator))]
public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;
    private NetworkAnimator networkAnimator;

    private float xAxisMovement = 0f;
    private float zAxisMovement = 0f;

    void Start ()
    {
        animator = GetComponent<Animator>();
        networkAnimator = GetComponent<NetworkAnimator>();
	}

    void Update()
    {
        if (GameManager.instance.lockControll || PauseMenu.IsActive)
        {
            SetMovementDirection(0f, 0f);
            return;
        }

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

    public void Trigger(string state)
    {
        networkAnimator.SetTrigger(state);
        animator.ResetTrigger(state);
    }

    public void SetBlock(bool isBlocking)
    {
        animator.SetBool("isBlocking", isBlocking);
    }

    public bool HasAnimationsEnded()
    {
        return !(animator.GetCurrentAnimatorStateInfo(1).IsName("MainAttack") 
         || animator.GetCurrentAnimatorStateInfo(1).IsName("SpecialAttack"));
    }

}
