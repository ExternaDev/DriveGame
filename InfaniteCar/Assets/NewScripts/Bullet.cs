using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	float inittime;
	float lifeTime = 5f;
    // Start is called before the first frame update
    void Start()
    {
        inittime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - inittime > lifeTime){
        	Destroy(this);
        }
    }
}

public class BulletDelta : MonoBehaviour
{
	float lifeTime=0;
	float maxTime = 5f;

    // Update is called once per frame
    void Update()
    {

		lifeTime+=Time.fixedDeltaTime;

        if(lifeTime > maxTime){
        	Destroy(this);
        }
    }
}