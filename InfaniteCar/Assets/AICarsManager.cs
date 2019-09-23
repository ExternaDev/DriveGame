using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICarsManager : MonoBehaviour
{
	public GameObject othervehicle;
	public List<AIDriver> Enemies = new List<AIDriver>();
    List<AIDriver> EnemiesToRemove = new List<AIDriver>();
    public Transform enemyNearSpawnPoint;
	public Transform enemyFarSpawnPoint;

	bool OnComing = false;
	float LastSpawnTime =0;
	GameManager GM;
    // Start is called before the first frame update
    void Start()
    {
        GM = GameManager.instance;
        EventManager.OnGameReset += OnGameReset;

    }

    void OnGameReset(){
    	foreach (AIDriver obj in Enemies)
        {
        	if(obj != null)
            Destroy(obj.gameObject);
        }
        Enemies.Clear();
        EnemiesToRemove.Clear();

    }
    void Update()
    {
    	if(GM.GameRunning()){


	        if(Input.GetKeyUp(KeyCode.Space) ){
	        	SpawnPC();
	        }
	        if (Time.time - LastSpawnTime > 2)
	        {
	            SpawnPC();
	            LastSpawnTime = Time.time;
	        }
    	}
        UpdateEnemiesToBeRemoved();
        
    }
    void SpawnPC(){

    	AIDriver en = (AIDriver)Instantiate(othervehicle, Vector3.one*-50f, Quaternion.identity).GetComponent<AIDriver>();   	
    	
    	Enemies.Add(en);
        en.Init(OnComing);
        OnComing = !OnComing;
    }
    public void UpdateEnemiesToBeRemoved()
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
    public void RemoveDriver(AIDriver driver)
    {
        EnemiesToRemove.Add(driver);    
    }
}
