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
    
    PlayerController pc;
    void Awake(){
    	pc = PlayerController.instance;
    }
    public void Init(bool _onComing){
    	tileMover = TileMover.instance; 

    	onComing = _onComing;
    	if(!onComing){
    		// int rand = Random.Range(0,10);
    		// if(rand > 7)
    		police = true;
    	}
    	

    	//if oncmine find closest wp from farthest
    	//if not oncoming start from first wp  
    	//currentTile = TileMover.instance.FindClosestTo(this.transform.position, onComing).GetComponent<Tile>();
    	currentTile = TileMover.instance.FindFirstTile(police);

    	//currentWaypoint = currentTile.FindClosestWayTo(this.transform.position);
    	currentWaypoint = currentTile.FindFirstWay(onComing);


    	//whatever tile the player is on, spawn on one before
			
		if(onComing)
			wayIndex =currentTile.GetWaypointCount();






    	// if(!onComing){
    	// 	this.transform.position = TileMover.instance.Tiles[0].waypoints[2].position;
    	// }else{
		if(onComing)
    		this.transform.position =LeftLane();
    	else
    		this.transform.position =RightLane();

    	
    }
    void Update()
    {
    	if(currentTile != null && currentWaypoint !=null){
    		// if(police && Vector3.Distance(this.transform.position, PlayerController.instance.transform.position) <15){
    		// 		inPursuit=true;
    		// 	}else{
    		// 		inPursuit=false;

    		// 	}
        		CheckWayPoint();
        	
        	Movement();
    	}else{
    		if(currentTile == null)
    		Debug.Log("<color=red> AI with no tile</color>");
    		if(currentWaypoint == null)
    		Debug.Log("<color=red> AI with no waypoint</color>");
    		Destroy(this.gameObject);
    	}
    }
    void Movement(){
    	Vector3 dir= Vector3.zero;
    	if(!onComing){
    	 	dir =RightLane() - this.transform.position;
        	this.transform.LookAt(RightLane());

    	}
    	else{
    	 	dir =LeftLane() - this.transform.position;

        	this.transform.LookAt(LeftLane());

    	}
				dir = dir.normalized;



        this.transform.position -= pc.playerForward * TileMover.instance.baseSpeed * (TileMover.instance.PlayerBrakeAmount);

        if(!police){
        	this.transform.position +=dir * .1f;
    	}else{
        	this.transform.position +=dir * .35f;

    	}


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
		    	if(wayIndex+1 < currentTile.waypoints.Count){
		    		wayIndex++;
		    		//if(!inPursuit)
		    		WayDirection = currentWaypoint.forward;
		    		currentWaypoint = currentTile.waypoints[wayIndex];
		    		//else
    				//currentWaypoint = PlayerController.instance.transform;

		    	}else{//Next tiles
		    		wayIndex =0;
		    		Debug.Log("Next Tile");
		    	 	currentTile = TileMover.instance.FindTileAfter(currentTile);
		    	 	

		    	 	if(currentTile == null)
		    	 		ReachedEndOfTrack();
		    	 	else{
		    	 		//if(!inPursuit)
		    	WayDirection = currentWaypoint.forward;

		    	 			currentWaypoint = currentTile.waypoints[wayIndex];
		    	 		//else
    						//currentWaypoint = PlayerController.instance.transform;

		    	 	}
		    	 }
	    	
	    }else{
	    	if(wayIndex-1 >=0){
	    		wayIndex--;
		    	WayDirection = -currentWaypoint.forward;

	    		currentWaypoint = currentTile.waypoints[wayIndex];
	    	}else{//Next tiles
	    	 	currentTile = TileMover.instance.FindTileBefore(currentTile);
	    		

	    	 	if(currentTile == null){
	    	 		ReachedEndOfTrack();
	    	 	}else{
	    			wayIndex =currentTile.GetWaypointCount()-1;
		    		WayDirection = -currentWaypoint.forward;

	    	 		currentWaypoint = currentTile.waypoints[wayIndex];
	    	 	}
	    	 }

	    }
    }
    void ReachedEndOfTrack(){
    	Debug.Log("End of track");
    	Destroy(this.gameObject);
    }
}
