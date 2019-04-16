using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mn_stg_mov : MonoBehaviour
{
	public Rigidbody rb;
	public float fwdforce = 5f;
	public float swdforce = 5f;
	public Quaternion originalRotationValue;

	Animator anim;
	Vector3 ip;


	// Start is called before the first frame update
	void Start()
    {
		anim = GetComponent<Animator>();
		ip = transform.position;
		originalRotationValue = transform.rotation; // save the initial rotation
	}

	private void FixedUpdate()
	{
		Mv_lft();
		Mv_rt();
		Mv_dwn();
		Mv_fwd();

		
	}

	// Update is called once per frame
	void Update()
    {
		Scn_Mngr();
		Ky_rls();
	}


	void Scn_Mngr()
	{
		//Go to the quiz if escape is pressed
		if (Input.GetKey(KeyCode.Escape))
		{
			SceneManager.LoadScene(0);
		}

		//Go to inventory
		if (Input.GetKey("p"))
		{
			SceneManager.LoadScene(5);
		}

		//Go to the boss fight if spacebar is pressed
		if (Input.GetKey(KeyCode.Space))
		{
			SceneManager.LoadScene(4);
		}
	}


/// ///////////////////////////////////////////////////////////////////////////////
// Movement Keyboard//


	public void Ky_rls()
	{
		if (Input.GetKeyUp("a") || Input.GetKeyUp("s") || Input.GetKeyUp("d") || Input.GetKeyUp("w"))
		{
			Stp_Mvmnt();
		}
	}

	public void Stp_Mvmnt()
	{
		anim.Play("Idle");
		rb.velocity = new Vector3(0, 0, 0);
	}

	public void Mv_lft()
	{
		if (Input.GetKey("a"))
		{

			transform.rotation = Quaternion.Slerp(transform.rotation, originalRotationValue, 10f);
			float maxangle = 90f;
			rb.AddForce(-swdforce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
			anim.Play("Running");
			if (transform.eulerAngles.magnitude < maxangle)

				transform.Rotate(-Vector3.up * 90f);
		}

	}

	public void Mv_rt()
	{
		if (Input.GetKey("d"))
		{
			transform.rotation = Quaternion.Slerp(transform.rotation, originalRotationValue, 10f);
			float maxangle = 90f;
			rb.AddForce(swdforce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
			anim.Play("Running");
			if (transform.eulerAngles.magnitude < maxangle)
				transform.Rotate(Vector3.up * 90f);

		}
	}

	public void Mv_fwd()
	{
		if (Input.GetKey("w"))
		{
			transform.rotation = Quaternion.Slerp(transform.rotation, originalRotationValue, 10f);
			rb.AddForce(0, 0, fwdforce * Time.deltaTime, ForceMode.VelocityChange);
			anim.Play("Running");
		}
	}

	public void Mv_dwn()
	{
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

	///////////////////////////////////////////////////////////////////////////////////
	//Button Movement//

	public void Btn_Mv_lft()
	{
		
			transform.rotation = Quaternion.Slerp(transform.rotation, originalRotationValue, 10f);
			float maxangle = 90f;
			rb.AddForce(-swdforce , 0, 0, ForceMode.VelocityChange);
			anim.Play("Running");
			if (transform.eulerAngles.magnitude < maxangle)

				transform.Rotate(-Vector3.up * 90f);		

	}

	public void Btn_Mv_rt()
	{
			transform.rotation = Quaternion.Slerp(transform.rotation, originalRotationValue, 10f);
			float maxangle = 90f;
			rb.AddForce(swdforce , 0, 0, ForceMode.VelocityChange);
			anim.Play("Running");
			if (transform.eulerAngles.magnitude < maxangle)
				transform.Rotate(Vector3.up * 90f);

	
	}

	public void Btn_Mv_fwd()
	{
		
			transform.rotation = Quaternion.Slerp(transform.rotation, originalRotationValue, 10f);
			rb.AddForce(0, 0, fwdforce , ForceMode.VelocityChange);
			anim.Play("Running");
	
	}

	public void Btn_Mv_dwn()
	{
		
			transform.rotation = Quaternion.Slerp(transform.rotation, originalRotationValue, 10f);

			float maxangle = 180f;
			rb.AddForce(0, 0, -fwdforce , ForceMode.VelocityChange);
			anim.Play("Running");

			if (transform.eulerAngles.magnitude < maxangle)
				transform.Rotate(Vector3.up * 180f);
	
	}


}
