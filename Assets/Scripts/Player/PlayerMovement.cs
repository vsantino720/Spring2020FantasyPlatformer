using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private CharacterController2D controller;
    [SerializeField] private Animator animator;
    [SerializeField] private float runSpeed = 40f;
    [SerializeField] private float horizontalMove = 0f;
    [SerializeField] private bool jump = false;

    //Combat Restrictions
    [SerializeField] private bool isDazed = false;
    [SerializeField] private bool isAttacking = false;

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

            if (Input.GetButtonDown("Jump") && controller.m_Grounded == true)
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
        Debug.Log("Player Landed.");
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

    public void setAttacking(bool attacking)
    {
        isAttacking = attacking;
    }

    public bool getDazed()
    {
        return isDazed;
    }

}
