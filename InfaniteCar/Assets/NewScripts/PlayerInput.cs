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
    bool action = false;
    bool actionAlt = false;

    public bool Up() { return up; }
    public bool Down() { return down; }
    public bool Left() { return left; }
    public bool Right() { return right; }
    public bool Action() { return action; }
    public bool ActionAlt() { return actionAlt; }

    bool upPress = false;
    bool downPress = false;
    bool leftPress = false;
    bool rightPress = false;
    bool actionPress = false;
    bool actionAltPress = false;

    public bool UpPress() { return upPress; }
    public bool DownPress() { return downPress; }
    public bool LeftPress() { return leftPress; }
    public bool RightPress() { return rightPress; }
    public bool ActionPress() { return actionPress; }
    public bool ActionAltPress() { return actionAltPress; }

    bool upRelease = false;
    bool downRelease = false;
    bool leftRelease = false;
    bool rightRelease = false;
    bool actionRelease = false;
    bool actionAltRelease = false;

    public bool UpRelease() { return upRelease; }
    public bool DownRelease() { return downRelease; }
    public bool LeftRelease() { return leftRelease; }
    public bool RightRelease() { return rightRelease; }
    public bool ActionRelease() { return actionRelease; }
    public bool ActionAltRelease() { return actionAltRelease; }

    void Awake()
    {
        instance = this;
    }
    
    void Update()
    {
        up = Input.GetKey(KeyCode.UpArrow);
        down = Input.GetKey(KeyCode.DownArrow);
        left = Input.GetKey(KeyCode.LeftArrow);
        right = Input.GetKey(KeyCode.RightArrow);
        action = Input.GetKey(KeyCode.Space);
        actionAlt = Input.GetKey(KeyCode.LeftShift);

        upRelease = Input.GetKeyUp(KeyCode.UpArrow);
        downRelease = Input.GetKeyUp(KeyCode.DownArrow);
        leftRelease = Input.GetKeyUp(KeyCode.LeftArrow);
        rightRelease = Input.GetKeyUp(KeyCode.RightArrow);
        actionRelease = Input.GetKeyUp(KeyCode.Space);
        actionAltRelease = Input.GetKeyUp(KeyCode.LeftShift);

        upPress = Input.GetKeyDown(KeyCode.UpArrow);
        downPress = Input.GetKeyDown(KeyCode.DownArrow);
        leftPress = Input.GetKeyDown(KeyCode.LeftArrow);
        rightPress = Input.GetKeyDown(KeyCode.RightArrow);
        actionPress = Input.GetKeyDown(KeyCode.Space);
        actionAltPress = Input.GetKeyDown(KeyCode.LeftShift);

    }
}