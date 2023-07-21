using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    //Start() variable
    private Rigidbody2D rb;
    private Animator anim;
    private Collider2D coll;


    //FSM
    private enum State {idle, running, jumping, falling, hurt, climb};
    private State state = State.idle;

    //Ladder variable
    [HideInInspector] public bool canClimb = false;
    [HideInInspector] public bool bottomLadder = false;
    [HideInInspector] public bool topLadder = false;
    public Ladder ladder;
    private float naturalGravity;
    [SerializeField] float climbSpeed = 3f;

   
    //inspector variable
    [SerializeField] private LayerMask ground;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float hurtForce = 10f;
    [SerializeField] private AudioSource cherry;
    [SerializeField] private AudioSource footstep;
    

    //test
    [SerializeField] public int cherries = 0;
    [SerializeField] private TextMeshProUGUI cherryText;
    [SerializeField] public int finalCherries;
    [SerializeField] private TextMeshProUGUI finalCherriesText;
    [SerializeField] private int health;
    [SerializeField] private TextMeshProUGUI healthText;

    private bool moveLeft;
    private bool moveRight;
    private float horizontalMove;
    private float hDirection;
    private float vDirection;

    private float originalJumpForce;
    private float originalSpeed;

    public bool hasFullCherries()
    {
        return cherries == finalCherries;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
        healthText.text = health.ToString();
        naturalGravity = rb.gravityScale;
        finalCherriesText.text = finalCherries.ToString();

        originalJumpForce = jumpForce;
        originalSpeed = speed;

        moveLeft = false;
        moveRight = false;
    }

    private void Update()
    {
        if(state == State.climb)
        {
            Climb();
        }
        else if(state != State.hurt)
        {
            Movement();

        }
        AnimationState();
        anim.SetInteger("state", (int)state); //set animation based on nemurator state
    }

    //cherries
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Collectable")
        {
            cherry.Play();
            Destroy(collision.gameObject);
            cherries += 1;
            cherryText.text = cherries.ToString();
        }
        if(collision.tag == "Poweruphigh")
        {
            Destroy(collision.gameObject);
            jumpForce = 35;
            GetComponent<SpriteRenderer>().color = Color.green;
            StartCoroutine(ResetPower());
        }
        if (collision.tag == "Powerupspeed")
        {
            Destroy(collision.gameObject);
            speed = 14;
            GetComponent<SpriteRenderer>().color = Color.yellow;
            StartCoroutine(ResetPower());
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
     if(other.gameObject.tag == "Enemy")
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();

            if (state == State.falling)
            {
                enemy.JumpedOn();
                Jump();
            }
            else
            {
                state = State.hurt;
                HandleHealth(); //deals with health updating UI, reset level when lose all health
                if (other.gameObject.transform.position.x > transform.position.x)
                {
                    //enemy is right then i move to left
                    rb.velocity = new Vector2(-hurtForce, rb.velocity.y);
                }
                else
                {
                    //enemy is left then i move to right
                    rb.velocity = new Vector2(hurtForce, rb.velocity.y);

                }

            }
        }   
    }

    private void HandleHealth()
    {
        health -= 1;
        healthText.text = health.ToString();
        if (health <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }



    //movement
    public void Movement()
    {
        //float hDirection = Input.GetAxis("Horizontal");

        if (canClimb && Mathf.Abs(Input.GetAxis("Vertical")) > .1f)
        {
            state = State.climb;
            rb.constraints = RigidbodyConstraints2D.FreezePositionX |
                RigidbodyConstraints2D.FreezeRotation;
            transform.position = new Vector3(ladder.transform.position.x, rb.position.y);
            rb.gravityScale = 0f;
        }

        //moving left
        if (hDirection < 0)
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            transform.localScale = new Vector2(-1, 1);
        }
        //moving right
        else if (hDirection > 0)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
            transform.localScale = new Vector2(1, 1);
        }
        //jumping
        if (vDirection>0 && coll.IsTouchingLayers(ground))
        {
            Jump();
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        state = State.jumping;
    }

    //animation
    private void AnimationState()
    {
        if(state == State.climb)
        {

        }
        else if(state == State.jumping)
        {
            if(rb.velocity.y < .1f)
            {
                state = State.falling;
            }
        }

        else if(state == State.falling)
        {
            if (coll.IsTouchingLayers(ground))
            {
                state = State.idle;
            }
        }
        else if(state == State.hurt)
        {
            if(Mathf.Abs(rb.velocity.x) < .1f)
            {
                state = State.idle;
            }
        }

        else if(Mathf.Abs(rb.velocity.x) > 2f)
        {
            //moving
            state = State.running;

        }
        else
        {
            state = State.idle;
        }
    }

    private void Footstep()
    {
        footstep.Play();
    }

    private IEnumerator ResetPower()
    {
        yield return new WaitForSeconds(2);
        jumpForce = originalJumpForce;
        speed = originalSpeed;
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    private void Climb()
    {
        if (Input.GetButton("Jump"))
        {
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            canClimb = false;
            rb.gravityScale = naturalGravity;
            anim.speed = 1f;
            Jump();
            return;
           
        }
        float vDirection = Input.GetAxis("Vertical");

        //Climbing up
        if(vDirection > .1f && !topLadder)
        {
            rb.velocity = new Vector2(0f, vDirection * climbSpeed);
            anim.speed = 1f;
        }
        //Climbing down
        else if(vDirection < -.1f && !bottomLadder)
        {
            rb.velocity = new Vector2(0f, vDirection * climbSpeed);
            anim.speed = 1f;

        }
        //Still
        else
        {
            anim.speed = 0f;
            rb.velocity = Vector2.zero;
        }
    }

    public void PointerDownLeft()
    {
        moveLeft = true;
        hDirection = -1;
    }

    public void PointerUpLeft()
    {
        moveLeft = false;
        hDirection = 0;
    }

    public void PointerDownRight()
    {
        moveRight = true;
        hDirection = 1;
    }

    public void PointerUpRight()
    {
        moveRight = false;
        hDirection = 0;
    }

    public void PointerDownJump()
    {
        vDirection = 1;
    }

    public void PointerUpJump()
    {
        vDirection = 0;
    }
}

