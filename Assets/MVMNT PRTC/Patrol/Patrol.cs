using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{

	public float speed;
	public Transform[] movespots;
	private int randomSpot;

	private float waitTime;
	public float start_Wait_time;

    // Start is called before the first frame update
    void Start()
    {
		waitTime = start_Wait_time;
		randomSpot = Random.Range(0, movespots.Length);
    }

    // Update is called once per frame
    void Update()
    {
		transform.position = Vector3.MoveTowards(transform.position, movespots[randomSpot].position, speed*Time.deltaTime) ;

		float distance;
		distance = Vector3.Distance(transform.position, movespots[randomSpot].position);

		if(  distance < 0.2f)
		{	
			if(waitTime <= 0)
			{
				randomSpot = Random.Range(0, movespots.Length);
				waitTime = start_Wait_time;
			}
			else
			{
				waitTime -= Time.deltaTime;
			}

		}


	}
}
