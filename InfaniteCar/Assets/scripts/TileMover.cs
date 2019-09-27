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
    float InitialBaseSpeed = .3f;
	float playerSpeed = 0;
	//float offset = 0;
	float maxSpeed = .3f;
	float width=15;
    float sideForce = 0;
    public float bumpAmount = .25f;

    //public float currentSpeed = 0;
    PlayerInput input;


	//float Acceleration = .005f;
	//float BrakePower = .03f;
	//float turnAmount = .15f;
	public float PlayerBrakeAmount = 1;
	//float PlayerBrakeAmountDecay = .01f;


    float CarDataSpeed =0;
    float CarDataAccel =0;


    PlayerController PC;
    GameManager GM;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;

        EventManager.OnGameReset += OnGameReset;
        EventManager.OnResumeAftervideo += OnResumeAftervideo;

        InitialBaseSpeed = baseSpeed;
    }
    void Start(){
        GM = GameManager.instance;
        PC = PlayerController.instance;
        input = PlayerInput.instance;
        CarDataSpeed = CarMovement.instance.CarData.Speed; 
        CarDataAccel = CarMovement.instance.CarData.Acceleration; 
    }
    void OnGameReset(){
        baseSpeed = InitialBaseSpeed;
    }
    void OnResumeAftervideo(){
       // baseSpeed = InitialBaseSpeed;
    }


    // Update is called once per frame
    void FixedUpdate()
    {

        if(!GM.GameRunning())   return;

        MoveTiles(); 
        CheckTilestoRemove();
        GatherInput();

        if(PlayerBrakeAmount <2 && !input.Down())
            PlayerBrakeAmount += CarDataAccel;

        if(HitBreak<1){
            HitBreak+=Time.fixedDeltaTime;

        }
        if (Mathf.Abs(sideForce) > 0) {
            sideForce *= .9f;

        }
        
    }

    void GatherInput(){
    	baseSpeed += Time.fixedDeltaTime/ 1000f;
        
    	if(input.Down() && PlayerBrakeAmount >.5f){
            Debug.Log("down");
    		PlayerBrakeAmount -=CarDataAccel*5f ;
    	}
    }
    void CheckTilestoRemove(){
    	if(TilesToRemove.Count >0){
    		foreach(Tile obj in TilesToRemove){
               
		    	Tiles.Remove(obj);
		    	Destroy(obj.gameObject);
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
            obj.transform.position += DriftDirection() * .1f * DriftAbsValue(ang);
        }else if(ang <-driftThreshold){
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
    float HitBreak = 1; 
    public Vector3 GetMovementUpdate(){
        return PC.playerForward* GetSpeed() + GetSideForce();
            
       
    }
    

    public float GetSpeed(){
        return (baseSpeed * PlayerBrakeAmount) * HitBreak + CarDataSpeed;
    }
    public Vector3 GetSideForce()
    {
        return PC.playerRight * sideForce;
    }

    public float GetUnstoppableSpeed(){
        return (baseSpeed*2f);
    }


    public void PlayerHitCar(){
        HitBreak=0;
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

    public Tile FindLastTile(){
        return Tiles[Tiles.Count-1];
    }
    public Tile GetCurrentTile()
    {
        return Tiles[2];

    }
    public void BumpRight() {
        sideForce = bumpAmount;
    }
    public void BumpLeft()
    {
        sideForce = -bumpAmount;
    }
}
