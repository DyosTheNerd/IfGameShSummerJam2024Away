using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using System;

[RequireComponent(typeof(SphereCollider))]
public class PickUpStartup : MonoBehaviour
{

    private float3 finalPosition;
    public float3 FinalPosition { get => finalPosition; set => finalPosition = value; }

    private bool scored = false;
    
    IEnumerator StartUpRoutine(){
        enabled = false;
        while(math.length((float3)transform.position - FinalPosition) > 0.5f){

            transform.Translate((FinalPosition -(float3)transform.position) * 0.8f * Time.fixedDeltaTime);

            yield return new WaitForFixedUpdate(); 
        }
        gameObject.layer = 8;
    }

    public void StartUp(){
        var upAxis = math.normalize((float3)transform.position + UnityEngine.Random.value * 3.0f - Asteroid.Instance.Center);
        
        FinalPosition=  Asteroid.Instance.Center + upAxis * 34.0f;
        StartCoroutine(StartUpRoutine());
    }

    public void score(int playerId){
        if(scored)return;
        PointSystem.Instance.AddScore(playerId, "collect");
        scored = true;
    }
    

}
