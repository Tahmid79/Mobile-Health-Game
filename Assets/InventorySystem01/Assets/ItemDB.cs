using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Mono.Data.Sqlite;
using System.Data;
using System;
/**
    ItemDB is a class where all the items available stored here as it
    is better to create a list than keep prompting data from db.
 */
public class ItemDB : MonoBehaviour {

    public static List<List<Item>> ItemList = new List<List<Item>>();
    public static List<Item> ConsumableList = new List<Item>(); // 0
    public static List<Item> EquipList = new List<Item>(); // 1
    public static List<Item> ThrowableList = new List<Item>(); // 2
    public static List<Item> AcquiredItems = new List<Item>();
    public static int[] itemCount = new int[50];
    public Sprite[] sprites;

    public static int CONSUMABLES = 0;
    public static int EQUIP = 1;
    public static int THROW = 2;

    public static Item EquipedItem;
    public static List<Item> QuickSlotItems =new List<Item>();
    // public Transform EC;
    // private EquipController ECs;
    public Transform bigcanva;
    private BigCanvaController bigcanvaS;
    public static Item emptyItem;
    public Sprite img;

    private string connectionString;
    private string sqlQuery;
    IDbConnection dbConnection;
    IDbCommand dbCommand;

    // Use this for initialization
    void Start () {

        emptyItem = new Item(){
            itemID = 0 ,
            itemName = null,
            icon = img,
            description = null,
            damage = 0,
            effectType = 0,
            effectNum = 0,
            isEquiped = false
        };

        ItemList.Add(ConsumableList);
        ItemList.Add(EquipList);
        ItemList.Add(ThrowableList);
        // ECs = EC.GetComponent<EquipController>();
        //bigcanva = this.transform;
        bigcanvaS = bigcanva.GetComponent<BigCanvaController>();

        Debug.Log("ItemDB Startup initialized");

	    connectionString = "URI=file:" + Application.dataPath + "/InventorySystem01/Assets/InventoryDatabase.db";

	    Debug.Log("ItemDB connection startup complete, load items..");

        loadItems();
        
        Debug.Log("ItemDB load items complete.");

    }

    void loadItems(){
        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
		{
			dbConnection.Open();

            Debug.Log("ItemDB connection opened,querying db..");

			using (IDbCommand dbCmd = dbConnection.CreateCommand())
			{
				string sqlQuery = "SELECT * FROM Item";

				dbCmd.CommandText = sqlQuery;

				using (IDataReader reader = dbCmd.ExecuteReader())
				{
                    int i = 0;

					while(reader.Read()){
						//Get and set the item from the database
                        int itemType = 0;
                        string itemTypeS = reader.GetString(2);
                        // string itemQuery;

						Item item = new Item(){
                            itemID = reader.GetInt32(0),
                            itemName = reader.GetString(1),
                            description = reader.GetString(3),
                            icon = sprites[i] // spriteID = itemID - 1
                        };

                        if (itemTypeS == "Equip"){
                            item.type= Item.Type.equip;
                            itemType = EQUIP;
                            // itemQuery="SELECT * FROM Weapon WHERE ItemID ="+item.itemID;
                            // dm
                        }else if (itemTypeS == "Consumable"){
                            item.type= Item.Type.consumables;
                            itemType = CONSUMABLES;
                        }else{
                            item.type= Item.Type.throwable;
                            itemType = THROW;
                        }
                        
                        ItemList[itemType].Add(item);

						//TODO: Remove debug statements
						Debug.Log("Item added: " + reader.GetString(1));
						Debug.Log("Item: " + ItemList[itemType][ItemList[itemType].Count-1].itemName);
						i++;
					}
					Debug.Log("All Item from db added");
					// 
					reader.Close();
				}
                
                // Read Weapon
                sqlQuery = "SELECT * FROM Weapon";
                dbCmd.CommandText = sqlQuery;

                using (IDataReader reader = dbCmd.ExecuteReader()){

                    while(reader.Read()){
                        
                        // get id
                        int ID = reader.GetInt32(1);

                        for(int j=0;j<ItemList[EQUIP].Count;j++){
                            if(ItemList[EQUIP][j].itemID == ID){
                                ItemList[EQUIP][j].damage = reader.GetInt32(2);
                                ItemList[EQUIP][j].effectType = reader.GetInt32(3);
                                Debug.Log("Weapon updated, ID= " + reader.GetInt32(1));
                                break;
                            }
                        }

                    }
					reader.Close();
                }

                // Read Consumable
                sqlQuery = "SELECT * FROM Consumable";
                dbCmd.CommandText = sqlQuery;

                using (IDataReader reader = dbCmd.ExecuteReader()){

                    while(reader.Read()){

                        // get id
                        int ID = reader.GetInt32(1);

                        for(int j=0;j<ItemList[CONSUMABLES].Count;j++){

                            if(ItemList[CONSUMABLES][j].itemID == ID){
                                ItemList[CONSUMABLES][j].effectType = reader.GetInt32(2);
                                ItemList[CONSUMABLES][j].effectNum = reader.GetDouble(3);
                                Debug.Log("Consumable updated, ID= " + reader.GetInt32(1));
                                break;
                            }
                            
                        }

                    }
                    reader.Close();
                }

                // Read Consumable
                sqlQuery = "SELECT * FROM Throwable";
                dbCmd.CommandText = sqlQuery;

                using (IDataReader reader = dbCmd.ExecuteReader()){

                    while(reader.Read()){

                        // get id
                        int ID = reader.GetInt32(1);

                        for(int j=0; j<ItemList[THROW].Count; j++){

                            if(ItemList[THROW][j].itemID == ID){
                                ItemList[THROW][j].damage = reader.GetInt32(2);
                                Debug.Log("Throwable updated, ID= " + reader.GetInt32(1));
                                break;
                            }
                            
                        }

                    }
                    reader.Close();
                }

                Debug.Log("Calling read acquired items funtion..");
                ReadAcquiredItemFromDB();
                // Debug.Log("")

                Debug.Log("Qeurying Equiped list..");
                sqlQuery = "SELECT * FROM EquipedItem";
                dbCmd.CommandText = sqlQuery;

                using (IDataReader reader = dbCmd.ExecuteReader()){

                    while(reader.Read()){

                        Debug.Log("Reading..");
                        // get id
                        int ID = reader.GetInt32(0);
                        int i = QuickSlotItems.Count;
                        Debug.Log(ID);
                        Debug.Log(""+ reader.GetDataTypeName(1));
                        int itemID = reader.GetInt32(1);

                        if( itemID != 0 ){
                            if( ID != 1 ){
                                if( i < 4 ){
                                    QuickSlotItems.Add( IDtoITEM( itemID ));
                                    IDtoITEM(itemID).isEquiped = true;
                                    i++;
                                    Debug.Log("Item "+i+" "+QuickSlotItems[i-1].itemName+" is read. quickslot item count:"+QuickSlotItems.Count);
                                }
                            } else {
                                EquipedItem = IDtoITEM( reader.GetInt32(1) );
                                for(int x = 0; x < InventoryController.acquiredItems.Count;x++){
                                    if(InventoryController.acquiredItems[x].itemName == EquipedItem.itemName){
                                        InventoryController.acquiredItems[x].isEquiped = true;
                                        Debug.Log(InventoryController.acquiredItems[x].itemName+" is set to true.");
                                        break;
                                    }
                                }
                                Debug.Log("Weapon "+EquipedItem.itemName+" is read.");
                                bigcanvaS.equipedItem = IDtoITEM(itemID);
                                Debug.Log("Big Canva Equiped item is set to: "+bigcanvaS.equipedItem.itemName);
                            }
                        } else {
                            Debug.Log("ID: "+ID+" is NULL.");
                            if (ID == 1){
                                bigcanvaS.equipedItem = emptyItem;
                                bigcanvaS.isUnequiped = true;
                            }
                        } 
                        
                    }
                    reader.Close();
                }
                
			}

            Debug.Log("All item updated.");
            dbConnection.Close();
		}
    }
    
    private void ReadAcquiredItemFromDB(){
        Debug.Log("Reading Acquired Item from Database..");
        int i = 0;
        AcquiredItems.Clear();
        Debug.Log("Acquired item list cleared");
        using (dbConnection = new SqliteConnection(connectionString)){
            dbConnection.Open();
            Debug.Log("connection opened.");
            dbCommand = dbConnection.CreateCommand();
            sqlQuery = "SELECT * FROM AcquiredItem LIMIT 18";
            dbCommand.CommandText = sqlQuery;
            IDataReader reader = dbCommand.ExecuteReader();
            while (reader.Read()){
                int x = reader.GetInt32(0);
                Debug.Log("ItemID "+x+" loaded.");
                Item item = IDtoITEM(x);
                Debug.Log("Item name: "+item.itemName);
                AcquiredItems.Add( item );
                Debug.Log(AcquiredItems[AcquiredItems.Count-1].itemName+" added to the list");
                itemCount[x]=reader.GetInt32(1);
                Debug.Log("Count at "+x+", "+itemCount[x]);
                i++;
            }
            reader.Close();
            reader = null;
            dbCommand.Dispose();
            dbCommand = null;
            dbConnection.Close();
            dbConnection = null;
        }
        Debug.Log("AI: Reading completed.");
    }

    private Item IDtoITEM(int id){
        Item item = null;

        // get max length
        int MAX = ConsumableList.Count;
        if( MAX < EquipList.Count ){
            if( EquipList.Count < ThrowableList.Count ){
                MAX = ThrowableList.Count;
            } else {
                MAX = EquipList.Count;
            }
        } else if( MAX < ThrowableList.Count){
            MAX = ThrowableList.Count;
        }

        for( int i=0; i<MAX; i++ ){
            if( i < EquipList.Count && EquipList[i].itemID == id){
                item = EquipList[i];
            }
            if( i < ThrowableList.Count && ThrowableList[i].itemID == id){
                item = ThrowableList[i];
            }
            if( i < ConsumableList.Count && ConsumableList[i].itemID == id){
                item = ConsumableList[i];
            }
        }
        return item;

    }
}
