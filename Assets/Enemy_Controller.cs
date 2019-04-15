using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_Controller : MonoBehaviour {

    public float lookRadius = 5f;
    public float distance_to_attack = 10f;
    public Rigidbody rb;
    AudioSource playerAudio;                                    // Reference to the AudioSource component.

     public float timeBetweenAttacks = 2f;     // The time in seconds between each attack.
    public int attackDamage = 1;               // The amount of health taken away per attack.
    public AudioClip slashClip;                                 // The audio clip to play when the player dies.


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
        Enemy  =  GameObject.FindGameObjectWithTag ("Enemy");
        Eanim = Enemy.GetComponent<Animator> ();     
        playerAudio = Walking.GetComponent <AudioSource> ();


	}
//The target player
 public Transform player;
 
 //In what time will the enemy complete the journey between its position and the players position
 public float smoothTime = 2f;
 //Vector3 used to store the velocity of the enemy
 private Vector3 smoothVelocity = Vector3.zero;


 //Call every frame
	void Update () {
        timer += Time.deltaTime;

        //Look at the player
     transform.LookAt(player);
     //Calculate distance between player
     float distance = Vector3.Distance(transform.position, player.position);
     //If the distance is smaller than the walkingDistance
        if(distance <= lookRadius && distance>distance_to_attack )
      {
        
           
        //Move the enemy towards the player with smoothdamp
         transform.position = Vector3.SmoothDamp(transform.position, player.position, ref smoothVelocity, smoothTime);
            Eanim.Play("Mutant Run");
        
        
        
        }else if(distance <= distance_to_attack)
        {     
           //    BackAway();  
          rb.velocity = new Vector3(0, 0, 0);

            if(timer >=timeBetweenAttacks){
                    Eanim.Play("Mutant Swiping");


            }

        }else
        {
            Eanim.Play("Mutant Idle");

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
       //  playerAudio.clip = slashClip;
       // playerAudio.Play ();
            // ... damage the player.
                                                playerHealth.TakeDamage (attackDamage);


        
    }
     void DealDamage(){
                timer = 0f;
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

/* 
    void randomize()
	{

		if (distance < 0.8f)
		{
			if (waitTime <= 0)
			{
				randomSpot = Random.Range(0, movespots.Length);
				waitTime = start_Wait_time;
				
			}
			else
			{
				waitTime -= Time.deltaTime;
				
			}

		}



	}
*/




}
