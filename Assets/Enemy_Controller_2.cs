using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_Controller_2 : MonoBehaviour {

	public float speed;
	public Transform[] movespots;
	private int randomSpot;

	private float waitTime;
	public float start_Wait_time;

	private float distance;
	private float player_distance;

	Rigidbody rb;

	/// //////////////////////////////////////////////////////////////

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

		//////////////////////////////////////////////////////

		waitTime = start_Wait_time;
		randomSpot = Random.Range(0, movespots.Length);

		rb = GetComponent<Rigidbody>();

	}
	
	// Update is called once per frame
	void Update () {

		randomize();
		timer += Time.deltaTime;

		player_distance = Vector3.Distance(target.position, transform.position);

		distance = Vector3.Distance(movespots[randomSpot].position, transform.position);


		if (distance <= lookRadius )//&& distance > 3f //&& backing == false)
		{
			MoveTowards();
		}

	
		else if (player_distance < 3f)
		{
			//backing = true;
			// agent.SetDestination(target.position);
			//BackAway();

			if (timer >= timeBetweenAttacks)
			{
				
				// ... attack.
				Attack();
			}
			//backing = false;

		}

		rb.AddForce(Physics.gravity * rb.mass * 3f); //gravity

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
		// transform.LookAt(target.position);

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


}
