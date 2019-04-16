using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Losemenue : MonoBehaviour
{
   GameObject[] loseObjects;

	// Use this for initialization
	void Start () {
		Time.timeScale = 1;
		loseObjects = GameObject.FindGameObjectsWithTag("loseEL");
foreach(GameObject g in loseObjects){
			g.SetActive(false);
		}
	}

    // Update is called once per frame
    void Update()
    {
        
    }
    public void displose(){

        foreach(GameObject g in loseObjects){
			g.SetActive(true);
		}
        Time.timeScale = 0;

    }
}
