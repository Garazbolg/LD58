using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public PlayerController controller;
    public Animator animator;
    
    void Update()
    {
        animator.SetBool("isGrounded", controller.isGrounded);
        animator.SetFloat("YSpeed", controller.rb.linearVelocity.y);
        animator.SetFloat("XSpeed", Mathf.Abs(controller.rb.linearVelocity.x));
        animator.SetBool("isDashing", controller.isDashing);
    }
}