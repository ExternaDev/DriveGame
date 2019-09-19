using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;


public class CarStats : MonoBehaviour
{
    
    public float OverallDistance = 0;
    public float GasAmount = 100f;
    public float DamageAmount = 0;
    PlayerController PC;

    public float distanceToNextWay = 0;

    public float TotalDistance = 0;
    int SmallTileCompleted=0;
    int LargeTileCompleted=0;
    int TinyTileCompleted=0;
    public TextMeshProUGUI DistanceText;


    public Image FuelGauge, DamageGuage;
    void Awake(){
    	PC = this.GetComponent<PlayerController>();
    }
    public void FillGas(){
    	GasAmount=100;
    }
    public void RepaireDamages(){
    	DamageAmount = 0;
    }
    public void TakeDamage(float amount){
    	DamageAmount += amount;
    	if(amount >=100)	PlayerDiedFromDamage();
    }
    public void PlayerDiedFromDamage(){
    	Debug.Log("<color=red>Player died from damage </color>");
    }
    void Update(){
    	GasAmount -= Time.fixedDeltaTime;
    	FindTotalDistance();
    	FuelGauge.fillAmount = GasAmount/100f;
    	DamageGuage.fillAmount = DamageAmount/100f;

    }
    void FindTotalDistance(){
    	if(PC.IsStarted()){
       	 distanceToNextWay = Mathf.Abs((PC.onComingWaypoint.position - this.transform.position).magnitude);

            TotalDistance = (SmallTileCompleted * 50f);
            TotalDistance += (LargeTileCompleted * 80f);
            TotalDistance += TinyTileCompleted * 25f;
            TotalDistance += (PC.NextWaytotalDistance - distanceToNextWay);
           

            DistanceText.text = TotalDistance.ToString("00");
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
