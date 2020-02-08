﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{

    public Animator animator;

    public Transform attackpoint;
    public int maxHealth = 12;
    int currentHealth;
    public float attackRange = 0.5f;
    public float attackRate = 2f;
    float nextAttackTime = 0f;
    public LayerMask enemyLayers;
    public int attackDamage = 2;
    private void Start()
    {
        currentHealth = maxHealth;
    }
    // Update is called once per frame
    void Update()
    {
        if(Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.F) && animator.GetBool("IsJumping") == false)
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }
    public void TakeDamage(int damage)
    {
        Debug.Log("Took" + damage + "Damage");
        currentHealth -= damage;
        animator.SetTrigger("Hurt");
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        //Play Death animation
        animator.SetBool("IsDead", true);
        Debug.Log("You Died");
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Rigidbody2D>().simulated = false;
        GetComponent<PlayerMovement>().enabled = false;
        this.enabled = false;
        //Restart game from beginning
    }

    void Attack()
    {
        //Play attack animation
        animator.SetTrigger("Attack");
        //Detect enemies in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackpoint.position, attackRange, enemyLayers);
        //Damage enemies
        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<SkeletonSwordBehavior>().TakeDamage(attackDamage);
        }
        //Freeze Player for extent of the animation?
    }

    private void OnDrawGizmosSelected()
    {
        if (attackpoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackpoint.position, attackRange);
    }
}
