using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetingArea : MonoBehaviour
{ 
    public static TargetingArea instance;
    public List<AIDriver> InRange = new List<AIDriver>();
    public List<AIDriver> OutOfRange = new List<AIDriver>();

    void Awake()
    {
        instance = this;
        
    }

   
    void Update()
    {
        RemoveFromList();
        
    }
    public void OnTriggerEnter(Collider col )
    {
        if (col.gameObject.tag == "Enemy")
        {
            // add this item to an arry of in range enemies
            InRange.Add(col.GetComponent<AIDriver>());

        }
        
    }
    public void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            // remove this item from the arry of in range enemies
           OutOfRange.Add(col.GetComponent<AIDriver>());
        }

    }
    public void RemoveFromList()
    {

        if (OutOfRange.Count > 0)
        {

            foreach (AIDriver obj in OutOfRange)
            {
                InRange.Remove(obj);
                
                Debug.Log("<color=green> fuck </color>");
            }
            OutOfRange.Clear();


            //clears bullets to remove after destorying all bullets



        }
        
    }
   
}
