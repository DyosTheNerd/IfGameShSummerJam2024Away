using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

public class PickUpMovement : MonoBehaviour
{


    public float MaxSpeed;
    public float Acceleration;

    //velocity is 2d because we are stuck on a shell
    public float3 velocity;


    public float3 OrbitCenter = new float3(32,32,32);


    float3 rightAxis;
    float3 upAxis;

    public void SetVelocity(float3 newVelocity){
        velocity = newVelocity;
    }

    public void AddVelocity(float3 acceleration){
            
            //remove rotation from velocity
            velocity =  math.mul(Quaternion.Inverse(transform.rotation),velocity);
            //adds new speed

            var velocityZ = math.projectsafe(acceleration, Vector3.forward, Vector3.forward * 0.001f);
            var velocityX = math.projectsafe(acceleration, rightAxis, Vector3.forward * 0.001f);

            velocity += velocityZ * Acceleration * Time.fixedDeltaTime;
            velocity += velocityX * Acceleration * Time.fixedDeltaTime;

            //readds rotation
            velocity = transform.rotation * velocity;

            //clamps
            velocity /= MaxSpeed;
            velocity = math.clamp(velocity, new float3(-1, -1, -1), new float3(1,1,1));
            velocity *= MaxSpeed;

            transform.rotation = Quaternion.LookRotation(velocity);
    }


    // Update is called once per frame
    void FixedUpdate(){

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
    void OnDrawGizmosSelected()
    {

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, (float3)transform.position + rightAxis * 5.0f);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, (float3)transform.position + upAxis * 5.0f);

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, (float3)transform.position + math.normalize(velocity) * 5.0f );

        Gizmos.color = Color.white;
        Gizmos.DrawLine(transform.position, (float3)transform.position +(OrbitCenter - (float3)transform.position) * 10f);

    }

}
