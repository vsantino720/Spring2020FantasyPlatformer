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
    public bool isAttacking = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDazed && !isAttacking)
        {
            horizontalMove = (Input.GetAxisRaw("Horizontal") * runSpeed);

            animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

            if (Input.GetButtonDown("Jump"))
            {
                jump = true;
                animator.SetBool("IsJumping", true);
            }
            if (controller.m_Grounded == true && gameObject.layer != 8)
            {
                gameObject.layer = 8;
            }

            if (controller.m_Grounded == true && Input.GetAxisRaw("Vertical") < 0)
            {
                gameObject.layer = 10;
            }
        }
        else
        {
            horizontalMove = 0;
            animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
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
