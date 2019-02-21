using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_Controller : MonoBehaviour {

    public float lookRadius = 10f;

    public float timeBetweenAttacks = 0.5f;     // The time in seconds between each attack.
    public int attackDamage = 5;               // The amount of health taken away per attack.


    Animator anim;                              // Reference to the animator component.
    GameObject Walking;                          // Reference to the player GameObject.
    PlayerHealth playerHealth;                  // Reference to the player's health.
    bool playerInRange;                         // Whether player is within the trigger collider and can be attacked.
    float timer;



    Transform target;
    NavMeshAgent agent;

    bool backing;
    int flg = 0;


    // Use this for initialization
    void Start () {
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


        

        if(distance <= lookRadius && distance>3f && backing==false)
        {
          
            MoveTowards();
          
        }

        else if(distance < 2f  )
        {
            backing = true;
            agent.SetDestination(target.position);
            BackAway();     

            if(timer >= timeBetweenAttacks){
                // ... attack.
            Attack ();
            }
            backing = false;
          
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

    void BackAway()
    {
        transform.Translate(Vector3.back * 4f * Time.deltaTime);
    }

    void MoveTowards()
    {
        transform.LookAt(target.position);
        transform.Rotate(new Vector3(0, -90, 0), Space.Self);


        //agent.SetDestination(target.position);
        float speed = 4f;
        transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));


    }


}
