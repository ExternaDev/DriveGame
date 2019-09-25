using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightBumper : MonoBehaviour
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
            //push the player to the left
            Debug.Log("fuck i hit a building R");

        }
    }
}
