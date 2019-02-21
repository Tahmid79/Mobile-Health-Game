using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vibrate : MonoBehaviour {

    public GameObject Box;

    public float speed = 5f;

    private Vector3 startpos;
    private Vector3 endpos;

    private float distance = 15f;

    float lerptime = 8f;

    float currentlerptime = 0f;


	// Use this for initialization
	void Start () {
        startpos = Box.transform.position;
        endpos = Box.transform.position + Vector3.back * distance;
    }
	
	// Update is called once per frame
	void Update () {
        // PingPong();
       
        currentlerptime += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.A))
        {

            if (currentlerptime >= lerptime)
            {
                currentlerptime = lerptime;
            }
            float Perc = currentlerptime / lerptime;
            Box.transform.position = Vector3.Lerp(startpos, endpos, Perc);

        }

    }

    void Lerp()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
          
            if (currentlerptime >= lerptime)
            {
                currentlerptime = lerptime;
            }
            float Perc = currentlerptime / lerptime;
            Box.transform.position = Vector3.Lerp(startpos, endpos, Perc);

        }
    }


    void PingPong()
    {
        float x = Mathf.PingPong(Time.time * speed, 15);
        transform.position = new Vector3(x, transform.position.y, transform.position.z);
    }




}



