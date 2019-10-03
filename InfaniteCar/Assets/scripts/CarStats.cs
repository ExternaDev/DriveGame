using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;


public class CarStats : MonoBehaviour
{

    public static CarStats instance;
   // public float OverallDistance = 0;
    public float GasAmount = 100f;
    public float DamageAmount = 0;
    PlayerController PC;

    public float distanceToNextWay = 0;

    public float TotalDistance = 0;
    int SmallTileCompleted=0;
    int LargeTileCompleted=0;
    int TinyTileCompleted=0;
    public TextMeshProUGUI DistanceText;
    public TextMeshProUGUI CoinsText;

   // public bool hasShield = false;



    public Image FuelGauge, DamageGuage,DistanceGauge;
    GameManager GM;
   
    public int PointsCollected = 0;
    public bool DebugInvinsable =false;
    PlayerData playerData;
   // public Event PlayerDied;
    void Awake(){
        instance = this;
    	PC = this.GetComponent<PlayerController>();
      
       // OnPlayerDied += 
        EventManager.OnGameReset += OnGameReset;
        EventManager.OnResumeAftervideo += OnResumeAftervideo;

        
    }
    void Start(){
        playerData =PlayerData.instance;
    	  GM = GameManager.instance;
       
    }
    void FixedUpdate(){
        if(!GM.GameRunning()) return;

        GasAmount -= Time.fixedDeltaTime;
        FuelGauge.fillAmount = GasAmount/100f;
        DamageGuage.fillAmount = DamageAmount/100f;
        FindTotalDistance();
        UpdateDistanceGauge();
        ZeroGas();

    }
    void UpdateDistanceGauge(){
        if(playerData.playerUnlocks.Distance[0] != 0 && TotalDistance < playerData.playerUnlocks.Distance[0]){
            DistanceGauge.fillAmount = TotalDistance/playerData.playerUnlocks.Distance[0];
        }else{


            for(float i = playerData.playerUnlocks.Distance[0];i <=TotalDistance;i+=500){
                DistanceGauge.fillAmount = TotalDistance/ (i+500);
                //Debug.Log(i+"  " +playerData.playerUnlocks.Distance[0]+ "  "+  TotalDistance +"  " + TotalDistance/( i+500));
            }
        }
    }
    public void FillGas(){
    	GasAmount=100;
    }
    public void RepaireDamages(){
    	DamageAmount = 0;
    }
    public void TakeDamage(float amount)
    {
        if(DebugInvinsable) return;
        Debug.Log("Player took " + amount  + " damage");
        
        DamageAmount += amount;
            if (DamageAmount >= 100) PlayerDiedFromDamage();
        
    }
    public void PlayerDiedFromDamage(){
    	Debug.Log("<color=red>Player died from damage </color>");
        //PC.PlayerDied();
        GM.SetCoinsCollected(PointsCollected);
        EventManager.instance.PlayerDied();

    }
    public void PlayerDiedFromGas()
    {
        Debug.Log("<color=red>Player died from damage </color>");
        //PC.PlayerDied();
        GM.SetCoinsCollected(PointsCollected);
        EventManager.instance.PlayerDied();

    }
    public void AddCoinPints(int i){
        PointsCollected+=i;
        CoinsText.text = PointsCollected.ToString("00");
        
    }
    public void SaveGameData(){
        PlayerData.instance.AddToCoins(PointsCollected);
    }
    void OnGameReset(){
       

        bool save = PlayerData.instance.CheckDistance(TotalDistance,0);
        if(PointsCollected >0 || save)
            SaveGameData();


        TotalDistance=0;
        SmallTileCompleted=0;
        LargeTileCompleted=0;
        TinyTileCompleted=0;
        GasAmount =100;
        DamageAmount=0;
        PointsCollected=0;



        CoinsText.text = PointsCollected.ToString("00");

    }
    void OnResumeAftervideo(){
        GasAmount =100;
        DamageAmount=0;
    }
    
    void FindTotalDistance(){
       	distanceToNextWay = Mathf.Abs((PC.onComingWaypoint.position - this.transform.position).magnitude);

        TotalDistance = (SmallTileCompleted * 50f);
        TotalDistance += (LargeTileCompleted * 80f);
        TotalDistance += TinyTileCompleted * 25f;
        TotalDistance += (PC.NextWaytotalDistance - distanceToNextWay);
           

        DistanceText.text = TotalDistance.ToString("00");
        
    }
    public void ZeroGas()
    {
        if (GasAmount <= 0)
        {
            PlayerDiedFromGas();
        }


    }
    public void HitTile( Tile tile){
    	if(tile.tileSize == TileSize.Big){
                LargeTileCompleted++;
            }else if(tile.tileSize == TileSize.Small){
                SmallTileCompleted++;
            }else if(tile.tileSize == TileSize.Tiny){

            }
    }
}
