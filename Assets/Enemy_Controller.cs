﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_Controller : MonoBehaviour {

    public float lookRadius = 5f;
    public Rigidbody rb;

     public float timeBetweenAttacks = 0.5f;     // The time in seconds between each attack.
    public int attackDamage = 1;               // The amount of health taken away per attack.


    Animator anim;                              // Reference to the animator component.
     public Animator Eanim;                             // Reference to the animator component.
    GameObject Walking;  
    GameObject Enemy ;                          // Reference to the player GameObject.
    PlayerHealth playerHealth;                  // Reference to the player's health.
    bool playerInRange;                         // Whether player is within the trigger collider and can be attacked.
    float timer;



    Transform target;

   
    // Use this for initialization
    void Start () {
        target = PlayerManager.instance.player.transform;
        Walking = GameObject.FindGameObjectWithTag ("Player");
        playerHealth = Walking.GetComponent <PlayerHealth> ();
        Enemy  =  GameObject.FindGameObjectWithTag ("Enemy");
        Eanim = Enemy.GetComponent<Animator> ();
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
        if(distance <= lookRadius && distance>2f )
      {
//Move the enemy towards the player with smoothdamp
         transform.position = Vector3.SmoothDamp(transform.position, player.position, ref smoothVelocity, smoothTime);
            Eanim.Play("Running");
        }else if(distance <= 2f)
        {     
           //    BackAway();  
          rb.velocity = new Vector3(0, 0, 0);


            Attack();

        }else
        {
            Eanim.Play("Idle");

        }
		
	}

      void Attack ()
    {
        // Reset the timer.
        timer = 0f;

        // If the player has health to lose...
        
            // ... damage the player.
            playerHealth.TakeDamage (attackDamage);
                                    Eanim.Play("Take 001");

        
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


}