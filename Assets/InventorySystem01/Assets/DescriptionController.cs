using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DescriptionController : MonoBehaviour {

    // public Transform selectedItem;
    public BigCanvaController bigCanva;
    public Transform descriptionPanel;
    // public Item chosenItem;
    public Transform textPanel;
    private Transform itemName;
    private Transform type;
    private Transform description;
    private Transform statsL;
    private Transform statsR;
    private Transform prevSelItem = null;
    public Sprite img;// transparent imageholder
    public Transform pokedexIcon;

    // Use this for initializations
    void Start () {
        Debug.Log("DescriptionController Startup initiated");
        bigCanva = transform.parent.GetComponent<BigCanvaController>();
        itemName = textPanel.Find("ItemName");
        type = textPanel.Find("ItemType");
        description = descriptionPanel.Find("Text");
        statsL = descriptionPanel.Find("stats").Find("left");
        statsR = descriptionPanel.Find("stats").Find("right");
        ResetDescription();
        Debug.Log("DescriptionController Startup Completed!");
    }

    // Update is called once per frame
    void Update()
    {
        // // detect changes
        // if ( bigCanva.isChanged ){
        //     Debug.Log("DC: changes detected!");
        //     if (selectedItem!=null){
        //         SetDescription();
        //     } else {
        //         ResetDescription();
        //     }
        //     prevSelItem = selectedItem;
        // }
        
    }

    public void ResetDescription(){
        itemName.GetComponent<Text>().text = "";
        type.GetComponent<Text>().text = "";
        description.GetComponent<Text>().text = "Please select an item.";
        statsL.GetComponent<Text>().text="";
        statsR.GetComponent<Text>().text="";
        pokedexIcon.GetComponent<Image>().sprite = img;
    }
    public void SetDescription(Item chosenItem){
        
        string stat = "";
        // selectedItem = bigCanva.selectedItem;
        // chosenItem = bigCanva.selectedItem.GetComponent<Item>();
        itemName.GetComponent<Text>().text = chosenItem.itemName;
        
        Debug.Log("DescriptionController called!\nItem: "+chosenItem.itemName+" selected!");

        if (chosenItem.type == Item.Type.equip)
        {            
            Debug.Log("Equip");
            type.GetComponent<Text>().text = "Equipable";
            type.GetComponent<Text>().color = Color.magenta;
            statsL.GetComponent<Text>().text = "Effect Type:\nDamage:";

            // differs with every type of Equip
            switch(chosenItem.effectType){
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

            statsR.GetComponent<Text>().text = stat + chosenItem.damage;
        }
        if (chosenItem.type == Item.Type.consumables)
        {
            Debug.Log("Consumables");
            string statL="Type:\n";
            type.GetComponent<Text>().text = "Consumable";
            type.GetComponent<Text>().color = Color.green;

            // differs with every type of CONSUMABLE
            switch(chosenItem.effectType){
                case 1:                
                    stat = "HEALING\n" + chosenItem.effectNum;
                    statL += "Healing Amount:";
                    break;
                case 2:
                    stat = "AS BUFF\n" + chosenItem.effectNum;
                    statL += "Buff Amount:";
                    break;
                default:
                    break;
            }
            statsL.GetComponent<Text>().text = statL;
            statsR.GetComponent<Text>().text = stat;
        }
        if (chosenItem.type == Item.Type.throwable)
        {
            Debug.Log("Throwable");
            type.GetComponent<Text>().text = "Throwable";
            type.GetComponent<Text>().color = Color.red;            
            statsL.GetComponent<Text>().text = "Damage:";
            statsR.GetComponent<Text>().text = stat + chosenItem.damage;
        }

        description.GetComponent<Text>().text = chosenItem.description;
        pokedexIcon.GetComponent<Image>().sprite = chosenItem.icon;
    }

 
}
