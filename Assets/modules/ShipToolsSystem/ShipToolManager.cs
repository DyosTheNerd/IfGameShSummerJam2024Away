using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
public class ShipToolManager : MonoBehaviour
{


    public InputActionAsset actions;

    public Transform ShipTransform;
    public ShipTool activeTool;
    public float3 AsteroidCenter = new float3(32, 32, 32);

    // offset on vectors ShipForward and cross(ShipForward, ShipForward - PlanetCenter)
    public Quaternion AimDirection;
    public float3 ShipAimForward;
    public float3 ShipAimRight;
    public float3 ShipAimCenter;
    public float2 RotationLimits;

    public float2 aimAxis;


    // Start is called before the first frame update
    void Awake()
    {
        ShipTransform = transform.parent;
        // actions.FindActionMap("ShipAimTest").FindAction("HorizontalAxis").performed += 
        //                                 (InputAction.CallbackContext context) => {aimAxis.x = context.ReadValue<float>();};
        // actions.FindActionMap("ShipAimTest").FindAction("VerticalAxis").performed += 
        //                                 (InputAction.CallbackContext context) => {aimAxis.y = context.ReadValue<float>();};
    }

    // Update is called once per frame
    void Update()
    {
        UpdateShipAimVectors();
        Aim(aimAxis);
    }

   // public float3 

    void UpdateShipAimVectors(){
        ShipAimCenter = math.normalize((float3)ShipTransform.position - AsteroidCenter);
        ShipAimForward = math.normalize(transform.forward);
        ShipAimRight = math.cross(ShipAimCenter, ShipAimForward);
    }

    void Aim(float2 axis){
        aimAxis.x = Input.GetAxis("Horizontal");
        aimAxis.y = Input.GetAxis("Vertical");
    
       Quaternion q1 = Quaternion.AngleAxis(aimAxis.y * RotationLimits.y, ShipAimRight);
       Quaternion q2 = Quaternion.AngleAxis(aimAxis.x * RotationLimits.x, ShipAimCenter);

       AimDirection = q1 * q2 * Quaternion.AngleAxis(0, ShipAimForward);

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
         Gizmos.DrawLine(transform.position, transform.position + (AimDirection * ShipTransform.forward  * 30f));

        //Gizmos.DrawLine(transform.position, )
    }
}
