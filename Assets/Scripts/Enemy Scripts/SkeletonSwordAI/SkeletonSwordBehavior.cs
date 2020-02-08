using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonSwordBehavior : MonoBehaviour
{
    //General Object Vars
    public Animator animator;
    public Transform player;
    public LayerMask layers;
    public Transform attackPoint;
    public GameObject GroundCheck;

    //Movement Vars
    public float speed = 1.0f;
   
    //Combat Vars
    private float timeBetweenAttack;
    public float cooldownTime;
    public int maxHealth = 4;
    int currentHealth;
    public float attackRange = 0.5f;
    public float playerRange = 1.2f;
    public int attackDamage = 1;
    private float dazedTime;
    public float startDazedTime = 0.6f;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        //Combat Modifiers
        if(dazedTime <= 0)
        {
            speed = 1.0f;
        } 
        else
        {
            speed = 0;
            dazedTime -= Time.deltaTime;
        }
        //Skeleton turns around
        if (player.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        //Movement and attack
        if (System.Math.Abs(player.position.x - transform.position.x) > playerRange)
        {
            Move();
        }
        else if (System.Math.Abs(player.position.y - transform.position.y) < 2)
        {
            if (timeBetweenAttack <= 0)
            {
                Attack();

            }

        }
        else
        {
            Idle();
        }
        timeBetweenAttack -= Time.deltaTime;
    }

    //Skeleton attack method
    void Attack()
    {
        animator.SetFloat("Action", 0);
        animator.SetTrigger("Attack");
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, layers);
        foreach (Collider2D player in hitPlayer)
        {
            player.GetComponent<PlayerCombat>().TakeDamage(attackDamage);
        }
        timeBetweenAttack = cooldownTime;

    }

    void Move()
    {
        animator.SetFloat("Action", 1);
        float step = Time.deltaTime * 2.5f * speed;
        transform.position = Vector3.MoveTowards(transform.position, player.position, step);
    }

    void Idle()
    {
        animator.SetFloat("Action", 0);

    }

    public void TakeDamage(int damage)
    {
        dazedTime = startDazedTime;
        currentHealth -= damage;
        Debug.Log("Took" + damage + "damage");
        // Play hurt animation
        animator.SetTrigger("Hurt");

        if (currentHealth <= 0)
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
        GetComponent<Collider2D>().enabled = false;
        GroundCheck.GetComponent<Collider2D>().enabled = false;
        GetComponent<Rigidbody2D>().simulated = false;
        this.enabled = false;
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
