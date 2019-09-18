using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
	public static PlayerInput instance;

	bool up = false;
	bool down = false;
	bool left = false;
	bool right = false;
	public bool Right(){ return right;}
	public bool Left(){ return left;}
	public bool Up(){ return up;}
	public bool Down(){ return down;}

	bool upPress = false;
	bool downPress = false;
	bool leftPress = false;
	bool rightPress = false;
	public bool RightPress(){ return rightPress;}
	public bool LeftPress(){ return leftPress;}
	public bool UpPress(){ return upPress;}
	public bool DownPress(){ return downPress;}


	bool upRelease = false;
	bool downRelease = false;
	bool leftRelease = false;
	bool rightRelease = false;
	public bool RightRelease(){ return rightRelease;}
	public bool LeftRelease(){ return leftRelease;}
	public bool UpRelease(){ return upRelease;}
	public bool DownRelease(){ return downRelease;}
    void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    		up = Input.GetKey(KeyCode.UpArrow);
    		down = Input.GetKey(KeyCode.DownArrow);
    		left = Input.GetKey(KeyCode.LeftArrow);
    		right = Input.GetKey(KeyCode.RightArrow);

    		upRelease = Input.GetKeyUp(KeyCode.UpArrow);
    		downRelease = Input.GetKeyUp(KeyCode.DownArrow);
    		leftRelease = Input.GetKeyUp(KeyCode.LeftArrow);
    		rightRelease = Input.GetKeyUp(KeyCode.RightArrow);

    		upPress = Input.GetKeyDown(KeyCode.UpArrow);
    		downPress = Input.GetKeyDown(KeyCode.DownArrow);
    		leftPress = Input.GetKeyDown(KeyCode.LeftArrow);
    		rightPress = Input.GetKeyDown(KeyCode.RightArrow);
    }
}
