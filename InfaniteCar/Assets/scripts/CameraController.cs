using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	public Transform IdealPosition;
	public Transform LookTarget;
    float TurnSpeed = 1f;
    // Update is called once per frame
    void Update()
    {
    	float distance = Vector3.Distance(transform.position, IdealPosition.position);
    	if(distance <2.5f && TurnSpeed >1.5f){
			TurnSpeed -=Time.fixedDeltaTime*10f;
    	}else if(distance >2.5f && TurnSpeed < 20f){
			TurnSpeed +=Time.fixedDeltaTime*10f;

    	}
        this.transform.position = Vector3.Slerp(transform.position, IdealPosition.position,Time.fixedDeltaTime*TurnSpeed);
    	
        transform.LookAt(LookTarget);

    }
}
