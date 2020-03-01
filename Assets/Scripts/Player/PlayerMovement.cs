using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController2D controller;
    public Animator animator;
    public float runSpeed = 40f;
    float horizontalMove = 0f;
    bool jump = false;

    //Combat Restrictions
    public bool isDazed = false;
    public bool knockFromRight;
    public float knockback = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDazed)
        {
            horizontalMove = (Input.GetAxisRaw("Horizontal") * runSpeed);

            animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

            if (Input.GetButtonDown("Jump"))
            {
                jump = true;
                animator.SetBool("IsJumping", true);
            }
        }
        else
        {
            if (knockFromRight)
            {
                //GetComponent<Rigidbody2D>().AddForce(new Vector2(-knockback, knockback));
                GetComponent<Rigidbody2D>().velocity = new Vector2(-knockback, knockback);
                horizontalMove = 0;
            } 
            else
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(knockback, knockback);
                //GetComponent<Rigidbody2D>().AddForce(new Vector2(knockback, knockback)); 
            }
        }
    }

    public void OnLanding()
    {
        animator.SetBool("IsJumping", false);
    }

    private void FixedUpdate()
    {
        //Move our character
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        jump = false;
    }

    public void Daze(bool daze)
    {
        isDazed = daze;
    }

}
