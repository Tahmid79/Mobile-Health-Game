using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_MOV : MonoBehaviour {

    public Rigidbody rb;
    public float fwdforce = 5f;
    public float swdforce = 5f;
    public Quaternion originalRotationValue;

    Animator anim;
    Vector3 ip;

  
    // Use this for initialization
    void Start () {

        anim = GetComponent<Animator>();
        ip = transform.position;
        originalRotationValue = transform.rotation; // save the initial rotation
    }

    // Update is called once per frame
    void FixedUpdate () {
		if (Input.GetKey("d"))
		{
			
			transform.Rotate(Vector3.up * 80f * Time.deltaTime);	
		}



		if (Input.GetKey("a"))
		{
			transform.Rotate(-Vector3.up * 80f * Time.deltaTime);	
		}


		if (Input.GetKey("w"))
		{
			     
			Vector3 dir = transform.rotation * Vector3.forward;
			
			rb.AddForce(dir * fwdforce * Time.deltaTime, ForceMode.VelocityChange);
			anim.Play("Take 001");
		}


		if (Input.GetKey("s"))
		{

			Vector3 dir = transform.rotation * Vector3.back;
			rb.AddForce(dir * fwdforce * Time.deltaTime, ForceMode.VelocityChange);
			
			anim.Play("Take 001");
			
		}


	}

    void Update()
    {
        if (Input.GetKeyUp("a") || Input.GetKeyUp("s") || Input.GetKeyUp("d") || Input.GetKeyUp("w"))
        {

            anim.Play("Idle");
            rb.velocity = new Vector3(0, 0, 0);
        }
    }

}
