using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TileMover : MonoBehaviour
{
	public static TileMover instance;
	//public GameObject tile;
	public List<GameObject> TileTypes = new List<GameObject>();

	public List<Tile> Tiles = new List<Tile>();
	public List<Tile> TilesToRemove = new List<Tile>();
	//int segments = 6;
	public float baseSpeed = .75f;
	 float playerSpeed = 0;
	 //float offset = 0;
	 float maxSpeed = .3f;
	 float width=15;

	//public float currentSpeed = 0;
	PlayerInput input;


	 float Acceleration = .005f;
	 float BrakePower = .03f;
	 float turnAmount = .15f;
	public float PlayerBrakeAmount = 1;
	 float PlayerBrakeAmountDecay = .01f;




    PlayerController PC;

   // public int turns = 0;
    // Start is called before the first frame update
    void Awake()
    {
        PC =PlayerController.instance;
        input = PlayerInput.instance;
        instance = this;
     //   SpawnFirstTiles();

    }
    // void SpawnFirstTiles(){
    // 	for(int i = 0; i <segments;i ++){
    // 		Tile obj = Instantiate(TileTypes[0], new Vector3(0,0,(i*50)-50),Quaternion.identity,this.transform).GetComponent<Tile>();
    		
    // 		Tiles.Add(obj);
            
    // 	}
    //     PlayerController.instance.Init();
    // }
    // Update is called once per frame
    void Update()
    {
       MoveTiles(); 
      // CheckForDoneTile();

       CheckTilestoRemove();
       GatherInput();

       if(PlayerBrakeAmount <2)
       PlayerBrakeAmount += PlayerBrakeAmountDecay;
       
    }

    void GatherInput(){
    	baseSpeed += Time.fixedDeltaTime/ 500f;
    	if(input.Down() && PlayerBrakeAmount >1){
    		PlayerBrakeAmount -=PlayerBrakeAmountDecay*5f ;
    	}


    }
    void CheckTilestoRemove(){
    	if(TilesToRemove.Count >0){
    		foreach(Tile obj in TilesToRemove){

		    	Tiles.Remove(obj);
		    	Destroy(obj.gameObject);
	    		//AddNewTile();
    		}
    		TilesToRemove.Clear();
    	}
    }


    Vector3 Absolute(Vector3 v){
        return new Vector3(Mathf.Abs(v.x),  Mathf.Abs(v.y), Mathf.Abs(v.z));
    }



    public Tile LastTile(){
        return Tiles.Last();
    }
    Vector3 offset = Vector3.zero;
    void MoveTiles(){
    	foreach(Tile obj in Tiles){
            obj.transform.position -= GetMovementUpdate();
            ApplyDrift(obj.gameObject,PC.turnAngle);
            offset += Absolute(GetMovementUpdate());

    	}
    }
    float driftThreshold = .65f;
    void ApplyDrift(GameObject obj, float ang){
        if(ang >driftThreshold ){
          //  Debug.Log("Right drift");
            obj.transform.position += DriftDirection() * .1f * DriftAbsValue(ang);

        }else if(ang <-driftThreshold){
            // Debug.Log("Left drift");
            obj.transform.position -= DriftDirection() * .1f *DriftAbsValue(ang);
        }
    
    }
    float DriftAbsValue(float ang){
        float amount = (Mathf.Abs(ang) -driftThreshold) /  (1f- driftThreshold );
        return amount;
    }
    Vector3 DriftDirection(){
        return (PC.transform.right+ PC.transform.forward + PC.transform.forward).normalized;
    }
    public Vector3 GetMovementUpdate(){
        return PC.playerForward*(baseSpeed * PlayerBrakeAmount);
       //return PC.playerForward* baseSpeed;

    }
    public Tile FindTileAfter(Tile tile){
    	int index = Tiles.IndexOf(tile);
    	if(index +1 >= Tiles.Count)
    		return null;
    	return Tiles[index+1];
    }
    
    public Tile FindTileBefore(Tile tile){
    	int index = Tiles.IndexOf(tile);
    	if(index -1 <0)
    		return null;
    	return Tiles[index-1];
    }

    public Tile FindFirstTile(bool popo){
        if(!popo)
        return Tiles[Tiles.Count-1];
        else
        return Tiles[1];


    	// if(onComing){
    	// 		return Tiles[Tiles.Count-1];

    	// 	}else{
    	// 		return Tiles[0];
    	// 	}
    }
    public Tile GetCurrentTile()
    {
        return Tiles[2];

    }
}
