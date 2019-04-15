using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Motion : MonoBehaviour {

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

        //Go to the quiz if escape is pressed
        if(Input.GetKey(KeyCode.Escape)){
            SceneManager.LoadScene(0);
        }

        //Go to inventory
        if(Input.GetKey("p")){
            SceneManager.LoadScene(5);
        }

        //Go to the boss fight if spacebar is pressed
        if(Input.GetKey(KeyCode.Space)){
            SceneManager.LoadScene(4);
        }

        if (Input.GetKey("d"))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, originalRotationValue, 10f);
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
            rb.AddForce(0, 0, fwdforce*Time.deltaTime, ForceMode.VelocityChange);
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
        if (Input.GetKeyUp("a") || Input.GetKeyUp("s") || Input.GetKeyUp("d") || Input.GetKeyUp("w"))
		{ 
          //  anim.Play("Idle");
            rb.velocity = new Vector3(0, 0, 0);
        }
    }

}
