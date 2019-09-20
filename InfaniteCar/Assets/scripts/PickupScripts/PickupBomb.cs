using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupBomb : MonoBehaviour
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
            player.bSpawner.hasBomb = true;
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
