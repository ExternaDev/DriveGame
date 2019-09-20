using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
	public GameObject othervehicle;
	public List<AIDriver> Enemies = new List<AIDriver>();
    List<AIDriver> EnemiesToRemove = new List<AIDriver>();
    public Transform enemyNearSpawnPoint;
	public Transform enemyFarSpawnPoint;

	bool toggle = false;
	float LastSpawnTime =0;
    //public bool override
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Space) ){
        	SpawnPC();
        }
        if (Time.time - LastSpawnTime > 2)
        {
            SpawnPC();
            LastSpawnTime = Time.time;
        }
        RemoveDriver();
        DeleteDriver();
        
    }
    void SpawnPC(){

    	AIDriver en = (AIDriver)Instantiate(othervehicle, Vector3.one*-50f, Quaternion.identity).GetComponent<AIDriver>();   	
    	
    	Enemies.Add(en);
        en.Init(toggle);
        toggle = !toggle;
    }
    public void DeleteDriver()
    {
        if (EnemiesToRemove.Count > 0)
        {
            //destroys all bullets that are in the bullets to remove list 
            foreach (AIDriver obj in EnemiesToRemove)
            {
                Enemies.Remove(obj);
                Destroy(obj.gameObject);
            }
            //clears bullets to remove after destorying all bullets
            EnemiesToRemove.Clear();
        }
    }
    public void RemoveDriver()
    {
        foreach (AIDriver obj in Enemies)
        {
            if (obj.isdead == true)
            {
                EnemiesToRemove.Add(obj);
            }
        }      
    }
}
