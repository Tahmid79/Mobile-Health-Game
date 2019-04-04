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


    // Use this for initialization
    void Start()
    {

        anim = GetComponent<Animator>();
        ip = transform.position;
        originalRotationValue = transform.rotation; // save the initial rotation
		anim.Play("Idle");
	}

    // Update is called once per frame
    void FixedUpdate()
    {

        if (Input.GetKey("d"))
        {
			//transform.rotation = Quaternion.Slerp(transform.rotation, originalRotationValue, 10f);
			//float maxangle = 90f;
			//rb.AddForce(swdforce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
			
			//anim.Play("Running");
			transform.Rotate(Vector3.up * 80f * Time.deltaTime);
			//anim.Play("Idle");

			/*
			if (transform.eulerAngles.magnitude < maxangle)
			{
				transform.Rotate(Vector3.up * 90f);
				//Cam.transform.Rotate(Vector3.up, 20f * Time.deltaTime);
			}
			*/
		}



        if (Input.GetKey("a"))
        {

            //transform.rotation = Quaternion.Slerp(transform.rotation, originalRotationValue, 10f);
            //float maxangle = 90f;
            //rb.AddForce(-swdforce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
            //anim.Play("Idle");
			transform.Rotate(-Vector3.up * 80f * Time.deltaTime);
			/*
			if (transform.eulerAngles.magnitude < maxangle)
                transform.Rotate(-Vector3.up * 90f);
			*/
		}


        if (Input.GetKey("w"))
        {
            //transform.rotation = Quaternion.Slerp(transform.rotation, originalRotationValue, 10f);           
             Vector3 dir = transform.rotation * Vector3.forward ;
            //rb.AddForce(0, 0, fwdforce * Time.deltaTime, ForceMode.VelocityChange);    
            rb.AddForce(dir * fwdforce *Time.deltaTime, ForceMode.VelocityChange) ;           
            anim.Play("Running");
        }


        if (Input.GetKey("s"))
        {

			Vector3 dir = transform.rotation * Vector3.back;
			rb.AddForce(dir * fwdforce * Time.deltaTime, ForceMode.VelocityChange);
			//transform.rotation = Quaternion.Slerp(transform.rotation, originalRotationValue, 10f);
			//float maxangle = 180f;
			//rb.AddForce(0, 0, -fwdforce * Time.deltaTime, ForceMode.VelocityChange);
			anim.Play("Running");		
			/*
            if (transform.eulerAngles.magnitude < maxangle)
                transform.Rotate(Vector3.up * 80f * Time.deltaTime);
			*/
        }

		rb.AddForce(Physics.gravity * rb.mass * 3f); //gravity

	}

    void Update()
    {
        if (Input.GetKeyUp("a") || Input.GetKeyUp("s") || Input.GetKeyUp("d") || Input.GetKeyUp("w"))
        {

            anim.Play("Idle");
            rb.velocity = new Vector3(0, 0, 0);
			//Cam.transform.rotation = Quaternion.Slerp(transform.rotation, originalRotationValue, 3f*Time.deltaTime);
		}

        if (Input.GetKey(KeyCode.Z))
        {
            anim.Play("Take 001");
        }

		


	}

}
