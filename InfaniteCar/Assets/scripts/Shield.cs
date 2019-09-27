using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public Renderer rend;
    CarStats CS;
    PlayerInput input;
    public float timer;
    public bool hasShield;
    void Start()
    {
        CS = CarStats.instance;
        input = PlayerInput.instance;
        rend = GetComponent<Renderer>();
        rend.enabled = false;        
    }
    
    void FixedUpdate()
    {
        ShieldUpdate();
        if (rend.enabled)
        {
            CS.hasShield = true;
        }
        else
        {
            CS.hasShield = false;
        }
        
        
    }
    void Timers()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        if (timer <= 0)
        {
            rend.enabled = false;
        }


    }
    void ShieldUpdate()
    {
        if (input.ActionAlt() && hasShield)
        {
            if (rend.enabled != true)
            {
                rend.enabled = true;
                timer = 3;
                hasShield = false;
            }
        }

        if (rend.enabled)
        {
            Timers();


        }



    }
}
