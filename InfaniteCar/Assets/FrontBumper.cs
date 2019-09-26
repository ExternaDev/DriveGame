using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontBumper : MonoBehaviour
{
    TileMover tilemover;
    

    void Start()
    {
        tilemover = TileMover.instance;
        

    }


    void Update()
    {

    }
    public void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Building")
        {
            //push the player to the left
            Debug.Log("fuck i hit a building F");
            tilemover.PlayerHitCar();
//            player

        }
    }
}
