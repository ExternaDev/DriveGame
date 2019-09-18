using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public Renderer rend;
    PlayerInput input;
    public float timer;
    public bool hasShield;
    void Start()
    {
        input = PlayerInput.instance;
        rend = GetComponent<Renderer>();
        rend.enabled = false;        
    }
    
    void Update()
    {
        ShieldUpdate();
        
        
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
