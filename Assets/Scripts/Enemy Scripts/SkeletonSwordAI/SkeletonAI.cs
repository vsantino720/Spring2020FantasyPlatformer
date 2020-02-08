<<<<<<< HEAD:Assets/Scripts/Enemy Scripts/SkeletonAI.cs
﻿using System.Collections;
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
    public float AttackRangeX = 2.3f;
    public float AttackRangeY = 2;
    // Start is called before the first frame update
    void Start()
=======
﻿using System.Collections;
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
>>>>>>> 014dd1495059547113de6aebf7610bfbee4f9a10:Assets/Scripts/Enemy Scripts/SkeletonSwordAI/SkeletonAI.cs
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
<<<<<<< HEAD:Assets/Scripts/Enemy Scripts/SkeletonAI.cs
        }

        //Movement and attack
        if (System.Math.Abs(player.position.x - transform.position.x) > AttackRangeX)
=======
        }

        //Movement and attack
<<<<<<< HEAD
        if (System.Math.Abs(player.position.x - transform.position.x) > playerRange)
>>>>>>> 014dd1495059547113de6aebf7610bfbee4f9a10:Assets/Scripts/Enemy Scripts/SkeletonSwordAI/SkeletonAI.cs
        {
            Move();
        }
        else if (System.Math.Abs(player.position.y - transform.position.y) < AttackRangeY)
=======
        if (System.Math.Abs(player.position.x - transform.position.x) > attackRange)
        {
            Move();
        }
        else if (System.Math.Abs(player.position.y - transform.position.y) < attackRange)
>>>>>>> 9e1077ef8fc3c104c28870b4167f07a25740895e
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
<<<<<<< HEAD:Assets/Scripts/Enemy Scripts/SkeletonAI.cs
        timeBetweenAttack -= Time.deltaTime;
    }

    //Skeleton attack method
=======
        timeBetweenAttack -= Time.deltaTime;
        
    }

    //Skeleton attack method
>>>>>>> 014dd1495059547113de6aebf7610bfbee4f9a10:Assets/Scripts/Enemy Scripts/SkeletonSwordAI/SkeletonAI.cs
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
<<<<<<< HEAD:Assets/Scripts/Enemy Scripts/SkeletonAI.cs
    }

    //Skeleton walk method
=======

    }

>>>>>>> 014dd1495059547113de6aebf7610bfbee4f9a10:Assets/Scripts/Enemy Scripts/SkeletonSwordAI/SkeletonAI.cs
    void Move()
    {
        animator.SetFloat("Action", 1);
        float step = Time.deltaTime * 2.5f * speed;
        transform.position = Vector3.MoveTowards(transform.position, player.position, step);
<<<<<<< HEAD:Assets/Scripts/Enemy Scripts/SkeletonAI.cs
    }

    //Skeleton idle method
    void Idle()
    {
        animator.SetFloat("Action", 0);
    }

    //Skeleton hitbox display
=======
    }

    void Idle()
    {
        animator.SetFloat("Action", 0);

    }

>>>>>>> 014dd1495059547113de6aebf7610bfbee4f9a10:Assets/Scripts/Enemy Scripts/SkeletonSwordAI/SkeletonAI.cs
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

}