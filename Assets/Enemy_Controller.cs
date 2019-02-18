using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_Controller : MonoBehaviour {

    public float lookRadius = 10f;

    Transform target;
    NavMeshAgent agent;

   
    // Use this for initialization
    void Start () {
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
        float distance;
        distance = Vector3.Distance(target.position, transform.position);

        if(distance <= lookRadius && distance>2f )
        {
            transform.LookAt(target.position);
            transform.Rotate(new Vector3(0, -90, 0), Space.Self);


            //agent.SetDestination(target.position);
            float speed = 4f;
            transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
        }

        if(distance < 2f)
        {     
            BackAway();     
        }
		
	}

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

    void BackAway()
    {
        transform.Translate(Vector3.back * 4f * Time.deltaTime);
    }


}
