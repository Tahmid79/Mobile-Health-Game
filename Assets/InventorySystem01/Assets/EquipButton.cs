using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipButton : MonoBehaviour {

    public Transform bigCanva;
    public BigCanvaController bigCanvaS;
    public Transform buttonText;
    public Transform equipController;
    public List<Item> QSList = EquipController.QSList;

	// Use this for initialization
	void Start () {
        bigCanvaS = bigCanva.GetComponent<BigCanvaController>();
	}

    public void EQUIP(){
        buttonText.GetComponent<Text>().text = "EQUIP";
    }

    public void UNEQUIP(){
        buttonText.GetComponent<Text>().text = "UNEQUIP";
    }

    /**
     * When Clicked,
     * 1. if it's equiped:
     *      a. remove item from big canva to null
     *      b. if item.type == equip
     *             i) reset equipment to null
     *         else
     *             i) remove from quick slot
     *      b. item.isEquiped = false
     * 2. if it's not equiped:
     *      a. replace item of equiped item in big canva
     *      b. set equipController item = 
     *      c. item.isEquiped = true
     */
    private void OnMouseDown()
    {//&& bigCanvaS.selectedItem.GetComponent<Item>().itemID!=0
        if(bigCanvaS.selectedItem!=null){
            Item selItemS = bigCanvaS.selectedItem.GetComponent<Item>();
            if (selItemS.isEquiped) // UNEQUIP
            {
                Debug.Log("unequip");
                EquipEvent(selItemS);
                if(selItemS.type == Item.Type.equip){
                    bigCanvaS.isUnequiped = true;
                }
            }
            else // Equip
            {
                selItemS.isEquiped = true;
                bigCanvaS.isUnequiped = false;
                if (selItemS.type == Item.Type.equip)
                {
                    if(bigCanvaS.equipedItem.itemID != 0){
                        bigCanvaS.equipedItem.isEquiped = false; // unequip the previous item.
                        Debug.Log("item "+bigCanvaS.equipedItem.itemName+" is unequiped.");
                    }  
                    Debug.Log("equip weapon "+bigCanvaS.selectedItem.GetComponent<Item>().itemName);
                    equipController.GetComponent<EquipController>().SetWeapon(bigCanvaS.selectedItem.GetComponent<Item>());
                }
                else
                {
                    int j = 0;
                    bool isListed =false;
                    int count = QSList.Count;
                    Debug.Log(selItemS.itemID);
                    while(j < QSList.Count){
                        if(QSList[j].itemID == selItemS.itemID){
                            isListed = true;
                            Debug.Log("found it.");
                            break;
                        }
                        Debug.Log(QSList[j].itemID);
                        Debug.Log(j);
                        j++;
                    }
                    if(!isListed){
                        Debug.Log(selItemS.itemName+" is not listed.");
                        equipController.GetComponent<EquipController>().SetEquip(bigCanvaS.selectedItem);
                    }
                }
            }
            bigCanvaS.equipedItem = bigCanvaS.selectedItem.GetComponent<Item>();
            
        }
        bigCanvaS.isSel = true;
        
    }

    // for global use
    public void EquipEvent(Item selItemS)
    {
        selItemS.isEquiped = false;
        if (selItemS.type == Item.Type.equip)
        {
            equipController.GetComponent<EquipController>().ResetWeapon(0);
            bigCanvaS.setEmpty(); // reset equiped item
            Debug.Log("Equiped item is set to itemID = "+bigCanvaS.equipedItem.itemID);
        }
        else
        {
            equipController.GetComponent<EquipController>().Unequip(bigCanvaS.selectedItem);
            //bigCanvaS.equipedItem = ItemDB.emptyItem;
        }
    }
}
