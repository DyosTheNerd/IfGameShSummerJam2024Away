using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using System;

[RequireComponent(typeof(SphereCollider))]
public class PickUpStartup : MonoBehaviour
{

    public float3 finalPosition;
    public float3 AsteroidCenter = new float3(32,32,32);

    IEnumerator StartUpRoutine(){
        enabled = false;
        while(math.length((float3)transform.position - finalPosition) > 0.5f){

            transform.Translate((finalPosition -(float3)transform.position) * 0.8f * Time.fixedDeltaTime);

            yield return new WaitForFixedUpdate(); 
        }
        TurnOn();
    }

    public void StartUp(){
        var upAxis = math.normalize((float3)transform.position + UnityEngine.Random.value * 3.0f - AsteroidCenter);
        
        finalPosition=  new float3(32, 32, 32) + upAxis * 34.0f;
        StartCoroutine(StartUpRoutine());

    }

    public void TurnOn(){
        gameObject.layer = 8;
        var movement = GetComponent<PickUpMovement>();
       movement.enabled = true;
      
       movement.SetVelocity(0.0f);

    }

}
