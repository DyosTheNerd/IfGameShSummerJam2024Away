using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
public class ShipToolManager : MonoBehaviour
{

    public int playerNumber;
    public float4 RotationLimits;
    public ShipTool activeTool;
    

    public float3 BaseAimDirection;
    private float3 AimDirection;

    private float3 ShipAimForward;
    private float3 ShipAimRight;
    private float3 ShipAimCenter;

    private Transform ShipTransform;
    private float2 aimAxis;

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

        if(Input.GetButtonDown("Shoot" + playerNumber)){
            ActivateTool();
        }

        if(Input.GetButtonUp("Shoot" + playerNumber)){
            DeactivateTool();
        }
    }

    void UpdateShipAimVectors(){
        ShipAimCenter = math.normalize((float3)ShipTransform.position - Asteroid.Instance.Center);
        ShipAimForward = math.normalize(transform.forward);
        ShipAimRight = math.cross(ShipAimCenter, ShipAimForward);
    }

    void Aim(float2 axis){
        aimAxis.x = Input.GetAxis("Horizontal" + playerNumber);
        aimAxis.y = Input.GetAxis("Vertical" + playerNumber);
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
    }

    public void DeactivateTool(){
        activeTool.DeactivateTool(this);
        
    }

}
