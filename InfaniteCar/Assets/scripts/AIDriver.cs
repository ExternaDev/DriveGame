using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDriver : MonoBehaviour
{
    // Start is called before the first frame update
    //right lane 
    TileMover tileMover;
   public bool onComing = false;

    //find closest tile from GM

    public Tile currentTile;
    public Transform currentWaypoint;
    float turnSpeed = .5f;
    float waypointMinDistance = 4f;
    int wayIndex = 0;
    float speed = .3f;
    float PoliceChaseSpeed= .4f;
    float laneOffset = 2f;
    bool police = false;
    bool inPursuit = false;
    public bool isdead = false;
    
    PlayerController pc;
    GameManager GM;
    void Start(){
        GM = GameManager.instance;
    	pc = PlayerController.instance;
    }
    public void Init(bool _onComing){
    	tileMover = TileMover.instance; 

    	onComing = _onComing;
    	
    	currentTile = TileMover.instance.FindLastTile();

    	currentWaypoint = currentTile.FindFirstWay(onComing);

			
		if(onComing)
			wayIndex =currentTile.GetWaypointCount();
        transform.SetParent(currentTile.transform);

		if(onComing)
    		this.transform.position =LeftLane();
    	else
    		this.transform.position =RightLane();

    	
    }
    void OnDestroy(){
        DebugDeath("On Destroy");
        GameManager.instance.AICars.RemoveDriver(this);

    }
    void OnDisable(){
        DebugDeath("On Disable");
        GameManager.instance.AICars.RemoveDriver(this);

    }
    void OnDisabled(){
        DebugDeath("On Disabled");
        GameManager.instance.AICars.RemoveDriver(this);

    }
    void DebugDeath(string info){
     //   if(police)
//        Debug.Log(info);
    }
    public void InitPolice(){
        tileMover = TileMover.instance; 
        
        currentTile = TileMover.instance.Tiles[1];

        currentWaypoint = currentTile.FindFirstWay(false);

        transform.SetParent(currentTile.transform);

        this.transform.position =RightLane();

        police=true;
    }

    void FixedUpdate()
    {
        if(!GM.GameRunning()) return;
    	if(currentTile != null && currentWaypoint !=null){
        	CheckWayPoint();
        	Movement();
    	}else{
    		if(currentTile == null)
    		  Debug.Log("<color=red> AI with no tile</color>");
            
    		if(currentWaypoint == null)
    		  Debug.Log("<color=red> AI with no waypoint</color>");

           GameManager.instance.AICars.RemoveDriver(this);
           isdead =true;
    	}
    }
    void Movement(){
        if(police) PoliceMovement();
        else StandardMovement();
    	
    }
    void PoliceMovement(){
        Vector3 dir= Vector3.zero;
        float distance  = Vector3.Distance(this.transform.position, pc.transform.position);

        if(distance>10f){
            dir =RightLane() - this.transform.position;
            this.transform.LookAt(RightLane());

            dir = dir.normalized;

            this.transform.position +=dir *tileMover.GetUnstoppableSpeed() *1f;
        }else{
            this.transform.LookAt(pc.transform.position);
            dir =pc.transform.position - this.transform.position;
            dir = dir.normalized;

            this.transform.position +=dir * tileMover.GetUnstoppableSpeed() *1.0f;
        }
    }
    void StandardMovement(){
        Vector3 dir= Vector3.zero;

        if(!onComing){
            dir =RightLane() - this.transform.position;
            this.transform.LookAt(RightLane());
        }else{
            dir =LeftLane() - this.transform.position;
            this.transform.LookAt(LeftLane());
        }
        dir = dir.normalized;

        this.transform.position +=dir *.1f;

    }

    
    Vector3 RightLane(){
    	return currentWaypoint.position + (currentWaypoint.right*laneOffset);
    }
    Vector3 LeftLane(){
    	return currentWaypoint.position - (currentWaypoint.right*laneOffset);
    }
    void CheckWayPoint(){
    	float dist = Vector3.Distance(this.transform.position, currentWaypoint.position);

    	if( Vector3.Distance(this.transform.position, currentWaypoint.position) < waypointMinDistance){
    		MoveToNextWay();
    	}
    }
	Vector3 WayDirection = new Vector3(0,0,1);
    
    void MoveToNextWay(){
    	Debug.Log("Find next waypoint");
    	if(!onComing){
		    	if(wayIndex + 1 < currentTile.waypoints.Count){
		    		wayIndex++;
		    		WayDirection = currentWaypoint.forward;
		    		currentWaypoint = currentTile.waypoints[wayIndex];
                   
		    	}else{//Next tiles
		    		wayIndex =0;
		    	 	currentTile = TileMover.instance.FindTileAfter(currentTile);

                    if (currentTile == null)
                    {
                        RemoveDriver("End of way");
                    }
                    else
                    {
                        WayDirection = currentWaypoint.forward;
                        currentWaypoint = currentTile.waypoints[wayIndex];
                        transform.SetParent(currentTile.transform);

                    }
		    	 }
	    	
	    }else{
	    	if(wayIndex-1 >=0){
	    		wayIndex--;
		    	WayDirection =-currentWaypoint.forward;

	    		currentWaypoint = currentTile.waypoints[wayIndex];
	    	}else{//Next tiles
	    	 	currentTile = TileMover.instance.FindTileBefore(currentTile);
	    		
	    	 	if(currentTile == null){
	    	 		RemoveDriver("End of way");
	    	 	}else{
	    			wayIndex =currentTile.GetWaypointCount()-1;
		    		WayDirection = -currentWaypoint.forward;

	    	 		currentWaypoint = currentTile.waypoints[wayIndex];
                    transform.SetParent(currentTile.transform);

	    	 	}
	    	 }

	    }
    }
    public void RemoveDriver(string info){
    	//Debug.Log("End of track");
    	GameManager.instance.AICars.RemoveDriver(this,info);
       isdead = true;
    }
    void OnCollisionEnter(Collision col)
    {
            Debug.Log("<color=red>something hit ai driver</color>");

        if (col.gameObject.tag == "Bullet")
        {
            isdead = true;
            if(!police)
            GameManager.instance.AICars.RemoveDriver(this,"collision");

            Debug.Log("boom" + col.gameObject.name);
            //mark the enemy as dead
        }else if (col.gameObject.tag != "Bullet") //if the bullet hits somthing that is not an enemy it will do this
        {
            Debug.Log(col.gameObject.tag);
        }

    }
    void OnTriggerEnter(Collider col){
        if (col.gameObject.tag == "Player") 
        {
            Debug.Log("<color=red>Player hit car from AIDriver on trigger</color>");
            isdead = true;
            if(!police)
            GameManager.instance.AICars.RemoveDriver(this,"Collision");
            
            PlayerController.instance.HitOtherCar();
            TileMover.instance.PlayerHitCar();
        }
    }

}
