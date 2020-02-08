using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 4;
    int currentHealth;
    public Animator animator;
    public GameObject GroundCheck;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Took" + damage + "damage");
        // Play hurt animation
        animator.SetTrigger("Hurt");

        if(currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        animator.SetBool("IsDead", true);
        //Die Animation
        Debug.Log("Enemy Died!");
        //Disable the enemy
        GetComponent<SkeletonAI>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        GroundCheck.GetComponent<Collider2D>().enabled = false;
        GetComponent<Rigidbody2D>().simulated = false;
        this.enabled = false;
    }
}
