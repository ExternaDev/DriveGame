﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletController : MonoBehaviour
{
    public float howLongToLive ;
    public bool markedForDeath = false;
    public bool bulletBounce = false;
    public float bounceTimer ;
    Rigidbody rb;
    void Start(){
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        //timer for each bullet 
        if (howLongToLive > 0)
        {
            howLongToLive -= Time.deltaTime;
        }
        else
        {
            //if the timer runs out then the bullet is marked to be deleted
            markedForDeath = true;
        }
        if (bulletBounce == true)
        {

            if (bounceTimer > 0)
            {
                bounceTimer -= Time.deltaTime;
            }
            else
            {
                //if the timer runs out then the bullet is marked to be deleted
                markedForDeath = true;

            }

        }

       // this.transform.position -= TileMover.instance.GetMovementUpdate();
        //rb.AddForce(TileMover.instance.GetMovementUpdate(), ForceMode.Force);
     


    }
    private void OnCollisionEnter(Collision col)
    {
        Debug.Log("boom" +col.gameObject.name);
        // if the bullet hits an enemy it will do this
        if (col.gameObject.tag == "Enemy")
        {
            markedForDeath = true;
        }
        //if the bullet hits somthing that is not an enemy it will do this
        if (col.gameObject.tag != "Enemy")
        {
            bulletBounce = true;
            Debug.Log(col.gameObject.tag);
        }
       


    }
}