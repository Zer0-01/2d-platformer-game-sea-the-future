using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : Enemy
{
    [SerializeField] private float leftCap;
    [SerializeField] private float rightCap;

    [SerializeField] private float jumpLength = 10f;
    [SerializeField] private float jumpHeight = 15f;
    [SerializeField] private LayerMask ground;

    private Collider2D coll;

    private bool facingLeft = true;


    protected override void Start()
    {
        base.Start();
        coll = GetComponent<Collider2D>();
    }




    private void Update()
    {
       //transition from jump to fall
       if (anim.GetBool("Jumping"))
        {
            if(rb.velocity.y < .1)
            {
                anim.SetBool("Falling", true);
                anim.SetBool("Jumping", false);
            }
        }

        //transition form fall to idle
        if(coll.IsTouchingLayers(ground) && anim.GetBool("Falling"))
        {
            anim.SetBool("Falling", false);
        }
    }

    private void Move()
    {
        if (facingLeft)
        {

            //test to see if we are beyond leftCap
            if (transform.position.x > leftCap)
            {
                if (transform.localScale.x != 1)
                {
                    //make sure sprite is facing the right location, if not, face to the right
                    transform.localScale = new Vector2(1, 1);
                }

                if (coll.IsTouchingLayers(ground))
                {
                    //jump
                    rb.velocity = new Vector2(-jumpLength, jumpHeight);
                    anim.SetBool("Jumping", true);

                }
            }
            else
            {
                facingLeft = false;
            }
            //if it is not, the face right


        }
        else
        {
            //test to see if we are beyond leftCap
            if (transform.position.x < rightCap)
            {
                if (transform.localScale.x != -1)
                {
                    //make sure sprite is facing the right location, if not, face to the right
                    transform.localScale = new Vector2(-1, 1);
                }

                if (coll.IsTouchingLayers(ground))
                {
                    //jump
                    rb.velocity = new Vector2(jumpLength, jumpHeight);
                    anim.SetBool("Jumping", true);


                }
            }
            else
            {
                facingLeft = true;
            }
        }
    }

   

   
}
