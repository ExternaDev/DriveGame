using System.Collections;
using System.Collections.Generic;
using UnityEngine;
///using UnityEngine.UI;

public class PickupUIController : MonoBehaviour
{
	public GameObject Rcokets, Bomb, oil, shield;
	PlayerController PC;
    // Start is called before the first frame update
    void Start()
    {
      PC =   PlayerController.instance;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Bomb.SetActive(PC.bSpawner.hasBomb);
        Rcokets.SetActive(PC.sRockets.canShootRocket);
        oil.SetActive(PC.oil.hasOil);
       // shield.SetActive(PC.BombSpawner.hasBomb);

    }
}
