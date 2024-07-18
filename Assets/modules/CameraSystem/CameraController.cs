using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.Mathematics;
using Unity.VisualScripting;

public class CameraController : MonoBehaviour
{
    public List<GameObject> Players;


    public float3 initialLookAt;
    public float initialRadius;
    public float initialRotationSpeed;

    private float3 lookAtTarget;
    public float3 LookAtTarget
    {
        get => lookAtTarget; 
        set
        {
            lookAtTarget = value;
            transform.position = value;
        }
    }

    private float radius;
    public float Radius
    {
        get => radius;
        set{
            radius = value;
            UpdateRadius();
            }
    }

    /// <summary>
    /// Rotation Speed in Radians per Second ( i think )
    /// </summary>
    private float rotationSpeed;
    public float RotationSpeed { get => rotationSpeed; set => rotationSpeed = value; }


    void Start(){
        RotationSpeed = initialRotationSpeed;
        Radius = initialRadius;
        LookAtTarget = initialLookAt;
    }

    public void SetTargets(List<GameObject> targets)
    {
        Players.Clear();
        Players.AddRange(targets);
    }

    // Update is called once per frame
    void Update()
    {
        float3 midPoint = new();

        for (int i = 0; i < Players.Count; i++)
        {
            midPoint += (float3)Players[i].transform.position;
        }

        float3 direction = LookAtTarget - (midPoint / Players.Count);
        if (math.length(direction) < 0.01f) return;


        direction = math.normalize(direction);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(direction, Vector3.up), RotationSpeed * Time.deltaTime);
    }

    void UpdateRadius(){
            var childCamera = GetComponentInChildren<Camera>().transform;
            childCamera.transform.localPosition = new float3(0,0,1) * -radius;
    }


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawLine(Players[0].transform.position, Players[1].transform.position);

        Gizmos.color = Color.red;
        Gizmos.DrawLine((Players[0].transform.position + Players[1].transform.position) / 2, LookAtTarget);
    }


}
