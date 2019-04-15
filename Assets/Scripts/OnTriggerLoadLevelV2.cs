using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnTriggerLoadLevelV2 : MonoBehaviour
{

    public GameObject[]  BAttlePrompt ;

     GameObject[]  Prompt ;



    void Start()
    {
      Prompt= GameObject.FindGameObjectsWithTag("prompt");
            foreach(GameObject g in BAttlePrompt){
			g.SetActive(false);
		}        
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider plyr)
    {
        if (plyr.gameObject.tag == "Player")
        {
            foreach(GameObject g in Prompt){
			g.SetActive(true);
		}
              foreach(GameObject g in BAttlePrompt){
			g.SetActive(true);
		}
           
        }
    }
    public void OnTriggerExit(Collider plyr)
    {
        if (plyr.gameObject.tag == "Player")
        {
  foreach(GameObject g in BAttlePrompt){
			g.SetActive(false);
		}       
         }
    }
    public void hideui(){
        {
  foreach(GameObject g in Prompt){
			g.SetActive(false);
		} 
    }
    }
}
