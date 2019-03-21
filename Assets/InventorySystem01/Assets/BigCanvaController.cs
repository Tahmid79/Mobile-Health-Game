using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BigCanvaController : MonoBehaviour {

    public bool isSel=false;
    public bool isDel=false;
    public Transform selectedItem = null;
    private Transform prevSelItem = null;
    public Item equipedItem = ItemDB.emptyItem;
    public Item prevEquiped = ItemDB.emptyItem;
    public Transform DC;
    private DescriptionController DCs;
    public bool isUnequiped = false;
    public Transform EB;
    public EquipButton EBs;

    void Start(){
        Debug.Log("BigCanva Starting up..");
        DCs = DC.GetComponent<DescriptionController>();
        EBs = EB.GetComponent<EquipButton>();
        Debug.Log("BigCanva Startup Completed.");
    }

    void Update(){

        if(isSel && selectedItem!=null){
            DCs.SetDescription( selectedItem.GetComponent<Item>() );
            if(selectedItem.GetComponent<Item>().type == Item.Type.equip){
                Debug.Log("Selected item is a weapon");
                Debug.Log("empty item id: "+ItemDB.emptyItem.itemID);
                Debug.Log("x Equiped item id: "+equipedItem.itemID);
                if(!equipedItem.isEquiped){
                    equipedItem = ItemDB.emptyItem;
                    Debug.Log("surprize!");
                    Debug.Log("x Equiped item id: "+equipedItem.itemID);
                }
                
                if (equipedItem.itemID != 0 && !isUnequiped && equipedItem.itemID == selectedItem.GetComponent<Item>().itemID){
                    EBs.UNEQUIP();
                    Debug.Log("button set to unequip");
                    selectedItem.GetComponent<Item>().isEquiped = true;
                } else {
                    EBs.EQUIP();
                    Debug.Log("button set to equip");
                }
            } else {
                bool isListed = false;
                for(int i =0; i < ItemDB.QuickSlotItems.Count ; i++){
                    if(ItemDB.QuickSlotItems[i].itemID == selectedItem.GetComponent<Item>().itemID){
                        isListed = true;
                        break;
                    }
                }

                if(isListed){
                    EBs.UNEQUIP();
                } else {
                    EBs.EQUIP();
                }
            }
            isSel = false;
            Debug.Log("x Equiped item id: "+equipedItem.itemID);
        }

        if(isDel){
            Debug.Log("Reseting pokedex.");
            DCs.ResetDescription();
            Debug.Log("Reset completed.");
            isDel = false;
        }

        if(prevSelItem!=selectedItem && selectedItem!=null){
            Debug.Log("Change detected in BigCanva!");
            isSel = true;
            prevSelItem = selectedItem;
        }

        // if(selectedItem == null){
        //     Debug.Log("fu");
        // }

        // if(equipedItem!=prevEquiped && equipedItem.itemID != 0 && prevEquiped.itemID != 0){
        //     Debug.Log("Equiped item changed!");
        //     Debug.Log(prevEquiped.itemID+"->"+equipedItem.itemID);
        //     prevEquiped = equipedItem;
        // }

         //Go to main stage
        if(Input.GetKey("p")){
            SceneManager.LoadScene(3);
        }
    }

    public void setEmpty(){
        equipedItem = ItemDB.emptyItem;
        Debug.Log("equip item= "+equipedItem.itemID);
    }

	
}
