using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using UnityEngine.UI;
using Mono.Data.Sqlite;
using System.Data;
using System;

public class GameOverManager : MonoBehaviour {

	GameObject tryAgainButton, quitButton, scoreCounter, win;

	public GameObject GameOverUI, PopupMenuUI;

	private float transitionDelay = 1f;

	GameManager gameManager = new GameManager();

	[SerializeField]
	private Text scoreText;

	[SerializeField]
	private Text winText;

	// Use this for initialization
	void Start () {

		//Initialize object references
		tryAgainButton = GameObject.Find("TryAgainBtnLayer");
		quitButton = GameObject.Find("quitBtnLayer");
		scoreCounter = GameObject.Find("Score");
		win = GameObject.Find("Win");

		//Display the session score
		scoreText.text = gameManager.getSessionScore().ToString();

		int score = gameManager.getSessionScore();

		//Check if the player won or lost the session
		if(score > 5){

			winText.text = "YOU WIN!";
			Debug.Log("YOU WIN!");
			
			displayPopup();

		} else{

			winText.text = "YOU LOSE!";
			Debug.Log("YOU LOSE!");
		}

		Debug.Log("Final Score: " + gameManager.getSessionScore().ToString());
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public GameObject getSelectedButton(int selectedButtonIndex){
		GameObject btnSelected;
		switch(selectedButtonIndex){
			case 0:
			btnSelected = tryAgainButton;
			break;
			case 1:
			btnSelected = quitButton;
			break;
			default:
			btnSelected = quitButton;
			break;
		}

		return btnSelected;
	}

	public void onTryAgainBtnClick(){
		SceneManager.LoadScene(1);
	}

	public void displayPopup(){

		
		// yield return new WaitForSeconds(transitionDelay);

		Debug.Log("Popup displayed");

		PopupMenuUI.SetActive(true);
		GameOverUI.SetActive(false);
	}

	public void onPopupDoneBtnClick(){
		PopupMenuUI.SetActive(false);
		GameOverUI.SetActive(true);
	}

}
