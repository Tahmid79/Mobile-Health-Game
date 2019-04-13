using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reference : MonoBehaviour
{
	public GameObject player;
	Vector3 init_pos;
	public Quaternion originalRotationValue;

	// Start is called before the first frame update
	void Start()
    {
		player = GameObject.FindGameObjectWithTag("Player");
		init_pos = player.transform.position;
		originalRotationValue = transform.rotation;
	}

	// Update is called once per frame
	void Update()
	{ 
		transform.position = Vector3.MoveTowards(transform.position, player.transform.position, 10f);
		transform.rotation = Quaternion.Slerp(transform.rotation, originalRotationValue, 10f);
	}
}
