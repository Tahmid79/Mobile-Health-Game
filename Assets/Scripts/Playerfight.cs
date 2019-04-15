using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerfight : MonoBehaviour
{

    public Rigidbody rb;
    public float fwdforce = 5f;
    public float swdforce = 5f;
    public Quaternion originalRotationValue;
	public GameObject Cam;

    Animator anim;
    Vector3 ip;
	public EnemyHealth enemyHealth;
	public float timeBetweenAttacks = 2f;     // The time in seconds between each attack.
	public int attackDamage = 1;

	public GameObject Enemy;
	 float enemy_distance;

	public bool fwd_hold;
	public bool bck_hold;

	// Use this for initialization
	void Start()
    {

        anim = GetComponent<Animator>();
        ip = transform.position;
        originalRotationValue = transform.rotation; // save the initial rotation
		anim.Play("Idle");

		Enemy = GameObject.FindGameObjectWithTag("Enemy");
		enemyHealth = Enemy.GetComponent<EnemyHealth>();

		fwd_hold = false;
		bck_hold = false;

	}

    // Update is called once per frame
    void FixedUpdate()
    {

		rot_rt();
		rot_lft();
		mov_fwd();
		mov_bck();

		button_press();



		rb.AddForce(Physics.gravity * rb.mass * 3f); //gravity

	}

    void Update()
    {
		enemy_distance = Vector3.Distance(Enemy.transform.position, transform.position);

		KeyRelease();
		Attack();

		//Debug.Log(enemy_distance);
	
	}


	public void KeyRelease()
	{

		if (Input.GetKeyUp("a") || Input.GetKeyUp("s") || Input.GetKeyUp("d") || Input.GetKeyUp("w"))
		{
			StopMoving();
		}
	}

	public void Attack()
	{

		if (Input.GetKey(KeyCode.Z))
		{
			anim.Play("Slash");

			if (enemy_distance < 8f)
			{
				enemyHealth.TakeDamage(attackDamage);
			}
		}

	}

	public void rot_lft()
	{
		if (Input.GetKey("a"))
		{
			transform.Rotate(-Vector3.up * 80f * Time.deltaTime);
		}
	}

	public void rot_rt()
	{
		if (Input.GetKey("d"))
		{
			transform.Rotate(Vector3.up * 80f * Time.deltaTime);
		}
	}

	public void mov_fwd()
	{
		if (Input.GetKey("w") )
		{
			Vector3 dir = transform.rotation * Vector3.forward;
			rb.AddForce(dir * fwdforce * Time.deltaTime, ForceMode.VelocityChange);
			anim.Play("Running");
		}
	}

	public void mov_bck()
	{
		if (Input.GetKey("s"))
		{
			Vector3 dir = transform.rotation * Vector3.back;
			rb.AddForce(dir * fwdforce * Time.deltaTime, ForceMode.VelocityChange);
			anim.Play("Running");
		}
	}

	public void StopMoving()
	{
		anim.Play("Idle");
		rb.velocity = new Vector3(0, 0, 0);
	}


	/// ///////////////////////////////////////////////////////////////////////////////////////////////////

	//Without Keyboard Input


	public void btn_rot_lft()
	{		
		transform.Rotate(-Vector3.up * 5f );	
	}

	public void btn_rot_rt()
	{	
		transform.Rotate(Vector3.up * 5f );	
	}

	public void btn_mov_fwd()
	{		
			Vector3 dir = transform.rotation * Vector3.forward;
			rb.AddForce(dir * fwdforce * Time.deltaTime  , ForceMode.VelocityChange);
			anim.Play("Running");	
	}

	public void btn_mov_bck()
	{
			Vector3 dir = transform.rotation * Vector3.back;
			rb.AddForce(dir * fwdforce * Time.deltaTime , ForceMode.VelocityChange);
			anim.Play("Running");	
	}

	public void btn_attack()
	{	
			anim.Play("Slash");

			if (enemy_distance < 8f)
			{
				enemyHealth.TakeDamage(attackDamage);
			}
	}

	public void set_fwd_true()
	{
		fwd_hold = true;
	}

	public void set_fwd_false()
	{
		fwd_hold = false;
		StopMoving();
	}

	public void set_bck_true()
	{
		bck_hold = true;
	}

	public void set_bck_false()
	{
		bck_hold = false;
		StopMoving();
	}


	public void button_press()
	{
		if (fwd_hold)
		{
			btn_mov_fwd();
		}

		if (bck_hold)
		{
			btn_mov_bck();
		}

	}
	


}