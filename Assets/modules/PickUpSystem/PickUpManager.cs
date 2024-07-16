using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class PickUpManager : MonoBehaviour
{
    private static PickUpManager _instance;
    public static PickUpManager Instance { 
        get{if(_instance == null){Debug.Log("Missing PickUpManager");} return _instance;}
        set{if(_instance != null){Debug.Log("Extra PickUpManagers in scene.");}}
    }
    public GameObject pickUpPrefab;
    public float3 Center = new float3(32,32,32);
    public int NumberOfPickups = 1000;

    public float2 RadiusLimits = new float2(0, 30);
    // Start is called before the first frame update

    void Awake(){

        }
    void Start()
    {
        Instance = this;
        for(int i = 0; i < NumberOfPickups; i++){
            var pickup = Instantiate(pickUpPrefab);
            var direction = (UnityEngine.Random.rotationUniform * Vector3.up);
            pickup.transform.position = Center + (float3)(direction * (RadiusLimits.y - RadiusLimits.x) * UnityEngine.Random.value + direction * RadiusLimits.x);
        }
    }

}
