using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.Mathematics;

public class CameraController : MonoBehaviour
{
    public List<GameObject> Players;

    // in  'meters'
    ///public float Radius;

    // angular speed in rad/second
    public float RotationSpeed;

    public float3 LookAtTarget;

    public void SetTargets(List<GameObject> targets){
        Players.Clear();
        Players.AddRange(targets);
    }

    // Update is called once per frame
    void Update()
    {
        float3 midPoint = new ();

        for (int i = 0; i < Players.Count; i++)
        {
            midPoint += (float3)Players[i].transform.position;
        }

        float3 direction = LookAtTarget - (midPoint/Players.Count);
        if(math.length(direction) < 0.01f) return;


        direction = math.normalize(direction);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(direction, Vector3.up), RotationSpeed * Time.deltaTime);
    }

    void OnDrawGizmosSelected(){
        Gizmos.color = Color.white;
        Gizmos.DrawLine(Players[0].transform.position, Players[1].transform.position);

        Gizmos.color = Color.red;
        Gizmos.DrawLine((Players[0].transform.position + Players[1].transform.position) / 2, LookAtTarget);
    }


}
