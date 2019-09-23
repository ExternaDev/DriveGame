using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
	

    public AICarsManager AICars;
   // public EventManager eventManager;
    //public bool override
    // Start is called before the first frame update
    public bool PlayerAlive = false;
    public bool GameStarted = false;

    void Awake()
    {
        instance = this;
        AICars= this.GetComponent<AICarsManager>();
        EventManager.OnPlayerDied += OnPlayerDied;
        EventManager.OnGameStart += StartGame;

    }
    void OnDisable(){
        EventManager.OnPlayerDied -= OnPlayerDied;
        EventManager.OnGameStart -= StartGame;
    }
    void OnPlayerDied(){
        PlayerAlive = false;
    }


    public void ResetGame(){
        //resest scores pickups 
    }
    public void StartGame(){
        //start game running 
        GameStarted = true;
        PlayerAlive =true;
        
    }

    public bool GameRunning(){return PlayerAlive && GameStarted;}
}
