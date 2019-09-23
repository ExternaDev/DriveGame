﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
	PlayerInput input;
	float movement = .25f;
	float width = 15;

   public float rotateSpeed = 2;


float IncreaseScale = 2.5f;
    // //Tilting
    // float rotation = 180;
    // float maxAngle = 20;
    // float turnAngle = 0;


    GameManager GM;
    // Start is called before the first frame update
    void Start()
    {
        GM = GameManager.instance;

        input = PlayerInput.instance;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!GM.GameRunning()) return;
      
        HandleRotation();
       // HandleTilting();
    }
    void HandleRotation(){
        //float RotSpeed = rotateSpeed;

        // if(input.Down())
             //RotSpeed *= 2f;

        if(input.Left()){
            rotateSpeed += Time.fixedDeltaTime*IncreaseScale;
            this.transform.eulerAngles = Vector3.Lerp(this.transform.eulerAngles, this.transform.eulerAngles - new Vector3(0,rotateSpeed,0) , 1);
        }else if (input.Right() ){
            rotateSpeed += Time.fixedDeltaTime*IncreaseScale;

            this.transform.eulerAngles = Vector3.Lerp(this.transform.eulerAngles, this.transform.eulerAngles + new Vector3(0,rotateSpeed,0) , 1);
        }else if(rotateSpeed>2f){
            rotateSpeed-= Time.fixedDeltaTime*IncreaseScale*4f;

        }

    }
    // void HandleTilting(){
    //     if(input.Right() && turnAngle < 25){
    //         turnAngle +=1.75f;
    //     }else if(input.Left()&& turnAngle > -25){
    //         turnAngle -=1.75f;

    //     }else 
    //         turnAngle *= .95f;

    //     this.transform.localRotation = Quaternion.Euler(new Vector3(0,turnAngle,0));

    // }
}
