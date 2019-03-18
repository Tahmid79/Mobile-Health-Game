using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Mono.Data.Sqlite; 
using System.Data; 
using System;

public class Inv_slots : MonoBehaviour {
private string connectionString;
    PlayerHealth playerHealth;                  // Reference to the player's health.

	// Use this for initialization
	void Start () {
		        playerHealth = this.GetComponent <PlayerHealth> ();

	Debug.Log("Startup initialized");

	connectionString = "URI=file:" + Application.dataPath + "/Scripts/InventoryDatabase.db";
	//IDbConnection  dbconn;
    // dbconn = (IDbConnection) new SqliteConnection(connectionString);
   //  dbconn.Open(); //Open connection to the database.
   //  IDbCommand dbcmd = dbconn.CreateCommand();
		
	}

	// Update is called once per frame
	void Update () {
		 if (Input.GetKey("1"))
        {
			HealthCounterSlot();
           
        }
	}

	void  HealthCounterSlot(){
		int b,Health_potion_count,Health_potion_ID;
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

						Health_potion_ID = reader.GetInt32(0);
						//	reader.GetString(0),
						 b = reader.GetInt32(1);
						 Health_potion_count = reader.GetInt32(2);
						Debug.Log("Health Increased,ID="+Health_potion_ID+",ItemID="+b+",ItemCount="+Health_potion_count);

																reader.Close();

					

					if (Health_potion_count > 0 ){
						playerHealth.IncHealth(10);
						Health_potion_count=Health_potion_count-1;
						string sqlQ = "UPDATE Equipped SET ItemCount = "+Health_potion_count+"  WHERE ID  = 2";
						dbCmd.CommandText = sqlQ;

						dbCmd.ExecuteScalar();
					}


					dbConnection.Close();

				}
			}
		}
	}
}
