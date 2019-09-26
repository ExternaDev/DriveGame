using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{
    public bool isdead = false;
    public float speed;
    public float deathTimer = 3;
    private GameObject explode;
    public GameObject explotion;
    private Vector3 bombpos;
    public float bombRadius = 20;

    void Update()
    {
        MoveBomb();
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
            Detonate();
        }
    }

    void MoveBomb()
    {

    }

    private void OnTriggerEnter(Collider trig)
    {
        if (trig.gameObject.tag == "Enemy")
        {
            isdead = true;
            Debug.Log("big bada boom");
            Detonate();
        }

        if (trig.gameObject.tag != "Enemy")
        {

            //isdead = true;
            Debug.Log(trig.gameObject.tag);
        }
        //detect if the enemys are hitting the bomb
        //if they are hitting the bomb 
        //then detonate it       
    }

    private void Detonate()
    {
        RaycastHit hit; 

        isdead = true;
        explode = Instantiate(explotion, this.transform.position, Quaternion.identity);
        Destroy(explode, .5f);
        Destroy(this.gameObject);
        if (Physics.SphereCast(this.transform.position,bombRadius,this.transform.position,out hit, 10))
        {
            // kill what was hit
            Debug.Log("enemy hit a bomb");
        }
    }
    
}

