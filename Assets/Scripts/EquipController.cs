using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mono.Data.Sqlite;
using System.Data;
using System;

public class EquipController : MonoBehaviour {

    public Transform QuickSlots;
    public Transform SlotPrefabs;

    public static List<Item> QSList = new List<Item>();
    public Transform equipButton;
    public Transform deleteButton;
    public Item equipedItem;
    public Transform Icon;
    public Transform itemName;
    public Transform description;
    public Transform stats;
    public Sprite img;

    // connector
    private string connectionString;
    private string sqlQuery;
    IDbConnection dbConnection;
    IDbCommand dbCommand;

	// Use this for initialization
	void Start () {

        Debug.Log("EC starting up..");
        Debug.Log("Equip Controller DB startup initialized");

	    connectionString = "URI=file:" + Application.dataPath + "/InventorySystem01/Assets/InventoryDatabase.db";

	    Debug.Log("Equip Controller DB startup complete");

        Debug.Log("Instantiating quick slots..");

        for(int i = 0; i < 4; i++ ){

            Transform newSlot = Instantiate(SlotPrefabs, QuickSlots);
            newSlot.name = ""+i;
            Item newItem = newSlot.GetComponentInChildren<Item>();
            Text itemcount = newSlot.GetComponentInChildren<Text>();
            Sprite itemSprite = newItem.transform.GetComponent<Image>().sprite;
            itemSprite = img;
            newItem.name = "Item";
            itemcount.text = "";
            // slots.Add(newSlot.GetComponent<Slot>());
            newItem.ResetItem(img);
            QSList.Add(newItem);
            
        }

        Debug.Log("Quick slots generated.");

        ReadQS_DB();
        ResetWeapon(1);
        UpdateQS();

        if (transform.parent.parent.GetComponent<BigCanvaController>().isUnequiped){
            equipedItem = ItemDB.emptyItem;
            Debug.Log("EC reads: null, from Bigcanva: null");
        } else{
            equipedItem = transform.parent.parent.GetComponent<BigCanvaController>().equipedItem;
            Debug.Log("EC reads: "+equipedItem.itemName+" from Bigcanva: "+transform.parent.parent.GetComponent<BigCanvaController>().equipedItem.itemName);
            SetWeapon(equipedItem);
            Debug.Log(equipedItem.itemName+" is set");
            transform.parent.parent.GetComponent<BigCanvaController>().isUnequiped = false;
        }
        
        Debug.Log("EC startup completed.");
    }


    private void ReadQS_DB(){
        for(int i = 0; i<ItemDB.QuickSlotItems.Count; i++){
            Debug.Log("qsItem count: "+ItemDB.QuickSlotItems.Count);
            for (int j=0; j<4; j++){
                if(QSList[j].itemID == 0 ){
                    QSList[j].SetItem(ItemDB.QuickSlotItems[i]);
                    //QSList[j].transform.GetComponent<Image>().sprite = QSList[j].icon;
                    Debug.Log("QSlist-" +j+ " is set to "+QSList[j].itemName+"\n itemID: "+QSList[j].itemID);
                    break;
                }
            }
        }
    }

    private void UpdateQS(){
        for(int i = 0; i < 4; i++){
            if(QSList[i].itemID != 0){
                QSList[i].transform.GetComponent<Image>().sprite = QSList[i].icon;
                if( ItemDB.itemCount[QSList[i].itemID] > 1 ){
                    QSList[i].transform.parent.GetComponentInChildren<Text>().text = ""+ItemDB.itemCount[QSList[i].itemID];
                }
                
                Debug.Log(i+ " " +QSList[i].itemName+"'s icon is set");
            } else {
                QSList[i].transform.GetComponent<Image>().sprite = img;
                QSList[i].transform.parent.GetComponentInChildren<Text>().text = "";
            }
        }
    }

    public void SetWeapon(Item itemS)
    {
        //equipedItem = item;
        string stat="";
        if ( itemS.itemName!=null ){
            //Item itemS = item.GetComponent<Item>();            
            Icon.GetComponent<Image>().sprite = itemS.icon;
            itemName.GetComponent<Text>().text = itemS.itemName;
            description.GetComponent<Text>().text = itemS.description;
            itemS.isEquiped = true;
            switch(itemS.effectType){

                case 1:                
                    stat += "ALL\n";
                    break;

                case 2:
                    stat += "Grand Mal\n";
                    break;

                case 3:
                    stat += "Focal Seizures\n";
                    break;

                default:
                    break;

            }
            stats.GetComponent<Text>().text = stat + itemS.damage;
            UpdateDB(1,itemS.itemID);
            ItemDB.EquipedItem = itemS;
        } else {
            ResetWeapon(0);
        }
    }

    public void SetEquip(Transform item){
        Item itemS = item.GetComponent<Item>();
        for(int i = 0; i< 4; i++ ){
            if(QSList[i].itemID==0){
                QSList[i].SetItem(itemS);
                Debug.Log("QSList[" + i + "] is set to " + QSList[i].itemName);
                QSList[i].isEquiped = true;
                UpdateDB( 2, QSList[i].itemID );
                UpdateQS();
                ItemDB.QuickSlotItems.Add(itemS);
                break;
            }
        }
    }

    public void Unequip(Transform item){
        Item itemS = item.GetComponent<Item>();
        for(int i = 0; i< 4; i++ ){
            if(QSList[i].itemID!=0 && QSList[i].itemID == itemS.itemID) {
                Debug.Log(QSList[i].itemName);
                for(int j = 0; j<ItemDB.QuickSlotItems.Count;j++){
                    if(ItemDB.QuickSlotItems[j].itemID == itemS.itemID){
                        Debug.Log("Quick Slot removed");
                        ItemDB.QuickSlotItems.RemoveAt(j);
                    }
                }
                itemS.isEquiped = false;
                QSList[i].ResetItem(img) ;
                UpdateDB( 0, itemS.itemID );
                UpdateQS();
                break;
            }
        }
    }

    //to reset while unequiped
    public void ResetWeapon(int i)
    {
        equipedItem = null;
        ItemDB.EquipedItem = ItemDB.emptyItem;
        Icon.GetComponent<Image>().sprite = img;
        description.GetComponent<Text>().text = "Equip Something!";
        stats.GetComponent<Text>().text = "";
        itemName.GetComponent<Text>().text = "";
        if(i == 0){
            UpdateDB(0,1); // set to null
        }
    }

    private void UpdateDB(int ID,int itemID){

        using ( dbConnection = new SqliteConnection(connectionString)){

            Debug.Log("Updating equip DB");

            dbConnection.Open(); // Open connection to the db
            dbCommand = dbConnection.CreateCommand();
            if( ID == 0 && itemID == 1){ //if reset weapon
                sqlQuery = "UPDATE EquipedItem SET ItemID = 0 WHERE ID = 1";
            }else if( ID == 0 ){// if reset item
                sqlQuery = "UPDATE EquipedItem SET ItemID = 0 WHERE ItemID = " + itemID;
            }else if( ID == 1 ){// if set weapon
                sqlQuery = "UPDATE EquipedItem SET ItemID = " + itemID + " WHERE ID = 1";
            }else{
                int i = 0;
                sqlQuery = "SELECT ID FROM EquipedItem WHERE ItemID = 0 LIMIT 1";
                dbCommand.CommandText = sqlQuery;
                IDataReader reader = dbCommand.ExecuteReader();
                while (reader.Read()){
                    i = reader.GetInt32(0);
                }
                reader.Close();
                reader = null;
                dbCommand.Dispose();
                dbCommand = dbConnection.CreateCommand();
                sqlQuery = "UPDATE EquipedItem SET ItemID = "+itemID+" WHERE ID = " + i;
                // 
            }
            dbCommand.CommandText = sqlQuery;
            dbCommand.ExecuteScalar();
            dbConnection.Close();

        }
    }
}
