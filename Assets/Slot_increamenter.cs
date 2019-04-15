using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Mono.Data.Sqlite; 
using System.Data; 
using System;
public class Slot_increamenter : MonoBehaviour {
private string connectionString;


    Text text ;
	// Use this for initialization
	void Start () {
		text = this.GetComponentInChildren<Text>();
			Debug.Log("Startup initialized");

	connectionString = "URI=file:" + Application.dataPath + "/Scripts/InventoryDatabase.db";
	}
	
	// Update is called once per frame
	void Update () {
		incr();
	}
void incr(){
	int Health_potion_count;
		using (IDbConnection dbConnection = new SqliteConnection(connectionString))
		{
			
			dbConnection.Open();
	Debug.Log("Database Opened");

			using (IDbCommand dbCmd = dbConnection.CreateCommand())
			{
				string sqlQuery = "SELECT * FROM Equipped WHERE ID  = 2";

				dbCmd.CommandText = sqlQuery;

				using (IDataReader reader = dbCmd.ExecuteReader())
				{
	Debug.Log("SQL excuted ");

						reader.Read();

						
						 Health_potion_count = reader.GetInt32(2);
						//Debug.Log("Health Increased,ID="+Health_potion_ID+",ItemID="+b+",ItemCount="+Health_potion_count);
					text.text = Health_potion_count.ToString();
						reader.Close();
					dbConnection.Close();

				}
			}
		}
}
	
}
