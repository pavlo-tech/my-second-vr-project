using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 0.5f;
    public int attackDamage = 10;


    Animator anim;
    GameObject player;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    bool playerInRange;
    float timer;


    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("PlayerBody");
        playerHealth = player.GetComponent<PlayerHealth>();
        enemyHealth = GetComponent<EnemyHealth>();
        anim = GetComponent<Animator>();
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            playerInRange = true;
            anim.SetBool("playerInRange", playerInRange);
        }
    }

    private void OnTriggerStay(Collider other)
    {
    }


    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            playerInRange = false;
            anim.SetBool("playerInRange", playerInRange);
        }
    }


    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= timeBetweenAttacks && playerInRange && enemyHealth.currentHealth > 0)
        {
            Attack();
        }

        if (playerHealth.currentHealth <= 0)
        {
            anim.SetTrigger("PlayerDead");
        }
    }


    void Attack()
    {
        timer = 0f;

        if (playerHealth.currentHealth > 0)
        {
            playerHealth.TakeDamage(attackDamage);
        }
    }
}


//public float timeBetweenAttacks = 0.5f;
//public int attackDamage = 10;


//Animator anim;
//GameObject player;
//PlayerHealth playerHealth;
//EnemyHealth enemyHealth;
//bool playerInRange;
//float timer;


//void Awake ()
//{
//    player = GameObject.FindGameObjectWithTag ("Player");
//    playerHealth = player.GetComponent <PlayerHealth> ();
//    enemyHealth = GetComponent<EnemyHealth>();
//    anim = GetComponent <Animator> ();
//}


//void OnTriggerEnter (Collider other)
//{
//    if (other.gameObject == player)
//    //if(other.gameObject.tag == "PlayerBody")
//    {
//        playerInRange = true;
//        anim.SetBool("playerInRange", playerInRange);
//    }
//}

//private void OnTriggerStay(Collider other)
//{
//  //  if(other.gameObject == player)
//    //    anim.SetTrigger("AttackPlayer");
//}


//void OnTriggerExit (Collider other)
//{
//    if(other.gameObject == player)
//    {
//        playerInRange = false;
//        anim.SetBool("playerInRange", playerInRange);
//    }
//}


//void Update ()
//{
//    timer += Time.deltaTime;

//    if(timer >= timeBetweenAttacks && playerInRange && enemyHealth.currentHealth > 0)
//    {
//        Attack ();
//    }

//    if(playerHealth.currentHealth <= 0)
//    {
//        anim.SetTrigger ("PlayerDead");
//    }
//}


//void Attack ()
//{
//    timer = 0f;

//    if(playerHealth.currentHealth > 0)
//    {
//        playerHealth.TakeDamage (attackDamage);
//    }
//}