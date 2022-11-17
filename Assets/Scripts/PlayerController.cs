using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movimiento")]
    public float speed;

    [Header("Salto")]
    private bool DoubleJump;
    public float jumpForce;

    [Header("Componentes")]
    public Rigidbody2D RB;

    [Header("Grounded")]
    private bool grounded;
    [Header("Animator")]
    private Animator anim;
    private SpriteRenderer theSR;

    public Transform groundCheckpoint;
    public LayerMask ground;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        theSR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        RB.velocity = new Vector2(speed * Input.GetAxis("Horizontal"), RB.velocity.y);

        grounded = Physics2D.OverlapCircle(groundCheckpoint.position, .2f, ground);

        if (grounded)
        {
            DoubleJump = true;
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (grounded)
            {
                RB.velocity = new Vector2(RB.velocity.x, jumpForce);
            }
            else
            {
                if (DoubleJump)
                {
                    RB.velocity = new Vector2(RB.velocity.x, jumpForce);
                    DoubleJump = false;
                }
            }
            
        }

        if (RB.velocity.x < 0)
        {
            theSR.flipX = true;
        }else if (RB.velocity.x > 0)
        {
            theSR.flipX = false;
        }
        anim.SetFloat("speed", Mathf.Abs(RB.velocity.x));
        anim.SetBool("grounded", grounded);

    }
}
