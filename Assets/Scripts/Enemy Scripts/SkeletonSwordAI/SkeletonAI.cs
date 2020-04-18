using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAI : MonoBehaviour
{
    public Animator animator;
    private float timeBetweenAttack;
    public float cooldownTime;
    public Transform player;
    public float speed = 1.0f;
    private bool moving;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask layers;
    public int attackDamage = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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
        if (System.Math.Abs(player.position.x - transform.position.x) > attackRange)
        {
            Move();
        }
        else if (System.Math.Abs(player.position.y - transform.position.y) < attackRange)
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
<<<<<<< HEAD
        timeBetweenAttack -= Time.deltaTime;
    }
=======
        timeBetweenAttack -= Time.deltaTime;
    }
>>>>>>> acd5895815bb37961ceae90625503d92d2a982b0

    void Attack()
    {
        animator.SetFloat("Action", 0);
        animator.SetTrigger("Attack");
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, layers);
        foreach(Collider2D player in hitPlayer)
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

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

}