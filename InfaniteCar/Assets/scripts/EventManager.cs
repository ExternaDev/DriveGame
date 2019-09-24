﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
	public static EventManager instance;

	public delegate void GameEventAction();
    public static event GameEventAction OnGameStart;



    //public delegate void OnPlayerDiedAction();
    public static event GameEventAction OnPlayerDied;


    //public delegate void OnGameResetAction();
    public static event GameEventAction OnGameReset;


    public static event GameEventAction OnResumeAftervideo;



    void Awake(){
    	instance = this;
    }
    public void PlayerDied(){
        if(OnPlayerDied != null)
            OnPlayerDied();
    }

    public void StartGame(){
        if(OnGameStart != null)
            OnGameStart();
    }


    public void GameReset(){
        if(OnGameReset != null)
            OnGameReset();
    }


    public void ResumeGameAftervideo(){
        if(OnResumeAftervideo != null)
            OnResumeAftervideo();
    }
}
