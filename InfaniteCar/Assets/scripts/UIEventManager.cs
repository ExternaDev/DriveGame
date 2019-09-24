using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEventManager : MonoBehaviour
{

	public GameObject deathPanel;
	public GameObject startPanel;

    // Start is called before the first frame update
    void Start()
    {
        EventManager.OnPlayerDied += OnPlayerDied;
        EventManager.OnGameStart += OnGameStart;
        EventManager.OnGameReset += OnGameReset;

        OnGameReset();//to get panels on
    }

   
    void OnPlayerDied(){
    	deathPanel.SetActive(true);
    }
    void OnGameStart(){
    	startPanel.SetActive(false);
    }
    void OnGameReset(){
    	startPanel.SetActive(true);
    	deathPanel.SetActive(false);

    }
}
