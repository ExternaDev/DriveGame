using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
	bool hit = false;
    CoinsObjectManager COM;
    public void SetParent(CoinsObjectManager c){
    	COM=c;
    }
    void OnTriggerEnter(Collider col){
    	if(col.gameObject.tag == "Player" && !hit){
    		col.GetComponentInParent<CarStats>().AddCoinPints(1);
    		COM.Remove(this);
        }
    }
}
