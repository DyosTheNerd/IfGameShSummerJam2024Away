using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.VFX;
//using UnityEngine.VFX;.

[RequireComponent(typeof(Rigidbody),
typeof(Collider))]
public class Rocket : ToolEffect
{
    public float MaxSpeed = 100.0f;
    public float Acceleration = 10.0f;
    public float LifeSpan = 20.0f;
    public float BlastRadius = 1.0f;
    //TODO generalize
    public float3 AsteroidCenter = new float3(32,32,32);
    public Rigidbody rb;

    public GameObject vfx;

    public override void Activate(ShipToolManager manager)
    {
        rb = GetComponent<Rigidbody>();
        transform.rotation = Quaternion.LookRotation(manager.GetAim(), AsteroidCenter - (float3)manager.transform.position);
        transform.position = manager.transform.position;
        
    }

    public override void Deactivate(ShipToolManager manager)
    {
        //do nothing
    }

    void FixedUpdate(){

        rb.AddForce(transform.forward * Acceleration , ForceMode.Acceleration);
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, MaxSpeed);
    }

    void Update(){
        LifeSpan -= Time.deltaTime;
        if(LifeSpan<=0 )Destroy(gameObject);
    }

    void OnCollisionEnter(Collision collision){
        VolumeManager.Instance.RemoveSphere(collision.contacts[0].point, new float3(1,1,1), BlastRadius );
        var colliders = Physics.OverlapSphere(collision.contacts[0].point, BlastRadius * 1.5f, LayerMask.GetMask("DormantPickup"), QueryTriggerInteraction.Collide);
        foreach(var collider in colliders)
        {
            PickUpStartup startUp = collider.GetComponent<PickUpStartup>();
            startUp.StartUp();
            startUp.score(manager.playerNumber);


        }
        colliders = Physics.OverlapSphere(collision.contacts[0].point, BlastRadius* 2f, LayerMask.GetMask("Terrain"), QueryTriggerInteraction.Collide);
        foreach(var collider in colliders){
            collider.GetComponent<HedronChunkCollider>().SetDirty();
            
        }

        GameObject go = Instantiate(vfx);
        go.transform.position = transform.position;
        VisualEffect vx =  go.GetComponent<VisualEffect>();
        vx.Play();
        
        SoundEffectsMaster.playSoundEffect("rocket");

        if (!accountedFor)
        {
            PointSystem.Instance.AddScore(manager.playerNumber, "shoot");
            accountedFor = true;
        }
        
        
        Destroy(gameObject);
    }

    bool accountedFor = false;

}
