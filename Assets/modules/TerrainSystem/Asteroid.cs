using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

[RequireComponent(typeof(Hedronizer), typeof(HedronizerRenderer))]
public class Asteroid : MonoBehaviour
{
    private static Asteroid _instance;
    public static Asteroid Instance { 
        get{if(_instance == null){Debug.Log("Missing Asteroid");} return _instance;}
        set{if(_instance != null){Debug.Log("Extra Asteroid in scene.");}}
    }

    private float3 _center = new float3(32,32,32);
    public float3 Center { get {return _center;}}

    public Hedronizer hedronizer;
    public HedronChunkCollisionManager colliderManager;
    public VolumeManager volume;
    public Material asteroidFiller;
    public int3 resolution = new int3(64, 64,64); 
    public float3 scale = new float3(64, 64, 64);

    

    void Awake(){
        Instance = this;
        hedronizer = GetComponent<Hedronizer>();
        volume = GetComponent<VolumeManager>();
        colliderManager = GetComponent<HedronChunkCollisionManager>();
    }
    
    // Start is called before the first frame update
    void Start()
    {

        volume.Initialize(resolution, new float3(64, 64, 64));
        hedronizer.volumeManager = volume;
        hedronizer.Initialize();
        colliderManager.Initialize();

    }

    public void CarveSphere(float3 position, float radius){
        volume.RemoveSphere(position, new float3(1,1,1), radius);
    }
    

}
