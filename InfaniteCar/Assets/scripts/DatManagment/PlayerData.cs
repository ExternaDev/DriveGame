﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;
using System.IO;
public class PlayerData : MonoBehaviour
{
	public static PlayerData instance;
	public CarDataScriptableObject currentSelection;
	public PlayerUnlocks playerUnlocks;
	public PlayerUpgrades playerUpgrades;

	string UnlocksPath = "Assets/Resources/Data/Data1.txt";
	string UpgradePath = "Assets/Resources/Data/Data2.txt";

	public bool OverriteSaveOnLoad = false;
	void Awake(){
		if(instance == null){
			instance = this;
			DontDestroyOnLoad(this.gameObject);
		}else{
			DestroyImmediate(this.gameObject);
		}

		FindUnlockedCars();
		FindUpgrades();
	}

   	void FindUnlockedCars(){
   		string data = "";
   		
		if (System.IO.File.Exists(UnlocksPath) && !OverriteSaveOnLoad)
		{
			//Debug.Log("File exists");
   			data = ReadData(UnlocksPath);

		    playerUnlocks = (PlayerUnlocks)JsonUtility.FromJson(data, typeof(PlayerUnlocks));
			
		}else{
			Debug.Log("Unlocks File does not exists or overrite is active");

			playerUnlocks = new PlayerUnlocks();
			playerUnlocks.Coins = 10000;
			playerUnlocks.Cars.Add(true);

			for(int i = 0 ; i<6;i++)
				playerUnlocks.Cars.Add(false);
			SaveData(UnlocksPath, JsonUtility.ToJson(playerUnlocks));

		}
   	}
   	void FindUpgrades(){
   		string data = "";
   		
		if (System.IO.File.Exists(UpgradePath) )
		{
			//Debug.Log("File exists");
   			data = ReadData(UpgradePath);

		    playerUpgrades = (PlayerUpgrades)JsonUtility.FromJson(data, typeof(PlayerUpgrades));
			
		}else{
			Debug.Log("Upgrades File does not exists or overrite is active");

			playerUpgrades = new PlayerUpgrades();


			for(int i = 0 ; i<7;i++)
				playerUpgrades.CarUpgrades.Add(new SingleCarUpgrade());


			SaveData(UpgradePath, JsonUtility.ToJson(playerUpgrades));

		}
   	}
   	public void SavePlayerUnlocks(Action callBack = null){
		SaveData(UnlocksPath, JsonUtility.ToJson(playerUnlocks));
		if(callBack !=null)
			callBack();
   	}
   	public void AddToCoins(int amount,Action callBack = null){
   		playerUnlocks.Coins += amount;
   		SaveData(UnlocksPath, JsonUtility.ToJson(playerUnlocks));
		if(callBack !=null)
			callBack();
   	}

   	public void SavePlayerUpgrades(Action callBack = null){
   		SaveData(UpgradePath, JsonUtility.ToJson(playerUpgrades));
		if(callBack !=null)
			callBack();
   	}
   	void SaveData(string _path, string s){
   		//Write some text to the test.txt file
        StreamWriter writer = new StreamWriter(_path, false);
        writer.WriteLine(s);
        writer.Close();

        //Re-import the file to update the reference in the editor
        AssetDatabase.ImportAsset(_path); 
   	}

   	string ReadData(string _path){
   		StreamReader reader = new StreamReader(_path); 
       	string s= reader.ReadToEnd();
        reader.Close();
   		return s;
   	}
}


[Serializable]
public class PlayerUnlocks{
	public List<bool> Cars = new List<bool>(){};
	public int Coins;
}
[Serializable]
public class PlayerUpgrades{
	public List<SingleCarUpgrade> CarUpgrades = new List<SingleCarUpgrade>(){};
	//public int Coins;
}
[Serializable]
public class SingleCarUpgrade{
	public int Speed = 0;
	public int Grip = 0;
	public int Accel = 0;

}
