using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupRockets : MonoBehaviour
{
    PlayerController player;
    // Start is called before the first frame update
    void Awake()
    {
        player = PlayerController.instance;

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider col)
    {
        Debug.Log("you get a bo-bomb");
        //Debug.Log("boom" + col.gameObject.name);
        // if the bullet hits an enemy it will do this
        if (col.gameObject.tag == "Player")
        {
            player.sRockets.canShootRocket = true;
            player.oil.hasOil = false;
            player.bSpawner.hasBomb = false;
         
            Debug.Log("you get a bo-bomb");
            Destroy(this.gameObject);
            //mark the enemy as dead
        }
        //if the bullet hits somthing that is not an enemy it will do this
        if (col.gameObject.tag != "Player")
        {

            //Debug.Log(col.gameObject.tag);
        }





    }
}