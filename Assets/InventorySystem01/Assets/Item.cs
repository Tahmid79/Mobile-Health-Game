using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Item : MonoBehaviour {

    // item enabled to eventtrigger
    public bool itemEnabled = false;
    // item equiped?
    public bool isEquiped = false;

    public int itemID;
    public string itemName;
    public Sprite icon;
    public string description; 

    public enum Type { equip, consumables, throwable};
    
    public Type type;

    public double damage;

    public int effectType;

    public double effectNum;

    void Update(){

    }

    // only called when it's not null so no need avoid null value.
    public void SetItem(Item a)
    {

        Debug.Log("Set item called, item a = "+a.itemName+" item attributes:");

        name = a.itemName;
        itemID = a.itemID;
        itemName = a.itemName;
        icon = a.icon;
        description = a.description;
        type = a.type;
        damage = a.damage;
        effectType = a.effectType;
        effectNum = a.effectNum;
        Debug.Log("itemID: "+itemID+"\nItem name: "+itemName+"\nItem description: "+description+"\nItem type: "+type+"\nItem damage: "+damage+"\nItem effectType: "+effectType+"\nItem effectNum: "+effectNum);
        
    }

    public void ResetItem(Sprite img){
        //name = "item";
        itemID = 0 ;
        itemName = null;
        icon = img;
        description = null;
        // type = ;
        damage = 0;
        effectType = 0;
        effectNum = 0;
        
    }

    public void SetSelect()
    {

        Debug.Log("Item "+itemName+" is set to select!");
        BigCanvaController bigCanva = transform.parent.parent.parent.parent.GetComponent<BigCanvaController>();
        bigCanva.selectedItem = this.transform;
        
    }


    void OnMouseDown()
    {
        Debug.Log(itemName + " is pressed");
        if (itemEnabled)
        {
            //get script
            BigCanvaController bigCanva = transform.parent.parent.parent.parent.GetComponent<BigCanvaController>();
            bigCanva.selectedItem = this.transform;
        }
    }

    
}
