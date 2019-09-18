using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
	PlayerInput input;
	float movement = .25f;
	float width = 15;

    float rotateSpeed = 4f;



    // //Tilting
    // float rotation = 180;
    // float maxAngle = 20;
    // float turnAngle = 0;



    // Start is called before the first frame update
    void Start()
    {
        input = PlayerInput.instance;
        
    }

    // Update is called once per frame
    void Update()
    {
      
        HandleRotation();
       // HandleTilting();
    }
    void HandleRotation(){
        float RotSpeed = rotateSpeed;

         if(input.Down())
             RotSpeed *= 2f;

          if(input.Left()){
            this.transform.eulerAngles = Vector3.Lerp(this.transform.eulerAngles, this.transform.eulerAngles - new Vector3(0,1,0) ,RotSpeed);
        }else if (input.Right() ){
            this.transform.eulerAngles = Vector3.Lerp(this.transform.eulerAngles, this.transform.eulerAngles + new Vector3(0,1,0) , RotSpeed);
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
