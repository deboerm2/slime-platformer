using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float jumpStrength;
    public float slideForce;

    private Collider2D col;
    private Rigidbody2D rb;
    private float xInput;
    private float yInput;
    [SerializeField]
    private bool isGroundedX;
    [SerializeField]
    private bool isGroundedY;

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

        CheckGrounded();
    }

    private void LateUpdate()
    {
        Movement();
    }

    void Movement()
    {
        rb.AddForce(new Vector2(xInput, yInput),ForceMode2D.Impulse);
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
            isGroundedX = true;
        else
            isGroundedX = false;

        //Debug.DrawRay(col.bounds.center + (1.05f * col.bounds.extents.y * Vector3.down), Vector2.up * col.bounds.size.y * 1.05f, Color.white);
        if (Physics2D.Raycast(col.bounds.center + (1.05f * col.bounds.extents.y * Vector3.down), Vector2.up, col.bounds.size.y * 1.05f, layermask))
            isGroundedY = true;
        else
            isGroundedY = false;

    }
}
