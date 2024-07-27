using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
public class ShipToolManager : MonoBehaviour
{

    public int playerNumber;
    public float4 RotationLimits;

    public ShipTool[] tools;

    public int[] ammoReserves;
    
    public bool isShooting = false;
    
    public int currentToolIndex = 0;

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

    private void Start()
    {
        ammoReserves = new int[tools.Length];
        for (int i = 0; i < tools.Length; i++)
        {
            ammoReserves[i] = tools[i].maxAmmo;
        }
    }


    // Update is called once per frame
    void Update()
    {
        UpdateShipAimVectors();
        Aim(aimAxis);
    }


    public void BindControls(PlayerInput input)
    {
        InputActionMap  actions = input.actions.FindActionMap("ShipControls");
        if(actions == null) return;
        actions.FindAction("ShootMain").started += ShootBinding;
        actions.FindAction("ShootMain").canceled += ShootBinding;
    }

    public void ShootBinding(InputAction.CallbackContext context){
        if(context.started){
            ActivateTool();
        } else if(context.canceled){
            DeactivateTool();
        }
    }

    public void FixedUpdate()
    {
        if(isShooting)
        {
            ammoReserves[currentToolIndex] -= tools[currentToolIndex].constAmmoLoss;

            if (ammoReserves[currentToolIndex] <= 0)
            {
                DeactivateTool();
            }
        }
        
        
    }

    void UpdateShipAimVectors(){
        ShipAimCenter = math.normalize((float3)ShipTransform.position - Asteroid.Instance.Center);
        ShipAimForward = math.normalize(transform.forward);
        ShipAimRight = math.cross(ShipAimCenter, ShipAimForward);
    }

    void Aim(float2 axis){
        // aimAxis.x = Input.GetAxis("Horizontal"+this.playerNumber);
        // aimAxis.y = Input.GetAxis("Vertical"+this.playerNumber);
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
        if (ammoReserves[currentToolIndex] < tools[currentToolIndex].activationCost || ammoReserves[currentToolIndex] <= 0) return;
        
        isShooting = true;
        ammoReserves[currentToolIndex] -= tools[currentToolIndex].activationCost;
        
        
        tools[currentToolIndex].ActivateTool(this);
    }

    public void DeactivateTool()
    {
        if (isShooting)
        {
            tools[currentToolIndex].DeactivateTool(this);
            isShooting = false;    
        }
        
    }

}
