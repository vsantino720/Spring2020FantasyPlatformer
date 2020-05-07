using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowmanBehavior : MonoBehaviour
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
    public int attackDamage = 1;
    private float dazedTime;
    public float startDazedTime = 0.6f;
    public float knockback;
    public bool knockFromRight;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
