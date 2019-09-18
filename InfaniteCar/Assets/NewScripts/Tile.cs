using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public enum TileDirection{
    straight,right,left
}
public enum TileSize{
    Big,Small,Tiny
}
public class Tile : MonoBehaviour
{
	//public List<Material> mats = new List<Material>();
	public GameObject plane;
    public List<Transform> waypoints = new List<Transform>();
   

    public TileDirection direction = TileDirection.straight;

    public bool isIntersection = false;
    public TileSize tileSize = TileSize.Small;
    public List<Transform> EndNodes = new List<Transform>();
    // Update is called once per frame

    public List<GameObject> FakeEnds = new List<GameObject>();

    List<AIDriver> Cars = new List<AIDriver>();
    void Update()
    {
        
    }
    public int GetWaypointCount(){ return waypoints.Count;}
    public void RealignToTile(Tile tile,int end){
        //make list of cars Rel position
        Vector3 MoveAmount = this.transform.position;

        this.transform.position = tile.EndNodes[end].position + (tile.EndNodes[end].forward *50f);

        this.transform.rotation = tile.EndNodes[end].rotation;


        MoveAmount = transform.position - MoveAmount;//find offest amount


        AdjustCars(MoveAmount);


        //if not alst tile then RECURSION
         if(TileMover.instance.LastTile() != this){
            
            //TileMover.instance.FindTileBefore(this).gameObject.name =" Align this one";
            TileMover.instance.FindTileAfter(this).RealignToTile(this,0);
         }
    }
   
    public void AdjustCars(Vector3 amount){
       foreach(AIDriver car in GameManager.instance.Enemies){
            if(car.currentTile == this){
               car.transform.position+= amount;
            }
        }
    }
    //last is most recently added
    public void RealignWithPreviousetile(){

        RealignToTile(TileMover.instance.FindTileAfter(this),0);
    }


    public Transform FindFirstWay(bool onComing){
           // return waypoints[0];

        if(onComing){
            return waypoints[waypoints.Count-1];

        }else{
            return waypoints[0];

         }
    }
    bool hit = false;
    public void OnTriggerEnter(Collider col){
        if(col.gameObject.tag == "Player"){
            hit = true;
            TileMover.instance.AddNewTile();

            PlayerController.instance.HitTile(this);

        }
    }


    //0 = straight 1 == left 2 == right
    public void PlayerIntersectionChange(int dir){
            if(dir != 0){
                RemoveFakes();
                TileMover.instance.FindTileAfter(this).RealignToTile(this,dir);
                TileMover.instance.AddTurnAmount(dir);

            }
    }
    void RemoveFakes(){
        foreach(GameObject obj in FakeEnds){
            obj.SetActive(false);
        }
    }
}
