using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetingArea : MonoBehaviour
{ 
    List<AIDriver> InRange = new List<AIDriver>();
    List<AIDriver> OutOfRange = new List<AIDriver>();

    void Start()
    {
        
    }

   
    void Update()
    {
        
    }
    public void OnTriggerStay(AIDriver col )
    {
        if (col.gameObject.tag == "Enemy")
        {
            // add this item to an arry of in range enemies
            InRange.Add(col);

        }
        
    }
    public void OnTriggerExit(AIDriver col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            // remove this item from the arry of in range enemies
            OutOfRange.Add(col);
        }

    }
    public void RemoveFromList()
    {
        
        if (OutOfRange.Count > 0) {

            //remove item from the in range list based of the out of range list
        }

    }
   
}
