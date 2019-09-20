using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarTilting : MonoBehaviour
{
	PlayerInput input;
	float rotataion = 180;
    float maxAngle = 20;
    float turnAngle = 0;
    void Start()
    {
        input = PlayerInput.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if(input.Right() && turnAngle < 25){
            turnAngle +=4f;
        }else if(input.Left()&& turnAngle > -25){
            turnAngle -=4f;

        }else if(!input.Right() && !input.Left() )
            turnAngle *= .95f;

        this.transform.localRotation = Quaternion.Euler(new Vector3(0,turnAngle,0));
       
        PlayerController.instance.SetTurnAngle(turnAngle);
    }
}
