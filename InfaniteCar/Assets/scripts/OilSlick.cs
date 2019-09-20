using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilSlick : MonoBehaviour
{ 
    public GameObject oil;
    private GameObject oily;
    public Transform ass;
    public bool hasOil = true;
    OilController oilController;
    PlayerInput input;

    void Start()
    {
        input = PlayerInput.instance;
    }

    void Update()
    {
        OilUpdate();
        IsShoot();
    }

    private void OilUpdate()
    {
        //check tp see if the bomb is marked for death
        // if marked blow it up and send it to the remove function

        //Debug.Log("fuck");
        //if exists
        if (oilController != null && oilController.isdead)
        {
            OilRemove();
        }
    }

    private void OilRemove()
    {
        //remove the bomb from the game
        Destroy(oily);
    }

    private void IsShoot()
    {
        if (input.ActionAlt() && hasOil)
        {
            BombSpawn();
            hasOil = false;
        }
    }

    private void BombSpawn()
    {
        Tile t = TileMover.instance.GetCurrentTile();

        Debug.Log("i done been spawned");
        oily = Instantiate(oil, ass.position, ass.rotation, null);
        oily.transform.SetParent(t.transform);
        oilController = oily.GetComponent<OilController>();
    }
}