using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using UnityEngine.InputSystem;
public class StationMovement : MonoBehaviour
{
    
    float3 rightAxis;
    float3 upAxis;
    public float3 velocity;

    public float3 OrbitCenter = new float3(32,32,32);

    void FixedUpdate()
    {
        MoveForward();
    }
    
    private void MoveForward(){
        upAxis = math.normalize((float3)transform.position - OrbitCenter) ;

        rightAxis = math.cross(upAxis, math.normalize(velocity));
        float3 v = math.mul(Quaternion.Inverse(transform.rotation), velocity);

        transform.RotateAround(OrbitCenter, rightAxis, 
            (math.length(velocity) /  math.length(OrbitCenter - (float3)transform.position)) * 360/ (2 * math.PI ));

        velocity = transform.rotation * v ;

    }
    
}

