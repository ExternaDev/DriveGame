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
            //isdead = true;
        }
    }

    void MoveBomb()
    {
       
    }

    private void OnTriggerStay(Collider trig)
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
        isdead = true;
        explode = Instantiate(explotion, this.transform.position,Quaternion.identity);
        Destroy(explode, 2);
        // switch the bomb for an explosion or add particels to make it look like an explostion
        //mark the bomb for death
    }
}
