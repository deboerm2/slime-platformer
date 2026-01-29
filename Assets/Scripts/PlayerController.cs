using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float jumpStrength;
    public float slideStrength;

    private Collider2D col;
    private Rigidbody2D rb;
    private float xInput;
    private float yInput;
    [SerializeField]
    [Tooltip("Grounded on a vertical plane")]
    private bool isGroundedY;
    [SerializeField] [Tooltip("Grounded on a horizontal plane")]
    private bool isGroundedX;
    
    //used to determine if a burst of movement on respective axis is available
    public bool xBurst;
    public bool yBurst;
    private bool xBurstInput;
    private bool yBurstInput;

    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        xInput = Input.GetAxis("Horizontal");
        yInput = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.W))
        {
            yBurstInput = true;
            yInput = 1f;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            yBurstInput = true;
            yInput = -1f;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            xBurstInput = true;
            xInput = -1f;
        }
        if(Input.GetKeyDown(KeyCode.D))
        {
            xBurstInput = true;
            xInput = 1f;
        }
            

        CheckGrounded();

        if(isGroundedX || isGroundedY)
        {
            xBurst = true;
            yBurst = true;
        }

        
       
    }

    private void LateUpdate()
    {
        Movement();
    }

    void Movement()
    {
        //rb.AddForce(new Vector2(xInput, yInput),ForceMode2D.Impulse);
        if(xInput != 0f)
        {
            if (isGroundedX)
            {
                rb.velocity = new Vector2(xInput, 0) * slideStrength;
            }
            else if (xBurst && xBurstInput)
            {
                rb.velocity = Vector2.zero;
                rb.AddForce(new Vector2(xInput, 0) * jumpStrength,ForceMode2D.Impulse);
                xBurst = false;
            }
            
        }
        if(yInput != 0f)
        {
            if (isGroundedY)
            {
                rb.velocity = new Vector2(0, yInput) * slideStrength;
            }
            else if (yBurst && yBurstInput)
            {
                rb.velocity = Vector2.zero;
                rb.AddForce(new Vector2(0, yInput) * jumpStrength,ForceMode2D.Impulse);
                yBurst = false;
            }
            
        }
        xBurstInput = false;
        yBurstInput = false;
    }

    void CheckGrounded()
    {
        Vector2 corner00 = col.bounds.min;                                              //bottom left
        Vector2 corner01 = col.bounds.min + (Vector3.right * transform.localScale.x);   //bottom right
        Vector2 corner10 = col.bounds.max + (Vector3.left * transform.localScale.x);    //top left
        Vector2 corner11 = col.bounds.max;                                              //top right

        
        int layermask = 1 << LayerMask.NameToLayer("Player");
        layermask = ~layermask;

        //Debug.DrawRay(col.bounds.center + (1.05f * col.bounds.extents.x * Vector3.left), Vector2.right * col.bounds.size.x * 1.05f, Color.white);
        if (Physics2D.Raycast(col.bounds.center + (1.05f * col.bounds.extents.x * Vector3.left), Vector2.right, col.bounds.size.x * 1.05f, layermask))
            isGroundedY = true;
        else
            isGroundedY = false;

        //Debug.DrawRay(col.bounds.center + (1.05f * col.bounds.extents.y * Vector3.down), Vector2.up * col.bounds.size.y * 1.05f, Color.white);
        if (Physics2D.Raycast(col.bounds.center + (1.05f * col.bounds.extents.y * Vector3.down), Vector2.up, col.bounds.size.y * 1.05f, layermask))
            isGroundedX = true;
        else
            isGroundedX = false;

    }
}
