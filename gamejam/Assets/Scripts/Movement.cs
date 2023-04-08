using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    [Header("Movement")]
    public float acceleration_speed;
    public float max_speed;
    public float jump_power;
    public bool is_grounded;
    public float slowdown_factor;
    public float groundcheck_raycastdistance;
    [Header("Components")]
    public GameObject raycast_origin;
    public Rigidbody2D rb2D;
    public Animator animator;
    [Header("States")]
    public float throw_power;
    public bool D_held;
    public bool A_held;
    public bool pickedup;
    public bool is_dead;

    public GameObject pickup_origin;
    public LayerMask layerMask;
    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!is_dead)
        {
            CheckGround();
            if (is_grounded)
            {
                Jump();
                KYS();
            }
            CheckKeyPress();
            rb2D.velocity = new Vector2(Mathf.Clamp(rb2D.velocity.x, -max_speed, max_speed), Mathf.Clamp(rb2D.velocity.y, -max_speed, max_speed));

        }
    }
    void FixedUpdate()
    {
        if (!is_dead)
        {
            if (is_grounded)
            {
                SlowDown();
            }
            LFMovement();
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
            if (is_grounded)
            {
                rb2D.AddForce(dir);
            }
            else
            {
                rb2D.AddForce(dir / 3);
            }
            transform.localScale = new Vector3(-1f, 1f, 0f);
            animator.SetBool("isrunning", true);
        }
        if(D_held)
        {
            Vector2 dir = acceleration_speed * Time.deltaTime * Vector2.right;
            if (is_grounded)
            {
                rb2D.AddForce(dir);
            }
            else
            {
                rb2D.AddForce(dir / 3);
            }
            transform.localScale = new Vector3(1f, 1f, 0f);
            animator.SetBool("isrunning", true);
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
            else
            {
                animator.SetBool("isrunning", false);
            }
            
        }

    }
    public void CheckGround()
    {
        RaycastHit2D hit = Physics2D.Raycast(raycast_origin.transform.position, Vector2.down, groundcheck_raycastdistance, layerMask);
        if(hit.collider != null)
        {
            is_grounded = true;
            if(hit.transform.tag == "Spikes")
            {
                animator.SetTrigger("hedead");
                gameObject.tag = "Pickable";
                is_dead = true;
            }
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
            animator.SetTrigger("isjumping");
        }
    }
    public void JumpEvent()
    {
        rb2D.AddForce(jump_power * Vector2.up);
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        if (!is_dead)
        {
            if (Input.GetKey(KeyCode.E) && !pickedup && collision.tag == "Pickable")
            {
                collision.gameObject.transform.SetParent(pickup_origin.transform, false);
                animator.SetTrigger("ispickingup");
                collision.gameObject.layer = 7;
                collision.GetComponent<Rigidbody2D>().gravityScale = 0f;
                pickedup = true;
            }
        }
        
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "LevelEnd")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
    public void Throw()
    {
        GameObject gameObject = pickup_origin.GetComponentInChildren<GameObject>();
        gameObject.transform.parent = null;
        gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(1f, 0.3f) * throw_power);
    }
    public void KYS()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            animator.SetTrigger("iskys");
            gameObject.tag = "Pickable";
            int x = (int)UnityEngine.Random.Range(0f, 1.99f);
            animator.SetInteger("Death", x);
            is_dead = true;
        }
    }
    public void KYSRespawn()
    {
        GameManager.Instance.player = Instantiate(GameManager.Instance.playerprefab, GameManager.Instance.spawnpoint.transform.position, GameManager.Instance.spawnpoint.transform.rotation);
    }
}
