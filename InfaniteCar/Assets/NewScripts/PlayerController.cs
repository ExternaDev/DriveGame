using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerController : MonoBehaviour
{
	public static PlayerController instance;
    public Vector3 playerForward = new Vector3(0,0,1);
    // Start is called before the first frame update
    


    //public float TotalDistance = 0;
    //public float distanceToNextWay = 0;

    public Transform onComingWaypoint;
   public float NextWaytotalDistance =0;
    bool inited = false;

   // public TextMeshProUGUI  tex;

    public Tile currTile;
    public float turnAngle = 0;
    public void Init(){
        onComingWaypoint =TileMover.instance.Tiles[2].waypoints[0].transform;
        NextWaytotalDistance =  Mathf.Abs((onComingWaypoint.position - this.transform.position).magnitude);
       currTile = TileMover.instance.Tiles[1];
        
        inited = true;

    }
    void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        playerForward = this.transform.forward;

        // if(inited){
        //     distanceToNextWay = Mathf.Abs((onComingWaypoint.position - this.transform.position).magnitude);
        //     TotalDistance = (SmallTileCompleted * 50f);
        //     TotalDistance += (LargeTileCompleted * 80f);
        //     TotalDistance += TinyTileCompleted * 25f;
        //     TotalDistance += (NextWaytotalDistance -distanceToNextWay);
           

        //     tex.text = TotalDistance.ToString("00");
        // }
    }
    public bool IsStarted(){
        return inited;
    }
    public void SetTurnAngle(float t){
        turnAngle = Mathf.Clamp(t/25f, -1,1);
    }
    public void HitTile( Tile tile){
        GetComponent<CarStats>().HitTile(currTile);//pass in previouse tile to add to distance
        
        onComingWaypoint = TileMover.instance.Tiles[4].waypoints[0].transform;
        NextWaytotalDistance =  Mathf.Abs((onComingWaypoint.position - this.transform.position).magnitude);

       currTile = tile;

    }
}
