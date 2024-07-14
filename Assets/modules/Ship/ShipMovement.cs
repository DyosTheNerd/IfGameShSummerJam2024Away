using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

public class ShipMovement : MonoBehaviour
{

    public string playerNumber;
    public float MaxSpeed;
    public float Acceleration;
    public float TurnAcceleration;
    public float MaxTurnSpeed;

    //velocity is 2d because we are stuck on a shell
    public float3 velocity;

    private float turnSpeed;

    public float3 OrbitCenter = new float3(32,32,32);

    float2 inputAxis;

    float3 rightAxis;
    float3 upAxis;

    public bool fire = false;
    
    void Update(){
        SetInputAxis(new float2(
            Input.GetAxis("Horizontal"+playerNumber),
            Input.GetAxis("Vertical"+playerNumber)
        ));


    }

    public void SetInputAxis(float2 axis){
        inputAxis = axis ;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        Accelerate();
        AccelerateTurn();

        MoveForward();
        Turn();


    }

    public void Accelerate(){
        if(inputAxis.y >= 0.05f){
            //remove rotation from velocity
            velocity =  math.mul(Quaternion.Inverse(transform.rotation),velocity);
            //adds new speed
            velocity += (float3)Vector3.forward * Acceleration * Time.fixedDeltaTime * inputAxis.y;
            //readds rotation
            velocity = transform.rotation * velocity;

            //clamps
            velocity /= MaxSpeed;
            velocity = math.clamp(velocity, new float3(-1, -1, -1), new float3(1,1,1));
            velocity *= MaxSpeed;
            fire = true;
        }else if(inputAxis.y <= -0.05f){
            velocity += velocity * inputAxis.y / 2.0f * Time.fixedDeltaTime;
            fire = false; 
        }
        else{
            velocity -= velocity * 0.05f * Time.fixedDeltaTime;
            fire = false; 
        }

    }

    public void AccelerateTurn(){
        turnSpeed += math.clamp(TurnAcceleration * Time.fixedDeltaTime * inputAxis.x, -MaxTurnSpeed, MaxTurnSpeed);

        if(math.abs(inputAxis.x) < 0.05f)
            turnSpeed += -turnSpeed * 0.2f * Time.fixedDeltaTime;
    }

    private void MoveForward(){
        upAxis = math.normalize((float3)transform.position - OrbitCenter) ;

        rightAxis = math.cross(upAxis, math.normalize(velocity));
        float3 v = math.mul(Quaternion.Inverse(transform.rotation), velocity);

        transform.RotateAround(OrbitCenter, rightAxis, 
            (math.length(velocity) /  math.length(OrbitCenter - (float3)transform.position)) * 360/ (2 * math.PI ));

        velocity = transform.rotation * v ;

    }
    private void Turn()
    {   
        quaternion axis = quaternion.AxisAngle(OrbitCenter - (float3)transform.position, turnSpeed * Time.fixedDeltaTime);
        transform.rotation = axis * transform.rotation ;
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
