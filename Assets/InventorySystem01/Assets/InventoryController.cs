using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mono.Data.Sqlite;
using System.Data;
using System;

public class InventoryController : MonoBehaviour
{
    public GameObject SlotPrefab;
    private const int SLOT = 18;
    public static List<Item> acquiredItems = ItemDB.AcquiredItems;
    public List<GameObject> slots = new List<GameObject>();
    //public int[] ItemDB.itemCount = ItemDB.ItemDB.itemCount;
    public Sprite transparentImg;

    // connector
    private string connectionString;
    private string sqlQuery;
    IDbConnection dbConnection;
    IDbCommand dbCommand;
    
    // Use this for initialization
    void Start()
    {

        Debug.Log("Inventory Controller DB connection startup initialized");
	    connectionString = "URI=file:" + Application.dataPath + "/InventorySystem01/Assets/InventoryDatabase.db";
	    Debug.Log("Inventory Controller DB connection startup complete");

        for (int i = 0; i < 18; i++)
        {
            GameObject newSlot;
            
            string newText = "";
            newSlot = Instantiate(SlotPrefab,this.transform);
            newSlot.name = "Slot " + i;
            Transform newItem = newSlot.transform.Find("ItemPrefab");
            Item newItemS = newItem.GetComponent<Item>();
            Image itemImage = newItemS.transform.GetComponent<Image>();
            newItemS.enabled = true;
            newItem.name = "Item " + i;
            newSlot.GetComponent<Slot>().slotID = i;
            
            Debug.Log("Slot "+i);
            if (i < acquiredItems.Count)
            {
                
                Debug.Log("Setting item: "+acquiredItems[i].itemName);          
                acquiredItems[i].isEquiped = true;      
                newItemS.SetItem(acquiredItems[i]);
                newItemS.itemEnabled = true;
                if(newItemS.itemEnabled){
                    Debug.Log(newItemS.itemName +" "+newItemS.itemID+" is set to true, count = "+ItemDB.itemCount[newItemS.itemID]);
                }
                itemImage.sprite = newItemS.icon;
                newSlot.GetComponent<Slot>().item = acquiredItems[i];
                if (ItemDB.itemCount[newItemS.itemID] > 1)
                {
                    newText += ItemDB.itemCount[newItemS.itemID];
                }
                if (i == 0)
                {
                    newItemS.SetSelect();
                }
            }
            newSlot.GetComponentInChildren<Text>().text = newText;
            slots.Add(newSlot);
        }
        Debug.Log("IC initialization completed");
    }

    private void Sort()
    {
        for (int i = 0; i < 18; i++)
        {
            Transform slot;

            string newText = "";
            string slotName = "Slot " + i;
            string itemName = "Item " + i;
            slot = transform.Find(slotName);
            Transform item = slot.transform.Find(itemName);
            Item itemS = item.GetComponent<Item>();
            Image itemImage = item.transform.GetComponent<Image>();
            if (ItemDB.itemCount[itemS.itemID] > 1)
            {
                newText += ItemDB.itemCount[itemS.itemID];
            }
            slot.GetComponentInChildren<Text>().text = newText;
            if (i < acquiredItems.Count)
            {
                itemS.itemEnabled = true;
                itemS.SetItem(acquiredItems[i]);
                itemImage.sprite = itemS.icon;
                //slot.GetComponent<Slot>().item = acquiredItems[i];
                // if (i == 0)
                // {
                //     itemS.SetSelect();
                // }
            } else {
                itemS.itemEnabled = false;
                itemS.SetItem(null);
                itemImage.sprite = transparentImg;
                //slot.GetComponent<Slot>().item = null;
            }
        }
    }

    private void SortItemCount(int offset){

        while(offset<SLOT-1){
            ItemDB.itemCount[offset] = ItemDB.itemCount[offset+1];
            offset++;
        }

    }

    /**
     * Remove in steps:
     * - trace item in list
     * - trace item in slot
     * - trace the slotID
     * - remove item from slot
     * 
     */
    public void RemoveItemFromList(Item item)
    {
        string itemName = item.itemName;
        int i;
        for(i = 0; i<SLOT; i++)
        {
            string itemslot = "Item " + i;
            if (slots[i].transform.Find(itemslot).GetComponent<Item>().itemName == itemName)
            {
                acquiredItems.Remove(item);
                ItemDB.itemCount[item.itemID] = 0;
                Sort();
                DeleteAcquiredItem(item.itemID);
                break;
            }
        }
    }
    
    public void DeleteAcquiredItem(int id){
        // abc
        using (dbConnection = new SqliteConnection(connectionString)){
            dbConnection.Open(); // Open connection to the db
            dbCommand = dbConnection.CreateCommand();
            sqlQuery = " DELETE FROM EquipedItem WHERE ItemID = "+id;
            dbCommand.CommandText = sqlQuery;
            dbCommand.ExecuteScalar();
            dbConnection.Close();
        }
    }

}
