using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftBumper : MonoBehaviour
{
    void Start()
    {

    }


    void Update()
    {

    }
    public void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Building")
        {
            //push the player to the right
            Debug.Log("fuck i hit a building L");

        }
    }
}

