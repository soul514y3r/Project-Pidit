
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMoveScript : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    public float raydist;
    public float jumpTimeMax;

    RaycastHit2D Hit;
    AudioSource source;
    Rigidbody2D rg;
    SpriteRenderer rd;
    Collider2D col;
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
    public bool shouldSound;
    float jumpTimer;

    

    void Awake()
    {// Gets all refrences
        source = gameObject.GetComponent<AudioSource>();
        rg = gameObject.GetComponent<Rigidbody2D>();
        rd = gameObject.GetComponent<SpriteRenderer>();
        col = gameObject.GetComponent<Collider2D>();
        MoveAction = InputSystem.actions.FindAction("Move");
        JumpAction = InputSystem.actions.FindAction("Jump");
    }

    void OnEnable()
    {//subscribes to the inputs from the new inputsystem
        MoveAction.performed += MoveInp;
        MoveAction.canceled += MoveNot;
        JumpAction.performed += JumpInp;
        JumpAction.canceled += JumpNot;
    }

    void OnDisable()
    {//Unsubscribes to the inputs to prevent errors on load
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

    public void playSound()
    {
        source.Play();
    }

    void Update()
    {
        if(shouldSound)
        {
         //   playSound();
            shouldSound = false;
        }
    }

    void FixedUpdate()
    {//Raycast for jumping
    Hit = Physics2D.BoxCast(col.bounds.center, col.bounds.extents *2,0, Vector2.down, raydist, GroundDet);

        //just resets your velocity instantly for more snappy movement
        if(rg.linearVelocityX * MoveVec.x < 0 && Hit == true)
        rg.linearVelocityX = 0;

        //Movement
        rg.AddForce(new Vector2 (MoveVec.x * speed, 0), ForceMode2D.Impulse);

        //Jumping. So long as your key is down, you continue jumping and continue an ongoing jump. Can cancel on canceled input.
        if (JumpIsDown && canJump)
        {
            jumpTimer = jumpTimeMax;
            rg.linearVelocityY = 0;
            rg.AddForce(Vector2.up*jumpForce*5, ForceMode2D.Impulse);
        }

        if(jumpTimer > 0 && JumpIsDown)
        {
            isJumping = true;
            rg.AddForce(Vector2.up*jumpForce, ForceMode2D.Impulse);
        }
        else
        {
            isJumping = false;
            jumpTimer = 0;
        }
        
            
        if(jumpTimer > 0)
        jumpTimer -= Time.fixedDeltaTime;

        if (Hit.collider != null && shouldcheck == true)
        {
            canJump = true;
            isJumping = false;
        }
        
        else
        canJump = false;

        shouldcheck = true;


        //flip sprite logic. Will move to method in order to make animating easier
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
        //Debug for the jump raycast
        Gizmos.DrawWireCube(new Vector3(col.bounds.center.x, col.bounds.center.y - raydist, col.bounds.center.z), col.bounds.extents *2);
    }
}
