using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_Controller : MonoBehaviour {

    public float lookRadius = 5f;

     public float timeBetweenAttacks = 0.5f;     // The time in seconds between each attack.
    public int attackDamage = 5;               // The amount of health taken away per attack.


    Animator anim; 
   public Animator Eanim;                             // Reference to the animator component.
    GameObject Walking;  
    GameObject Enemy     ;                   // Reference to the player GameObject.
    PlayerHealth playerHealth;                  // Reference to the player's health.
    bool playerInRange;                         // Whether player is within the trigger collider and can be attacked.
    float timer;



    Transform target;
    NavMeshAgent agent;
   

    // Use this for initialization
    void Start () {
        
        Enemy  =  GameObject.FindGameObjectWithTag ("Enemy");
        Eanim = Enemy.GetComponent<Animator> (); 
        
        target = PlayerManager.instance.player.transform;
        Walking = GameObject.FindGameObjectWithTag ("Player");

        playerHealth = Walking.GetComponent <PlayerHealth> ();
        agent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
                timer += Time.deltaTime;

        float distance;
        distance = Vector3.Distance(target.position, transform.position);

        if(distance <= lookRadius)
        {
            agent.SetDestination(target.position);
                        Eanim.Play("Running");

            if(timer >= timeBetweenAttacks){
                // ... attack.
          Eanim.Play("Take 001");

            Attack ();
            }
        }
		
	}

      void Attack ()
    {
        // Reset the timer.
        timer = 0f;

        // If the player has health to lose...
        
            // ... damage the player.
            playerHealth.TakeDamage (attackDamage);
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
