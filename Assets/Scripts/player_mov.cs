using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_mov : MonoBehaviour
{
    public int attackDamage = 1;               // The amount of health taken away per attack.
    float timer;
     public float timeBetweenAttacks = 1.5f;     // The time in seconds between each attack.
    AudioSource playerAudio;                                    // Reference to the AudioSource component.
    public AudioClip footstepclip;                                 // The audio clip to play when the player dies.

    public Rigidbody rb;
    GameObject Enemy;  
    EnemyHealth EnemyHealth;
    public float fwdforce = 25f;
    public float swdforce = 25f;
    public Quaternion originalRotationValue;
    public float rotation_speed = 80f;

    Animator anim;
    Vector3 ip;


    // Use this for initialization
    void Start()
    {
        playerAudio = this.GetComponent <AudioSource> ();

        anim = GetComponent<Animator>();
        Enemy = GameObject.FindGameObjectWithTag ("Enemy");
        EnemyHealth = Enemy.GetComponent <EnemyHealth> ();
        ip = transform.position;
        originalRotationValue = transform.rotation; // save the initial rotation
    }
 public Transform Enemypos;

    // Update is called once per frame
    void FixedUpdate()
    {
       if (Input.GetKey("d"))
        {
		
			transform.Rotate(Vector3.up * rotation_speed * Time.deltaTime);
			
		}



        if (Input.GetKey("a"))
        {

           
			transform.Rotate(-Vector3.up * rotation_speed * Time.deltaTime);
			
		}


        if (Input.GetKey("w"))
        {
             Vector3 dir = transform.rotation * Vector3.forward ;
            rb.AddForce(dir * fwdforce *Time.deltaTime, ForceMode.VelocityChange) ;           
            anim.Play("Running");
        }


        if (Input.GetKey("s"))
        {

			Vector3 dir = transform.rotation * Vector3.back;
			rb.AddForce(dir * fwdforce * Time.deltaTime, ForceMode.VelocityChange);
			anim.Play("Sword And Shield Walk");		
			
        }

		rb.AddForce(Physics.gravity * rb.mass * 3f); //gravity
        


    }

    void Update()
    {
                timer += Time.deltaTime;

         float distance = Vector3.Distance(transform.position, Enemypos.position);

        if (Input.GetKeyUp("a") || Input.GetKeyUp("s") || Input.GetKeyUp("d") || Input.GetKeyUp("w"))
        {

            anim.Play("Idle");
            rb.velocity = new Vector3(0, 0, 0);
        }

        if (Input.GetKey(KeyCode.Z))
        {
                        anim.Play("Slash");

            if(distance<=8f){
            
           if(timer >=timeBetweenAttacks){

            Attack();

            }
            }
        }
		if (Input.GetKey(KeyCode.X))
        {
            anim.Play("Dance");
        }


    }
     void Attack ()
    {
       ;

        // If the player has health to lose...
                timer = 0f;

            // ... damage the player.
            EnemyHealth.TakeDamage (attackDamage);
                                  //  Eanim.Play("Take 001");

        
    }
 void footstepsound  (float volume = 1f) {
    //Change the "SoundFileName" to the sound file name that you want to play that is located in the Assets/Resources folder.
				playerAudio.PlayOneShot (footstepclip,volume);

		//playerAudio.volume = volume;
	}


}
