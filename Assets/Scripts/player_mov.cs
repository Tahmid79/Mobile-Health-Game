using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_mov : MonoBehaviour
{
    public int attackDamage = 1;               // The amount of health taken away per attack.

    public Rigidbody rb;
    GameObject Enemy;  
    EnemyHealth EnemyHealth;
    public float fwdforce = 25f;
    public float swdforce = 25f;
    public Quaternion originalRotationValue;

    Animator anim;
    Vector3 ip;


    // Use this for initialization
    void Start()
    {

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
            transform.rotation = Quaternion.Slerp(transform.rotation, originalRotationValue, 20f);
            float maxangle = 90f;
            rb.AddForce(swdforce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
            anim.Play("Running");
            if (transform.eulerAngles.magnitude < maxangle)
                transform.Rotate(Vector3.up * 90f);

        }



        if (Input.GetKey("a"))
        {

            transform.rotation = Quaternion.Slerp(transform.rotation, originalRotationValue, 10f);
            float maxangle = 90f;
            rb.AddForce(-swdforce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
            anim.Play("Running");
            if (transform.eulerAngles.magnitude < maxangle)
                transform.Rotate(-Vector3.up * 90f);

        }


        if (Input.GetKey("w"))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, originalRotationValue, 10f);
            rb.AddForce(0, 0, fwdforce * Time.deltaTime, ForceMode.VelocityChange);
            anim.Play("Running");
        }


        if (Input.GetKey("s"))
        {

            transform.rotation = Quaternion.Slerp(transform.rotation, originalRotationValue, 10f);

            float maxangle = 180f;
            rb.AddForce(0, 0, -fwdforce * Time.deltaTime, ForceMode.VelocityChange);
            anim.Play("Running");

            if (transform.eulerAngles.magnitude < maxangle)
                transform.Rotate(Vector3.up * 180f);
        }
        


    }

    void Update()
    {
         float distance = Vector3.Distance(transform.position, Enemypos.position);

        if (Input.GetKeyUp("a") || Input.GetKeyUp("s") || Input.GetKeyUp("d") || Input.GetKeyUp("w"))
        {

            anim.Play("Idle");
            rb.velocity = new Vector3(0, 0, 0);
        }

        if (Input.GetKey(KeyCode.Z))
        {
                        anim.Play("Boxing");

            if(distance<=2f){
            
            Attack ();
            }
        }
		if (Input.GetKey(KeyCode.X))
        {
            anim.Play("BreakDance");
        }


    }
     void Attack ()
    {
       ;

        // If the player has health to lose...
        
            // ... damage the player.
            EnemyHealth.TakeDamage (attackDamage);
                                  //  Eanim.Play("Take 001");

        
    }



}
