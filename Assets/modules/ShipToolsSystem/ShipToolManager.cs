using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
public class ShipToolManager : MonoBehaviour
{

    public string playerNumber;
    
    public Transform ShipTransform;
    public ShipTool activeTool;
    public float3 AsteroidCenter = new float3(32, 32, 32);

    // offset on vectors ShipForward and cross(ShipForward, ShipForward - PlanetCenter)
    public float3 BaseAimDirection;
    private float3 AimDirection;

    public float3 ShipAimForward;
    public float3 ShipAimRight;
    public float3 ShipAimCenter;
    public float4 RotationLimits;

    public float2 aimAxis;

    public float3 GetAim(){
        return AimDirection;
    }


    // Start is called before the first frame update
    void Awake()
    {
        ShipTransform = transform.parent;

        BaseAimDirection = math.normalize(BaseAimDirection);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateShipAimVectors();
        Aim(aimAxis);

        if(Input.GetButtonDown("Shoot"+this.playerNumber)){
            ActivateTool();
        }

        if(Input.GetButtonUp("Shoot"+this.playerNumber)){
            DeactivateTool();
        }
    }

   // public float3 

    void UpdateShipAimVectors(){
        ShipAimCenter = math.normalize((float3)ShipTransform.position - AsteroidCenter);
        ShipAimForward = math.normalize(transform.forward);
        ShipAimRight = math.cross(ShipAimCenter, ShipAimForward);
    }

    void Aim(float2 axis){
        aimAxis.x = Input.GetAxis("Horizontal"+this.playerNumber);
        aimAxis.y = Input.GetAxis("Vertical"+this.playerNumber);
       Quaternion q1;
       if(axis.y >= 0.0f)
            q1 = Quaternion.AngleAxis(aimAxis.y * RotationLimits.y, ShipAimRight);
       else q1 = Quaternion.Inverse(Quaternion.AngleAxis(-aimAxis.y * RotationLimits.y, ShipAimRight));

       Quaternion q2;
       if(axis.x >= 0.0f)
            q2 = Quaternion.AngleAxis(aimAxis.x * RotationLimits.x, ShipAimForward);
       else q2 = Quaternion.Inverse(Quaternion.AngleAxis(-aimAxis.x * RotationLimits.x, ShipAimForward));


       AimDirection = transform.rotation * q1 * q2 * BaseAimDirection;

    }

    void OnDrawGizmosSelected(){
        Gizmos.color = Color.red;

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, (float3)transform.position + ShipAimCenter * 1.5f);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, (float3)transform.position + ShipAimForward * 1.5f);
                Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, (float3)transform.position + ShipAimRight * 1.5f);

        Gizmos.color = Color.cyan;
         Gizmos.DrawLine(transform.position, (float3)transform.position + AimDirection * 15f);

        Gizmos.color = Color.yellow;
                 Gizmos.DrawLine(transform.position, transform.position + (transform.rotation * BaseAimDirection  * 2f));

    }

    public void ActivateTool(){
        activeTool.ActivateTool(this);
        Debug.Log("Button Pressed");
    }

    public void DeactivateTool(){
        activeTool.DeactivateTool(this);
        Debug.Log("Button Unpressed");
        
    }

}
