using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{

    public GameObject currentHitObj;

    public bool isdead = false;
    public float speed;
    public float deathTimer = 3;
    private GameObject explode;
    public GameObject explotion;
    private Vector3 bombpos;
    
    AIDriver aidriver;

    public float maxDistance;
    public LayerMask layerMask;
    public float bombRadius = 20;
    RaycastHit hit;
    private Vector3 origin;
    private Vector3 direction;

    private float currentHitDis;

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
           // Debug.Log(trig.gameObject.tag);
        }
        //detect if the enemys are hitting the bomb
        //if they are hitting the bomb 
        //then detonate it       
    }

    private void Detonate()
    {
        origin = transform.position;
        direction = transform.forward;
        isdead = true;
        explode = Instantiate(explotion, this.transform.position, Quaternion.identity);
        Destroy(explode, .5f);
        Explosion(origin,bombRadius);
        Destroy(this.gameObject);

        //if (Physics.SphereCast(origin,bombRadius,direction,out hit, maxDistance,layerMask,QueryTriggerInteraction.UseGlobal))
        //{
            // kill what was hit
            //if (hit.)
           // currentHitObj = hit.transform.gameObject;
           // currentHitDis = hit.distance;
           // hit.transform.gameObject.name;
          // aidriver = hit.rigidbody.GetComponent<AIDriver>();
           //(hit.collider.GetComponent<AIDriver>());
           // Debug.Log(hit.collider.tag);
           
            //Debug.DrawRay(hit.point,,Color.white);
       // }



    }
    void Explosion(Vector3 c, float r)
    {
        Collider[] hitColliders = Physics.OverlapSphere(c, r);
        int i = 0;
        while (i < hitColliders.Length)
        {
            if (hitColliders[i].gameObject.tag == "Enemy")
            {
                GameManager.instance.AICars.RemoveDriver(hitColliders[i].GetComponent<AIDriver>());               

            }
            i++;
        }



    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(origin + direction * currentHitDis, bombRadius);
    }

}

