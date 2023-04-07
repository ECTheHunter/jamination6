using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float acceleration_speed;
    public float max_speed;
    public float jump_power;
    public bool is_dead;
    public bool is_grounded;
    public float slowdown_factor;
    public Rigidbody2D rb2D;
    public float groundcheck_raycastdistance;
    public bool D_held;
    public bool A_held;
    public LayerMask layerMask;
    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckGround();
        if(is_grounded)
        {
            Jump();
        }
        CheckKeyPress();
        rb2D.velocity = new Vector2(Mathf.Clamp(rb2D.velocity.x, -max_speed, max_speed), Mathf.Clamp(rb2D.velocity.y, -max_speed, max_speed));

    }
    void FixedUpdate()
    {
        if (is_grounded)
        {
            LFMovement();
            SlowDown();
        }
    }
    public void CheckKeyPress()
    {
        A_held = Input.GetKey(KeyCode.A);
        D_held = Input.GetKey(KeyCode.D);
    }
    public void LFMovement()
    {
        if(A_held)
        {
            Vector2 dir = acceleration_speed * Time.deltaTime * Vector2.left;
            rb2D.AddForce(dir);
        }
        if(D_held)
        {
            Vector2 dir = acceleration_speed * Time.deltaTime * Vector2.right;
            rb2D.AddForce(dir);
        }
    }
    public void SlowDown()
    {
        if (!A_held && !D_held)
        {
            if (Mathf.Abs(rb2D.velocity.x) > 1f)
            {
                rb2D.velocity += -rb2D.velocity * slowdown_factor;
            }
        }

    }
    public void CheckGround()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundcheck_raycastdistance, layerMask);
        if(hit.collider != null)
        {
            is_grounded = true;
        }
        else
        {
            is_grounded = false;
        }
    }
    public void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb2D.AddForce(jump_power * Vector2.up);
        }
    }
}
