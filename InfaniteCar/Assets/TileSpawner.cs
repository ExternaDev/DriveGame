using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class TileOptions{
	public string name;
	public GameObject Tile;
	public bool Use = true;
}
public class TileSpawner : MonoBehaviour
{

	public static TileSpawner instance;

	 [SerializeField]
	public List<TileOptions> TileTypes = new List<TileOptions>();
	TileMover mover;

	int segments = 6;
    public int turns = 0;
    public List<GameObject> pickups = new List<GameObject>();

	void Awake(){
		instance = this;
	}
    // Start is called before the first frame update
    void Start()
    {
        pickups = new List<GameObject>(Resources.LoadAll<GameObject>("Pickups"));

    	mover = GetComponent<TileMover>();
        SpawnFirstTiles();

    }
    public GameObject GetRandomPickup(){
    	return pickups[Random.Range(0, pickups.Count)];
    }
    void SpawnFirstTiles(){
    	for(int i = 0; i <segments;i ++){
    		Tile obj = Instantiate(TileTypes[0].Tile, new Vector3(0,0,(i*50)-50),Quaternion.identity,this.transform).GetComponent<Tile>();
    		mover.Tiles.Add(obj);
    	}
        PlayerController.instance.Init();
    }

     public  void AddNewTile(){
       // Debug.Log("Spawn tile");
        GameObject pref = FindNextTileType();
    	Tile obj = Instantiate(pref, Vector3.one * 100f ,Quaternion.identity,this.transform).GetComponent<Tile>();
       if(Random.Range(0,10) > 7)
       		obj.SpawnPickup();
       mover.Tiles.Add(obj);
       mover.TilesToRemove.Add( mover.Tiles[0]);

        obj.RealignToTile(mover.FindTileBefore(obj),0);
        
    }
    // // Update is called once per frame
    // void Update()
    // {
        
    // }

     public void AddTurnAmount(int dir){
     	Debug.Log("Add turn amount");
        if(dir==1){
            turns --;

         }else if(dir==2){
            turns ++;
            }
    }
    public int TilesSinceInter =3;
    GameObject FindNextTileType(){
        int rand = Random.Range(0,TileTypes.Count);
       if(!TileTypes[rand].Use )
            return FindNextTileType();

        if(TileTypes[rand].Tile.GetComponent<Tile>().isIntersection){
            if(TilesSinceInter <10){
                return FindNextTileType();
            }else if( TilesSinceInter > 10){
                 TilesSinceInter =0;

                return TileTypes[rand].Tile;
            }
        } 

        TilesSinceInter ++;
         if( TileTypes[rand].Tile.GetComponent<Tile>().direction == TileDirection.right){
            if(turns>=1){
                return FindNextTileType();
            }else{
                turns ++;
            }
        }else if(TileTypes[rand].Tile.GetComponent<Tile>().direction == TileDirection.left){
            if(turns<=-1){
                return FindNextTileType();
            }else{
                turns --;
            }
        }
        return TileTypes[rand].Tile;

    }
}
