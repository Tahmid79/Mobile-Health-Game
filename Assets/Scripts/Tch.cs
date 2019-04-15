using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tch : MonoBehaviour
{
	public Camera cam;
	 Playerfight playerfight;
	 Vector2 startPos;
	 Vector2 direction;

	Touch touch;


	void Start()
	{
		playerfight = GetComponent<Playerfight>(); 
	}

	void Update()
	{
		if (Input.touchCount > 0)
		{

			//Debug.Log("Touch Detected");
			 touch = Input.GetTouch(0);
		
			if(Input.touchCount == 2)
				 touch = Input.GetTouch(1);
			
			
			Vector3 touch_pos = cam.ScreenToWorldPoint(touch.position);

			switch (touch.phase)
			{
				//When a touch has first been detected, change the message and record the starting position
				case TouchPhase.Began:
					// Record initial touch position.
					startPos = touch.position;

					break;

				//Determine if the touch is a moving touch
				case TouchPhase.Moved:
					// Determine direction by comparing the current touch position with the initial one
					//Debug.Log("Touch Moving");
					direction = touch.position - startPos;

					if (direction.x > 0)
					{
						startPos.x = touch.position.x - 0.5f;
					}

					if (direction.x < 0)
					{
						startPos.x = touch.position.x + 0.5f;
					}

					dir_det();
					
					break;

				case TouchPhase.Ended:
					// Report that the touch has ended when it ends	

					break;

			}

		}

	}

	void dir_det()
	{

		if (direction.x > 0)
		{
			playerfight.btn_rot_rt();
		}

		if (direction.x < 0)
		{
			playerfight.btn_rot_lft();
		}
	}



}
