using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update

GameObject[] pauseObjects;
GameObject pausebt;

	// Use this for initialization
	void Start () {
		Time.timeScale = 1;
		pauseObjects = GameObject.FindGameObjectsWithTag("ShowOnPause");
		pausebt = GameObject.FindGameObjectWithTag("PauseBT");

		hidePaused();
	}

	// Update is called once per frame
	void Update () {

		
	}


	//Reloads the Level
    public void pauseBt_pressed(){

        if(Time.timeScale == 1)
			{
				Time.timeScale = 0;
				showPaused();
			} else if (Time.timeScale == 0){
				Debug.Log ("high");
				Time.timeScale = 1;
				hidePaused();
			}
    }
	public void Reload(){
		Application.LoadLevel(4);
	}

	//controls the pausing of the scene
	public void pauseControl(){
			if(Time.timeScale == 1)
			{
				Time.timeScale = 0;
				showPaused();
			} else if (Time.timeScale == 0){
				Time.timeScale = 1;
				hidePaused();
			}
	}

	//shows objects with ShowOnPause tag
	public void showPaused(){
		foreach(GameObject g in pauseObjects){
			g.SetActive(true);
		}
        pausebt.SetActive(false);
	}

	//hides objects with ShowOnPause tag
	public void hidePaused(){
		foreach(GameObject g in pauseObjects){
			g.SetActive(false);
		}
                pausebt.SetActive(true);

	}

	//loads inputted level
	public void LoadLevel(int  level){
		Application.LoadLevel(level);
	}
}
