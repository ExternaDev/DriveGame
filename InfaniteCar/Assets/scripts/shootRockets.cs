using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootRockets : MonoBehaviour
{
    public GameObject rocket;
    PlayerInput input;
    List<RocketController> rockets = new List<RocketController>();
    List<RocketController> rocketsToRemove = new List<RocketController>();
    public float currentSpeed = .01f;
    public Transform barrel;
    public float thrust = 50;
    public float NumOfRockets = 20;
    public bool canShootRocket;
    //public bool startboll = false;
   // public Enemy enemyToAttack ;
    private Vector3 enemyToKill;
    public Vector3 enemyPos;
    
    void Start()
    {
        input = PlayerInput.instance;
        

    }

    void Update()
    {
        //Debug.Log(rocketsToRemove.Count);
        FindClosest();
        CreateRockets();
        //create rockets when active
        
        //update rockets , check to see if rockets need to be destroyed
        //destroy rockets when they are marked for death
        UpdateRockets();
        DeleteRockets();
    }

    private void CreateRockets()
    {
        if (input.ActionAlt() && canShootRocket)
        {
            

            AddRocket();
           //canShootRocket = false;
        }
    }
    private void FindClosest()
    {


       /* float distanceToClosestEnemy = Mathf.Infinity;
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
                if (input.ActionAlt() && canShootRocket)
                {
                    enemyToKill = closestEnemy.transform.position;

                }

                enemyPos = enemyToKill;


            }

        }*/


    }
    void UpdateRockets()
    {
        foreach (RocketController obj in rockets)
        {
            //check to see how long it has been alive 
            // if it has been alive for more then 3 seconds kill it
            if (obj.markedForExplode == true)
            {
                rocketsToRemove.Add(obj);
            }
            //if bullet gets too far away kill it
            if (obj.transform.position.z > 20)
            {
                //Debug.Log("kill me");
                rocketsToRemove.Add(obj);
            }
            //if (bullet.collision == true)
            // {
            //    bulletsToRemove.Add(obj); 
            // }        

            //check to see if bullet hits somthing
            //if it has mark for death and do damage 
        }
    }
    void DeleteRockets()
    {
        if (rocketsToRemove.Count > 0)
        {
            //destroys all bullets that are in the bullets to remove list 
            foreach (RocketController obj in rocketsToRemove)
            {
                rockets.Remove(obj);
                Destroy(obj.gameObject);
            }
            //clears bullets to remove after destorying all bullets
            rocketsToRemove.Clear();
        }
    }

    private void AddRocket()
    {
        for (int i = 0; i < NumOfRockets; i++)
        {
            RocketController obj = Instantiate(rocket, barrel.position, barrel.rotation, null).GetComponent<RocketController>();
            rockets.Add(obj);
            obj.rocketsShoot = this;
            //Debug.Log(rockets.Count);

            //obj.GetComponent<Rigidbody>().AddForce(transform.forward * -thrust, ForceMode.Force);
        }
    }
}

