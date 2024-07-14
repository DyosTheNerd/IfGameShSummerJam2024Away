using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{

    public float LifeSpan = 3.0f;


    void Update(){
        LifeSpan -= Time.deltaTime;
        if(LifeSpan<=0 )Destroy(gameObject);
    }

}
