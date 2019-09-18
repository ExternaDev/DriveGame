using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
	public GameObject othervehicle;
	public List<AIDriver> Enemies = new List<AIDriver>();
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
        if(Time.time - LastSpawnTime > 2){
        	SpawnPC();
        	LastSpawnTime = Time.time;
        }
    }
    void SpawnPC(){

    	AIDriver en = (AIDriver)Instantiate(othervehicle, Vector3.one*-50f, Quaternion.identity).GetComponent<AIDriver>();
    	
    	en.Init(toggle);
    	Enemies.Add(en);

    	toggle = !toggle;
    }
}
