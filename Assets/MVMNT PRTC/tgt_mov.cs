using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tgt_mov : MonoBehaviour {

    public Rigidbody rb;

    public Transform[] target;

    private int current=0;
    public float speed = 5f;


	// Use this for initialization
	void Start () {
		
     


	}
	
	// Update is called once per frame
	void Update () {
        if (transform.position != target[current].position)
        {
            Vector3 pos = Vector3.MoveTowards(transform.position, target[current].position, speed * Time.deltaTime);
            rb.MovePosition(pos);
        }
        else
        {
            current = (current + 1) % target.Length;
        }
    }
}
