using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

public class dummy_player : MonoBehaviour
{
    public float rotationSpeed;
    public float3 axis = new float3(1.0f, 0.0f, 0.0f);
    public float3 point;
    float timer;
    public float changeAxisAfter = 3.0f;
    Unity.Mathematics.Random random; 
    
    void Start(){
        random = new Unity.Mathematics.Random((uint)(math.frac(transform.position.x + 10000.21309082041f) * 10000.421984120741f));
    }
    void Update(){

        transform.RotateAround(point, axis, rotationSpeed * Time.deltaTime);
        timer += Time.deltaTime;
        if(timer >= changeAxisAfter){
            axis = math.normalize(random.NextFloat3());
            rotationSpeed *= -1.0f;
            timer = 0;
        }

    }

}
