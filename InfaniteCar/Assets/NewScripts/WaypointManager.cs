using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointManager : MonoBehaviour
{
	public Transform startPoint,endPoint;
	public List<Transform> middlepoint = new List<Transform>(); 


  
  	public Transform GetStart(){return startPoint;}
  	public Transform GetLast(){return endPoint;}
  	public Transform GetFirstMiddle(){return middlepoint[0];}
  	public Transform GetMiddleViaIndex(int i){
  		if(i< middlepoint.Count)
  		return middlepoint[i];
  		else return endPoint;
  	}

  	public int GetMiddleWaypointsCount(){return middlepoint.Count;}







}
