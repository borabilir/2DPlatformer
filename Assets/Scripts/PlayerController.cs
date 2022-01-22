using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private float moveSpeed = 0;
    [SerializeField] private float jumpForce = 0;
    [SerializeField] private LayerMask groundLayers = new LayerMask();

    private bool isGrounded = false;
    private bool canDoubleJump = true;
    private Rigidbody2D playerBody;
    private Transform groundCheckPoint;
    private Animator anim;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        playerBody = GetComponent<Rigidbody2D>();
        groundCheckPoint = transform.GetChild(0).transform;
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        playerBody.velocity = new Vector2(moveSpeed * horizontalInput, playerBody.velocity.y);

        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, .2f, groundLayers);

        if (isGrounded)
            canDoubleJump = true;

        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded)
            {
                playerBody.velocity = new Vector2(playerBody.velocity.x, jumpForce);
                //playerBody.AddForce(new Vector2(playerBody.velocity.x, jumpForce*Time.deltaTime));
            }
            else
            {
                if (canDoubleJump)
                {
                    playerBody.velocity = new Vector2(playerBody.velocity.x, jumpForce);
                    canDoubleJump = false;
                }
            }
        }

        if (playerBody.velocity.x > 0)
            spriteRenderer.flipX = false;
        else if(playerBody.velocity.x < 0)
            spriteRenderer.flipX = true;


        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("moveSpeed", Mathf.Abs(playerBody.velocity.x));
    }
}
