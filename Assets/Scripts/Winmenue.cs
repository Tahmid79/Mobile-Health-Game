using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Winmenue : MonoBehaviour
{
   GameObject[] WinObjects;

	// Use this for initialization
	void Start () {
		Time.timeScale = 1;
		WinObjects = GameObject.FindGameObjectsWithTag("WinEL");
foreach(GameObject g in WinObjects){
			g.SetActive(false);
		}
	}

    // Update is called once per frame
    void Update()
    {
        
    }
    public void dispWin(){

        foreach(GameObject g in WinObjects){
			g.SetActive(true);
		}
        Time.timeScale = 0;

    }
}
