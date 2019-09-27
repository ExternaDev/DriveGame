using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarTilting : MonoBehaviour
{
	PlayerInput input;
	float rotataion = 180;
    float maxAngle = 35;
    float turnAngle = 0;
    GameManager GM;
    void Start()
    {
        GM = GameManager.instance;
        input = PlayerInput.instance;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!GM.GameRunning()) return;




        if(input.Right() && turnAngle < maxAngle){
            turnAngle +=4f;
        }else if(input.Left()&& turnAngle > -maxAngle){
            turnAngle -=4f;

        }else if(!input.Right() && !input.Left() ){
            //if not turning apply turn angle to car/camera
            turnAngle *= .9f;
        }

        this.transform.localRotation = Quaternion.Euler(new Vector3(0,turnAngle,0));
       
        PlayerController.instance.SetTurnAngle(turnAngle,maxAngle);
    }
}
