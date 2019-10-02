using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
	public static PlayerData instance;
	public CarDataScriptableObject currentSelection;

	void Awake(){
		// if(instance != null){
		// 	DestroyImmediate(instance.gameObject);

		// 	instance = this;
		// 	DontDestroyOnLoad(this.gameObject);
		// }else{
		// 	instance = this;
			
		// }
		if(instance == null){
			instance = this;
			DontDestroyOnLoad(this.gameObject);
		}else{
			DestroyImmediate(this.gameObject);
		}
	}
   

}
