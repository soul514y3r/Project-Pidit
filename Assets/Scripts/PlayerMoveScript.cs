
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMoveScript : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    public float raydist;
    Rigidbody2D rg;
    SpriteRenderer rd;
    InputAction MoveAction;
    InputAction JumpAction;
    Vector2 MoveVec;
    bool canJump;
    bool isJumping;
    bool JumpIsDown;
    bool shouldcheck;
    public LayerMask GroundDet;

    public float jumpBufferMax;
    float jumpBuffer;

    void Awake()
    {
        rg = gameObject.GetComponent<Rigidbody2D>();
        rd = gameObject.GetComponent<SpriteRenderer>();
        MoveAction = InputSystem.actions.FindAction("Move");
        JumpAction = InputSystem.actions.FindAction("Jump");
    }

    void OnEnable()
    {
        MoveAction.performed += MoveInp;
        MoveAction.canceled += MoveNot;
        JumpAction.performed += JumpInp;
        JumpAction.canceled += JumpNot;
    }

    void OnDisable()
    {
        MoveAction.performed -= MoveInp;
        MoveAction.canceled -= MoveNot;
        JumpAction.performed -= JumpInp;
        JumpAction.canceled -= JumpNot;
    }

    void JumpInp(InputAction.CallbackContext context)
    {
        JumpIsDown = true;
    }
    void JumpNot(InputAction.CallbackContext context)
    {
        JumpIsDown = false;
    }

    void MoveInp(InputAction.CallbackContext context)
    {
        MoveVec = MoveAction.ReadValue<Vector2>();
    }

    void MoveNot(InputAction.CallbackContext context)
    {
        MoveVec = Vector2.zero;
    }

    void FixedUpdate()
    {
    RaycastHit2D hit = Physics2D.BoxCast(transform.position, transform.localScale,0, Vector2.down, raydist, GroundDet);

        if(rg.linearVelocityX * MoveVec.x < 0 && hit == true)
        rg.linearVelocityX = 0;

        rg.AddForce(new Vector2 (MoveVec.x * speed, 0), ForceMode2D.Impulse);


    
        if (jumpBuffer > 0 && canJump)
        {
            isJumping = true;
            Debug.Log("Jumping");
            rg.linearVelocityY = 0;
            rg.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            canJump = false;
            shouldcheck = false; 
            
        }
        if(JumpIsDown)
        {
            jumpBuffer = jumpBufferMax;
        }
        else
        {
            if (rg.linearVelocityY > 0 && isJumping)
        {
            rg.linearVelocityY = 0;
        }
            jumpBuffer -= Time.fixedDeltaTime;
        }

        if (hit.collider != null && shouldcheck == true)
        {
            canJump = true;
            isJumping = false;
        }
        
        else
        canJump = false;

        shouldcheck = true;

        if(MoveVec.x != 0)
        {
        if(rg.linearVelocityX < 0)
        rd.flipX = true;
        else
        rd.flipX = false;  
        }
        
        
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(new Vector3(transform.position.x, transform.position.y - raydist, 0), transform.localScale);
    }
}
