using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupOil : MonoBehaviour
{
    PlayerController player;
    // Start is called before the first frame update
    void awake()
    {
        player = PlayerController.instance;

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnCollisionEnter(Collision col)
    {
        //Debug.Log("boom" + col.gameObject.name);
        // if the bullet hits an enemy it will do this
        if (col.gameObject.tag == "Player")
        {
            player.sRockets.canShootRocket = true;
            Debug.Log("you get a bo-bomb");
            //mark the enemy as dead
        }
        //if the bullet hits somthing that is not an enemy it will do this
        if (col.gameObject.tag != "Player")
        {

            //Debug.Log(col.gameObject.tag);
        }





    }
}