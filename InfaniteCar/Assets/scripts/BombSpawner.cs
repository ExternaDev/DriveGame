using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombSpawner : MonoBehaviour
{
    public GameObject bomb;
    private GameObject bomby;
    public Transform ass;
    public bool hasBomb = true;
    BombController bombController;
    PlayerInput input;

    void Start()
    {
        input = PlayerInput.instance;
    }
    void Update()
    {
        BombUpdate();
        //IsShoot(); 
        Shoot();
    }

    private void BombUpdate()
    {
        //check tp see if the bomb is marked for death
        // if marked blow it up and send it to the remove function
        //Debug.Log("fuck");
        //if exists
        if (bombController != null && bombController.isdead)
        {  
            BombRemove();
        }
    }

    private void BombRemove()
    {
        //remove the bomb from the game
        Destroy(bomby);
    }

    // private void IsShoot()
    // {
    //     if (input.ActionAlt() && hasBomb)
    //     {
    //         BombSpawn();
    //         hasBomb = false;
    //     }
    // }
    public void Shoot(){

        if(hasBomb && input.ActionAlt())
        {
            Debug.Log("bomb");
            BombSpawn();
            //hasBomb = false;
        }
    }
    private void BombSpawn()
    {
        Tile t = TileMover.instance.GetCurrentTile();
        Debug.Log("i done been spawned");
        bomby = Instantiate(bomb, ass.position, ass.rotation, null);
        bomby.transform.SetParent(t.transform);
        bombController = bomby.GetComponent<BombController>();        
    }
}
