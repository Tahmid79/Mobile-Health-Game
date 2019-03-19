using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteButton : MonoBehaviour {

    public Transform bigCanva;
    public BigCanvaController bigCanvaS;

    public Transform inventory;
    public InventoryController inventoryS;

    public Transform equipButton;
    public EquipButton equipButtonS;
    private Item selItem;

    // Use this for initialization
    void Start()
    {
        bigCanvaS = bigCanva.GetComponent<BigCanvaController>();
        equipButtonS = equipButton.GetComponent<EquipButton>();
    }

    /**
     * On delete item:
     * 1. remove item from acquired list
     * 2. remove selected item of big canva
     * 3. remove shown item on the pokedex 
     * 4. if it's equiped, reset the equipment
     */
    private void OnMouseDown()
    {
        // selItem = bigCanvaS.selectedItem.GetComponent<Item>();

        // if (selItem.itemID != 1)
        // {
        //     if (selItem.isEquiped)
        //     {
        //         equipButtonS.EquipEvent(selItem);
        //     }
        //     RemoveFromList();
        // bigCanvaS.selectedItem = null;
        // }
    }

    public void RemoveFromList()
    {
        inventoryS.RemoveItemFromList(selItem);
    }
}
