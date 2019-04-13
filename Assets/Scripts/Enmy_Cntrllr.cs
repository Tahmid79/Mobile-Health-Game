using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enmy_Cntrllr : MonoBehaviour {

	public float speed;
	public Transform[] movespots;
	private int randomSpot;

	private float waitTime;
	public float start_Wait_time;
	private bool waiting;

	private float distance;
	private float player_distance;

	

	/// //////////////////////////////////////////////////////////////

	public float lookRadius = 5f;
	public float distance_to_attack = 10f;
	public Rigidbody rb;
	AudioSource playerAudio;                                    // Reference to the AudioSource component.

	public float timeBetweenAttacks = 2f;     // The time in seconds between each attack.
	public int attackDamage = 1;               // The amount of health taken away per attack.
	public AudioClip slashClip;                                 // The audio clip to play when the player dies.


	Animator anim;                              // Reference to the animator component.
	public Animator Eanim;                             // Reference to the animator component.
	GameObject Walking;
	GameObject Enemy;                          // Reference to the player GameObject.
	PlayerHealth playerHealth;                  // Reference to the player's health.
	bool playerInRange;                         // Whether player is within the trigger collider and can be attacked.
	float timer;


	Transform target;
	NavMeshAgent agent;


	/*//////////////////////////////////////////////////////////////*/


	public Transform player;        //The target player


	public float smoothTime = 2f;       //In what time will the enemy complete the journey between its position and the players position

	private Vector3 smoothVelocity = Vector3.zero;      //Vector3 used to store the velocity of the enemy


	// Use this for initialization
	void Start () {
        target = PlayerManager.instance.player.transform;
        Walking = GameObject.FindGameObjectWithTag ("Player");
        playerHealth = Walking.GetComponent <PlayerHealth> ();
        agent = GetComponent<NavMeshAgent>();
		
		//////////////////////////////////////////////////////

		waitTime = start_Wait_time;
		randomSpot = Random.Range(0, movespots.Length);

		rb = GetComponent<Rigidbody>();
		waiting = false;

	}
	
	// Update is called once per frame
	void Update () {

		Calculate();

		randomize();

		
		if (distance <= lookRadius && waiting==false )   
		{
			MoveTowards();
		}

	
		else if ( waiting ) //&& player_distance < 10f  )
		{		
			Attack();
			waiting = false;
		}
	
		else //if( !waiting )
		{
			Eanim.Play("Mutant Idle");
		}

	}
	


      void Attack ()
    {

		if (timer >= timeBetweenAttacks)
		{
			transform.LookAt(player);
			rb.velocity = new Vector3(0, 0, 0);

			Eanim.Play("Mutant Swiping");
			// Reset the timer.
			timer = 0f;

			// If the player has health to lose...

			// ... damage the player.
			playerHealth.TakeDamage(attackDamage);
		}
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
		// transform.LookAt(target.position);
		Eanim.Play("Mutant Run");
		agent.SetDestination(movespots[randomSpot].position);

		/*
		transform.LookAt(movespots[randomSpot].position);
		transform.Rotate(new Vector3(0, -90, 0), Space.Self);

        float speed = 4f;
        transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
		*/

    }

	void randomize()
	{

		if (distance < 2.8f)
		{
			if (waitTime <= 0)
			{
				waiting = false;
				randomSpot = Random.Range(0, movespots.Length);
				waitTime = start_Wait_time;
			}
			else
			{		
				waitTime -= Time.deltaTime;
				waiting = true;	
			}
		
		}

	}

	void Calculate()
	{
		timer += Time.deltaTime;

		player_distance = Vector3.Distance(target.position, transform.position);
		distance = Vector3.Distance(movespots[randomSpot].position, transform.position);

		rb.AddForce(Physics.gravity * rb.mass * 3f); //gravity

	}

}
