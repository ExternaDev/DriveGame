﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DeathPanelManager : MonoBehaviour
{
    public TextMeshProUGUI CoinsText;
    GameManager GM;
    // Start is called before the first frame update
    void Start()
    {
    	GM = GameManager.instance;
        EventManager.OnPlayerDied += OnPlayerDied;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnPlayerDied(){
    	CoinsText.text = GM.GetCoinsCollected().ToString("00");
    }
}
