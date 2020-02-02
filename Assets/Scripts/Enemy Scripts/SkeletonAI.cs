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
        if (System.Math.Abs(player.position.x - transform.position.x) > 2.3)
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
        foreach(Collider2D enemy in hitPlayer)
        {
            Debug.Log("Hit");
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