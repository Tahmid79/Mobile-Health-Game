using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class onScreen : MonoBehaviour {
	public Button button;
    public int attackDamage = 1;               // The amount of health taken away per attack.
    float timer;
     public float timeBetweenAttacks = 1.5f;     // The time in seconds between each attack.
    AudioSource playerAudio;                                    // Reference to the AudioSource component.

    public Rigidbody rb;
    GameObject Enemy;  
    public float fwdforce = 25f;
    public float swdforce = 25f;
    public Quaternion originalRotationValue;
    public float rotation_speed = 80f;
 private bool walkUp;
    private bool walkLeft;
    private bool walkRight;
    private bool walkDown;
    Animator anim;
    Vector3 ip;
	// Use this for initialization
	void Start () {
	//	button.onClick.AddListener(forward);
		 anim = GetComponent<Animator>();
        Enemy = GameObject.FindGameObjectWithTag ("Enemy");
        ip = transform.position;
        originalRotationValue = transform.rotation; // sa
	}
	
	// Update is called once per frame
	void Update () {
		if (walkUp)
        {
            Vector3 dir = transform.rotation * Vector3.forward ;
            rb.AddForce(dir * fwdforce *Time.deltaTime, ForceMode.VelocityChange) ;           
            anim.Play("Running");
			Debug.Log("Button Pressed Forward ");
        }
        else if (walkLeft)
        {
            //GetComponent<Rigidbody2D> ().AddForce (-Vector2.right * speed);
        }
        else if (walkRight)
        {
            //GetComponent<Rigidbody2D>().AddForce(Vector2.right * speed);
        }
        else if (walkDown)
        {
            //GetComponent<Rigidbody2D>().AddForce(-Vector2.up * speed);
        }
	}
	public void forward(){

 Vector3 dir = transform.rotation * Vector3.forward ;
            rb.AddForce(dir * fwdforce *Time.deltaTime, ForceMode.VelocityChange) ;           
            anim.Play("Running");
			Debug.Log("Button Pressed Forward ");

	}
      public void PlayerWalkUp(int value){
        if (value == 1)
        {
            walkUp = true;
        }
        else
        {
            walkUp = false;
        }
 
    }
 
    public void PlayerWalkLeft(int value){
        if (value == 1)
        {
            walkLeft = true;
        }
        else
        {
            walkLeft = false;
        }
       
    }
 
    public void PlayerWalkRight(int value){
        if (value == 1)
        {
            walkRight = true;
        }
        else
        {
            walkRight = false;
        }
       
    }
 
    public void PlayerWalkDown(int value){
        if (value == 1)
        {
            walkDown = true;
        }
        else
        {
            walkDown = false;
        }
       
    }
}
