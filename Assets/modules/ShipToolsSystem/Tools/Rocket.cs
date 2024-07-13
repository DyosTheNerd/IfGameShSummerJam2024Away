using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Rocket : ToolEffect
{
    public float MaxSpeed = 100.0f;
    public float Acceleration = 10.0f;
    public float LifeSpan = 20.0f;
    //TODO generalize
    public float3 AsteroidCenter = new float3(32,32,32);
    public Rigidbody rb;

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


}
