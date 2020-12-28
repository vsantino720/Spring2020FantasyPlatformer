using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{

    [SerializeField] private Animator animator;

    [SerializeField] private Transform attackpoint;
    [SerializeField] private int maxHealth = 6;
    [SerializeField] private int currentHealth;
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private float attackRange = 0.5f;
    [SerializeField] private float attackRate = 2f;
    [SerializeField] private float nextAttackTime = 0f;
    [SerializeField] private LayerMask enemyLayers;
    [SerializeField] private int attackDamage = 2;
    [SerializeField] private float dazedTime;
    [SerializeField] private float startDazedTime = 0.6f;
    [SerializeField] private float knockback = 10;
    [SerializeField] private bool knockFromRight;

    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxValue(maxHealth); 
    }
    // Update is called once per frame
    void Update()
    {
        if(dazedTime <= 0)
        {
            GetComponent<PlayerMovement>().Daze(false);
        } 
        else
        {
            GetComponent<PlayerMovement>().Daze(true);
            animator.SetFloat("Speed", 0);
            dazedTime -= Time.deltaTime;
        }
        if (Time.time <= nextAttackTime)
        {
            GetComponent<PlayerMovement>().setAttacking(true);
        }
        if (Time.time >= nextAttackTime)
        {
            GetComponent<PlayerMovement>().setAttacking(false);
            if (Input.GetKeyDown(KeyCode.F) && animator.GetBool("IsJumping") == false && !GetComponent<PlayerMovement>().getDazed())
            {
                //Play attack animation
                animator.SetTrigger("Attack");
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }
    public void TakeDamage(int damage)
    {
        Debug.Log("Took" + damage + "Damage");
        dazedTime = startDazedTime; //Initialize dazed countdown
        currentHealth -= damage;
        animator.SetTrigger("Hurt");
        if (knockFromRight)
        {
            //GetComponent<Rigidbody2D>().AddForce(new Vector2(-knockback, knockback));
            GetComponent<Rigidbody2D>().velocity = new Vector2(-2f * knockback, knockback);
        }
        else
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(2f * knockback, knockback);
            //GetComponent<Rigidbody2D>().AddForce(new Vector2(knockback, knockback)); 
        }

        //Changes UI Health Bar
        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }
    public void Die()
    {
        //Play Death animation
        animator.SetBool("IsDead", true);
        Debug.Log("You Died");
        GetComponent<PlayerMovement>().enabled = false;
        this.enabled = false;
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Rigidbody2D>().simulated = false;
        //Restart game from beginning
    }

    public void Attack()
    {
        //Detect enemies in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackpoint.position, attackRange, enemyLayers);
        //Damage enemies
        foreach(Collider2D enemy in hitEnemies)
        {
            if (enemy.transform.position.x < transform.position.x)
            {
                enemy.GetComponent<SkeletonSwordBehavior>().knockFromRight = true;
            }
            else
            {
                enemy.GetComponent<SkeletonSwordBehavior>().knockFromRight = false;
            }
            enemy.GetComponent<SkeletonSwordBehavior>().TakeDamage(attackDamage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackpoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackpoint.position, attackRange);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Checks that this is the health potion object
        if (other.gameObject.tag.Equals("HealthPotion"))
        {
            //Makes sure the player will not gain health above max health
            if (currentHealth < maxHealth)
            {
                this.currentHealth += 1;
                //Updates UI
                healthBar.SetHealth(currentHealth);
                //Disables Health potion object
                other.gameObject.SetActive(false);
            }
        }
    }

    public void knockRight()
    {
        knockFromRight = true;
    }

    public void knockLeft()
    {
        knockFromRight = false;
    }
}
