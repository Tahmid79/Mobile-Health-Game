using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviour {

	GameObject startButton, quitButton;

	// Use this for initialization
	void Start () {

		//Initialize object references
		startButton = GameObject.Find("StartBtnLayer");
		quitButton = GameObject.Find("quitBtnLayer");

		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//Get the reference of the selected button
	public GameObject getSelectedButton(int selectedButtonIndex){
		GameObject btnSelected;
		switch(selectedButtonIndex){
			case 0:
			btnSelected = startButton;
			break;
			case 1:
			btnSelected = quitButton;
			break;
			default:
			btnSelected = startButton;
			break;
		}

		return btnSelected;
	}

	public void onStartBtnClick(){
		SceneManager.LoadScene(5);
	}
}
