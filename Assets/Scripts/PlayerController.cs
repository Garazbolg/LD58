using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    
    #region Movement
    [Header("Movement")]
    public float speed = 5f;
    public float runningSpeed = 10f;
    public float airspeed = 4f;
    public float fallMultiplier = 2.5f;
    public InputActionReference moveAction;
    #endregion
    
    #region Jumping
    [Header("Jumping")]
    public bool jumpUnlocked = true;
    public bool doubleJumpUnlocked = true;
    public bool jumpUsed = false;
    public bool doubleJumpUsed = false;
    public float jumpForce = 7f;
    public float normalGravityScale = 1f;
    public InputActionReference jumpAction;
    public float timeSinceLastJumpInput = 100f;
    public float jumpInputBufferTime = 0.2f;
    public float jumpCoyoteTime = 0.2f;
    public float timeSinceLastGrounded = 0f;
    public float jumpGraceDuration = 0.3f;
    public float jumpGraceGravityScale = 0.5f;
    public float timeSinceLastJump = 100f;
    public AudioClip jumpSound;
    public AudioClip doubleJumpSound;
    
    #endregion

    #region Grounding
    [Header("Grounding")]
    public LayerMask groundLayer;
    public Vector3 groundPositionOffset = new Vector3(0, 0.1f,0);
    public float groundRayLength = 0.2f;
    public bool isGrounded;
    public bool wasGrounded;
    public AudioClip landSound;
    #endregion

    #region Dashing
    [Header("Dashing")]
    public bool dashUnlocked = true;
    public InputActionReference dashAction;
    public float dashForce = 10f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1f;
    public float timeSinceLastDash = 100f;
    public bool isDashing = false;
    public bool dahsUsed = false;
    public AudioClip dashSound;
    #endregion

    #region Rendering
    public bool facingRight = true;

    #endregion

    #region Respawn

    public Vector3 lastGroundedPosition;

    #endregion
    
    void Update()
    {
        UpdateTimeTrackers();
        RegisterJumpInput();
        UpdateGroundedStatus();
        HandleMovement();
        JumpHandler();
        DashHandler();
        UpdateFacingDirection();
        GravityScaling();
    }

    private void GravityScaling()
    {
        rb.gravityScale = normalGravityScale;
        if(isDashing)
        {
            rb.gravityScale = 0f;
            return;
        }
        
        if (rb.linearVelocity.y < float.Epsilon)
        {
            rb.gravityScale = fallMultiplier;
        }
        
        if(timeSinceLastJump < jumpGraceDuration && jumpAction.action.IsPressed())
        {
            rb.gravityScale = jumpGraceGravityScale;
        }
    }

    private void JumpHandler()
    {
        if (jumpUnlocked && timeSinceLastJumpInput<jumpInputBufferTime && timeSinceLastGrounded < jumpCoyoteTime && !jumpUsed)
        {
            AudioSource.PlayClipAtPoint(jumpSound, transform.position);
            jumpUsed = true;
            isGrounded = false;
            timeSinceLastJumpInput = 100f; // Reset jump input timer
            timeSinceLastJump = 0f;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }else if(doubleJumpUnlocked && !doubleJumpUsed && !isGrounded && jumpAction.action.triggered)
        {
            AudioSource.PlayClipAtPoint(doubleJumpSound, transform.position);
            doubleJumpUsed = true;
            timeSinceLastJumpInput = 100f; // Reset jump input timer
            timeSinceLastJump = 0f;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }
    
    private void DashHandler()
    {
        if (dashUnlocked && dashAction.action.triggered && timeSinceLastDash > dashCooldown && !dahsUsed)
        {
            AudioSource.PlayClipAtPoint(dashSound, transform.position);
            isDashing = true;
            dahsUsed = true;
            timeSinceLastDash = 0f;
            Vector2 dashDirection = moveAction.action.ReadValue<Vector2>();
            if (dashDirection == Vector2.zero)
            {
                dashDirection = Vector2.right * (facingRight ? 1 : -1); // Default dash direction based on facing
            }
            dashDirection.y = 0; // Lock dash to horizontal plane
            rb.linearVelocity = dashDirection.normalized * dashForce;
            Invoke(nameof(EndDash), dashDuration);
        }
    }
    
    private void EndDash()
    {
        isDashing = false;
    }

    private void HandleMovement()
    {
        if(timeSinceLastJump < jumpGraceDuration)
            return;
        
        if(isDashing)
            return;
        
        var moveInput = moveAction.action.ReadValue<Vector2>();
        var speedToUse = speed;
        speedToUse = dashUnlocked && dashAction.action.IsPressed() ? runningSpeed : speedToUse;
        speedToUse = isGrounded ? (speedToUse ) : airspeed;
        rb.linearVelocity = new Vector2(moveInput.x * speedToUse, rb.linearVelocity.y);
    }

    private void UpdateTimeTrackers()
    {
        timeSinceLastJump += Time.deltaTime;
        timeSinceLastJumpInput += Time.deltaTime;
        timeSinceLastGrounded += Time.deltaTime;
        timeSinceLastDash += Time.deltaTime;
    }
    
    private void UpdateFacingDirection()
    {
        if(facingRight)
        {
            if(rb.linearVelocity.x < -0.1f)
                facingRight = false;
        }
        else
        {
            if(rb.linearVelocity.x > 0.1f)
                facingRight = true;
        }
    }

    private void RegisterJumpInput()
    {
        if (jumpAction.action.triggered)
        {
            timeSinceLastJumpInput = 0;
        }
    }

    void UpdateGroundedStatus()
    {
        wasGrounded = isGrounded;
        RaycastHit2D hit = Physics2D.Raycast(transform.position + groundPositionOffset, Vector2.down, groundRayLength, groundLayer);
        isGrounded = hit.collider != null;
        if(isGrounded)
        {
            timeSinceLastGrounded = 0f;
            dahsUsed = false; // Reset dash usage when grounded
            doubleJumpUsed = false; // Reset double jump when grounded
            jumpUsed = false; // Reset jump usage when grounded
            lastGroundedPosition = transform.position;
        }
        if(!wasGrounded && isGrounded)
        {
            AudioSource.PlayClipAtPoint(landSound, transform.position);
        }
    }
}