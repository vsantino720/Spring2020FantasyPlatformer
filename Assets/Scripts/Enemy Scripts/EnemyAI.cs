using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{

    public int maxHealth;
    int currentHealth;
    public float speed;
    public Animator animator;
    public bool facingRight;
    public bool onGround;

    void Start()
    {
        if (gameObject.tag == "Wizard (Fire)")
        {
            WizardFire();
        }
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (onGround)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
        transform.position = transform.position - new Vector3(0.0f, 0.0f, transform.position.z);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            onGround = true;
        }
        else
        {
            onGround = false;
        }

        if (collision.gameObject.tag == "Edge")
        {
            if (facingRight)
            {
                transform.Translate(Vector3.left * 0.04f);
                transform.position = transform.position - new Vector3(0.0f, transform.position.y / 50, 0.0f);
                transform.eulerAngles += new Vector3(0, -180, 0);
            }
            else
            {
                transform.Translate(Vector3.left * 0.04f);
                transform.position = transform.position - new Vector3(0.0f, transform.position.y / 50, 0.0f);
                transform.eulerAngles += new Vector3(0, 0, 0);
            }

            facingRight = !facingRight;
        }
    }

    public void WizardFire ()
    {
        facingRight = true;
        maxHealth = 2;
        speed = 2.0f;
    }
}
