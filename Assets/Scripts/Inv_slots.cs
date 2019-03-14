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

	// Use this for initialization
	void Start () {
		
	Debug.Log("Startup initialized");

	connectionString = "URI=file:" + Application.dataPath + "/InventoryDatabase.db";
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
		int a,b,c;
	using (IDbConnection dbConnection = new SqliteConnection(connectionString))
		{
			
			dbConnection.Open();
	Debug.Log("Database Opened");

			using (IDbCommand dbCmd = dbConnection.CreateCommand())
			{
				string sqlQuery = "SELECT * FROM Equipped";

				dbCmd.CommandText = sqlQuery;

				using (IDataReader reader = dbCmd.ExecuteReader())
				{
	Debug.Log("SQL excuted ");

					while(reader.Read()){

						a = reader.GetInt32(0);
						//	reader.GetString(0),
						 b = reader.GetInt32(1);
						 c = reader.GetInt32(2);
						Debug.Log("Health INcreased,ID="+a+",ItemID="+b+",ItemCount="+c);

						
					}


					dbConnection.Close();
					reader.Close();
				}
			}
		}
	}
}
