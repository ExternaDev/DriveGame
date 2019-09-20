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
    void Awake(){
    	pc = PlayerController.instance;
    }
    public void Init(bool _onComing){
    	tileMover = TileMover.instance; 

    	onComing = _onComing;
    	// if(!onComing){
    	// 	// int rand = Random.Range(0,10);
    	// 	// if(rand > 7)
    	// 	police = true;
    	// }
    	

    	//if oncmine find closest wp from farthest
    	//if not oncoming start from first wp  
    	//currentTile = TileMover.instance.FindClosestTo(this.transform.position, onComing).GetComponent<Tile>();
    	currentTile = TileMover.instance.FindFirstTile(police);

    	//currentWaypoint = currentTile.FindClosestWayTo(this.transform.position);
    	currentWaypoint = currentTile.FindFirstWay(onComing);


    	//whatever tile the player is on, spawn on one before
			
		if(onComing)
			wayIndex =currentTile.GetWaypointCount();



    transform.SetParent(currentTile.transform);



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
            isdead = true;
    		if(currentWaypoint == null)
    		Debug.Log("<color=red> AI with no waypoint</color>");
            isdead = true;
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



        //this.transform.position -= pc.playerForward * TileMover.instance.baseSpeed * (TileMover.instance.PlayerBrakeAmount - 1f);

    

        if(!police){
        	this.transform.position +=dir *.1f;// * .1f;
    	}else{
        	this.transform.position +=dir *.1f;//* .35f;

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


                if (currentTile == null)
                {
                    Debug.Log("fuck");
                    ReachedEndOfTrack();
                }
                else
                {
                    //if(!inPursuit)
                    WayDirection = currentWaypoint.forward;

                    currentWaypoint = currentTile.waypoints[wayIndex];
                    //else
                    //currentWaypoint = PlayerController.instance.transform;
                transform.SetParent(currentTile.transform);

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
                transform.SetParent(currentTile.transform);

	    	 	}
	    	 }

	    }
    }
    void ReachedEndOfTrack(){
    	Debug.Log("End of track");
    	
       isdead = true;
    }
    void OnCollisionEnter(Collision col)
    {
            Debug.Log("<color=red>something hit ai driver</color>");

        if (col.gameObject.tag == "Bullet")
        {
            isdead = true;
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
            
           PlayerController.instance.HitOtherCar();
           TileMover.instance.PlayerHitCar();
        }
    }

}
