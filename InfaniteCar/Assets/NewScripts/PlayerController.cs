using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerController : MonoBehaviour
{
	public static PlayerController instance;
    public Vector3 playerForward = new Vector3(0,0,1);
    // Start is called before the first frame update
    int SmallTileCompleted=0;
    int LargeTileCompleted=0;
    int TinyTileCompleted=0;


    public float TotalDistance = 0;
    public float distanceToNextWay = 0;

    public Transform onComingWaypoint;
    float NextWaytotalDistance =0;
    bool inited = false;

    public TextMeshProUGUI  tex;

    public Tile currTile;
    public float turnAngle = 0;
    public void Init(){
        onComingWaypoint =TileMover.instance.Tiles[2].waypoints[0].transform;
        NextWaytotalDistance =  Mathf.Abs((onComingWaypoint.position - this.transform.position).magnitude);
       currTile = TileMover.instance.Tiles[1];
        
        inited = true;

    }
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        playerForward = this.transform.forward;
        if(inited){
            distanceToNextWay = Mathf.Abs((onComingWaypoint.position - this.transform.position).magnitude);
            TotalDistance = (SmallTileCompleted * 50f);
            TotalDistance += (LargeTileCompleted * 80f);
            TotalDistance += TinyTileCompleted * 25f;
            TotalDistance += (NextWaytotalDistance -distanceToNextWay);
           

            tex.text = TotalDistance.ToString("00");
        }






    }
    
    public void SetTurnAngle(float t){
        turnAngle = Mathf.Clamp(t/25f, -1,1);
    }
    public void HitTile( Tile tile){

        if(currTile.tileSize == TileSize.Big){
                LargeTileCompleted++;
            }else if(currTile.tileSize == TileSize.Small){
                SmallTileCompleted++;
            }else if(currTile.tileSize == TileSize.Tiny){

            }
        onComingWaypoint = TileMover.instance.Tiles[4].waypoints[0].transform;
        NextWaytotalDistance =  Mathf.Abs((onComingWaypoint.position - this.transform.position).magnitude);

       currTile = tile;

    }
}
