using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupMenu : MonoBehaviour {

	// Use this for initialization
	public static bool playerWon = false;

	public GameObject PopupMenuUI;

	public GameObject GameOverUI;

	GameManager gameManager = new GameManager();

	//Get the score
	int sessionScore = 6;
	
	// Update is called once per frame
	void Update () {



		// Debug.Log("Popup UI called");

		// if(Input.GetKeyDown(KeyCode.Escape)){
		// 	Debug.Log("Display the popup");
		// 	displayPopup();
		// }
	}

	public void displayPopup(){

		Debug.Log("Popup displayed");

		PopupMenuUI.SetActive(true);
		GameOverUI.SetActive(false);

			playerWon = true;
	}
}
