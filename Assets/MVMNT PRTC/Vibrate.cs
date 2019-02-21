using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vibrate : MonoBehaviour {

    public float speed = 5f;

    private Vector3 startpos;
    private Vector3 endpos;

    private float distance = 7f;

    float lerptime = 5f;

    float currentlerptime = 0f;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
    // PingPong();

	}






    void PingPong()
    {
        float x = Mathf.PingPong(Time.time * speed, 15);
        transform.position = new Vector3(x, transform.position.y, transform.position.z);
    }




}



