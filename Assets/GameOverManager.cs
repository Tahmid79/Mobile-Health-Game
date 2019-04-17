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

	[SerializeField]
	private Text itemText;

	[SerializeField]
	private Text itemDescription;

	private Item2 rewardedItem;

	private string connectionString;

	// Use this for initialization
	void Start () {

		connectionString = "URI=file:" + Application.dataPath + "/InventoryDatabase.db";


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
			
			//Call this method to add a new item to the player's inventory 
			addNewItem();
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

	public void onQuitBtnClick(){
		//Go to main stage
		SceneManager.LoadScene(0);
	}

	public void displayPopup(){

		
		//TODO: Add a time delay for popup to appear

		Debug.Log("Popup displayed");

		PopupMenuUI.SetActive(true);
		GameOverUI.SetActive(false);

		populateRewardPopup();
	}

	public void onPopupDoneBtnClick(){
		PopupMenuUI.SetActive(false);
		GameOverUI.SetActive(true);
	}

	//Gets a new item and adds it to the table for the inventory
	private void addNewItem(){
		
		using (IDbConnection dbConnection = new SqliteConnection(connectionString))
		{
			dbConnection.Open();

			//First get a new item from the Item table
			using (IDbCommand dbCmd = dbConnection.CreateCommand())
			{
				string sqlQuery = "SELECT * FROM Item WHERE ItemID = 1";

				dbCmd.CommandText = sqlQuery;

				using (IDataReader reader = dbCmd.ExecuteReader())
				{

					while(reader.Read()){
						//Add the new item from the database as a new Item object
						rewardedItem = new Item2(
							reader.GetString(1),
							reader.GetString(2),
							reader.GetString(3),
							1
						);
				}

						Debug.Log("Reward: " + rewardedItem.name + ", " + rewardedItem.description);
					}
				}

			using (IDbCommand dbCmd = dbConnection.CreateCommand())
			{
				string insert = "INSERT INTO EquippedItems (ID, ItemId, ItemCount) VALUES ";

				//Add the values to insert into the table
				string values = rewardedItem.id + ", " + rewardedItem.id + ", " + rewardedItem.count + ");";
				string sqlInsert = String.Concat(insert, rewardedItem);

				dbCmd.CommandText = sqlInsert;

				dbConnection.Close();
			}
		}
	}

	private void populateRewardPopup(){

		//Add the name, icon and description from the Item class attributes
		itemText.text = rewardedItem.name;
		itemDescription.text = rewardedItem.description;
		
	}

}
