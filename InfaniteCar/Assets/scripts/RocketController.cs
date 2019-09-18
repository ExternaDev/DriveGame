using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketController : MonoBehaviour
{
    public float lifeTimer;
    public float rocketBounceTimer;
    public float enemyTimer;

    public bool markedForExplode = false;
    public bool rocketBounce = false;
    public bool noMoreSpeed = false;

 public Vector3 enemyPos;

    public shootRockets rocketsShoot;
    
    public float thrust;
    public float forwardThrust = 10;
    public float upThrust;
    public float speed = 1f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        thrust = Random.Range(-10f, 10f);
        upThrust = Random.Range(-10f, 0f);
    }

    // use for physics
    void FixedUpdate()
    {
        
        Timers();
       
        if (enemyTimer <= 0)
        {
            GoTowardsEnemy();
            noMoreSpeed = true;
        }

        if(enemyTimer > 0) {
            RocketVariations();
        }


    }
    private void GoTowardsEnemy()
    {
        float distanceToClosestEnemy = Mathf.Infinity;
        Enemy closestEnemy = null;

        //very taxing in bulk
        Enemy[] allEnemies = GameObject.FindObjectsOfType<Enemy>();

        foreach (Enemy currentEnemy in allEnemies)
        {
            float distanceToEnemy = (currentEnemy.transform.position - this.transform.position).sqrMagnitude;
            if (distanceToEnemy < distanceToClosestEnemy)
            {
                distanceToClosestEnemy = distanceToEnemy;


                closestEnemy = currentEnemy;
                enemyPos = closestEnemy.transform.position;
                
            }
        }
                float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, rocketsShoot.enemyPos, step);


        /*if (Vector3.Distance(transform.position, rocketsShoot.enemyPos) < 1f)
        {
           
            markedForExplode = true;
        }*/

        
    }
   
    private void RocketVariations()
    {
        rb.AddForce(transform.forward * forwardThrust, ForceMode.Force);
        rb.AddForce(transform.right * -thrust, ForceMode.Force);
        rb.AddForce(transform.up * -upThrust, ForceMode.Force);

        //rb.AddTorque(10, 10, 10, ForceMode.Force);
    }
    private void Timers()
    {
        if (enemyTimer > 0)
        {
            enemyTimer -= Time.deltaTime;
        }
        if (lifeTimer > 0)
        {
            lifeTimer -= Time.deltaTime;
        }
        else
        {
            //if the timer runs out then the bullet is marked to be deleted
            markedForExplode = true;
        }
        
    }
   
    private void OnCollisionEnter(Collision col)
    {
        Debug.Log("boom"  + col.gameObject.name);
        // if the bullet hits an enemy it will do this
        if (col.gameObject.tag == "Enemy")
        {
            markedForExplode = true;
        }
        //if the bullet hits somthing that is not an enemy it will do this
        if (col.gameObject.tag != "Enemy")
        {
            markedForExplode = true;
            //Debug.Log(col.gameObject.tag);
        }

    }
}
