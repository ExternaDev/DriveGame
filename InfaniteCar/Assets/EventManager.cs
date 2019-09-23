using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
	public static EventManager instance;

	public delegate void OnGameStartAction();
    public static event OnPlayerDiedAction OnGameStart;



    public delegate void OnPlayerDiedAction();
    public static event OnPlayerDiedAction OnPlayerDied;


    public delegate void OnGameResetAction();
    public static event OnPlayerDiedAction OnGameReset;
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

}
