using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilController : MonoBehaviour
{
    public bool isdead = false;
    public float speed;   
    public float deathTimer = 3;   
    private Vector3 bombpos;


    void FixedUpdate()
    {
        MoveOil();
        Timers();
    }

    void Timers()
    {
        if (deathTimer > 0)
        {
            deathTimer -= Time.deltaTime;
        }

        if (deathTimer <= 0)
        {
           isdead = true;
        }
    }

    void MoveOil()
    {
       
    }

    private void OnTriggerStay(Collider trig)
    {
        if (trig.gameObject.tag == "Enemy")
        {
            isdead = true;
            Debug.Log("ive fallen and i choose NOT to get up");         
        }

        if (trig.gameObject.tag != "Enemy")
        {

            //isdead = true;
            Debug.Log(trig.gameObject.tag);
        }
    }
}
