using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {

	public Transform player;
	public Vector3 offset;
	Vector3 init_pos;
	

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = player.transform.position + offset;
		// transform.rotation = player.transform.rotation  ;
		//transform.Rotate(Vector3.up, 10f * Time.deltaTime);
		
	}
}
