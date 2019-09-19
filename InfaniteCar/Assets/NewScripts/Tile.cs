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


    public List<Transform> BuildLocations = new List<Transform>();
    public GameObject buildingprefab;
    void Update()
    {
        
    }
    void Start(){
        if(BuildLocations.Count>0){
            foreach(Transform t in BuildLocations){
                if(t.gameObject.activeInHierarchy){
                    GameObject building = Instantiate(buildingprefab, t.position, Quaternion.identity,t);
                    float height = Random.Range(7f,40f);
                    building.transform.localPosition += new Vector3(0,height/2f,0);
                    building.transform.localScale  = new Vector3(Random.Range(7f,10f), height,Random.Range(7f,10f) );

                }
            }
        }
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
            TileSpawner.instance.AddNewTile();

            PlayerController.instance.HitTile(this);

        }
    }


    //0 = straight 1 == left 2 == right
    public void PlayerIntersectionChange(int dir){
            if(dir != 0){
                RemoveFakes();
                TileMover.instance.FindTileAfter(this).RealignToTile(this,dir);
                TileSpawner.instance.AddTurnAmount(dir);

            }
    }
    void RemoveFakes(){
        foreach(GameObject obj in FakeEnds){
            obj.SetActive(false);
        }
    }
}
