using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class SuckBeam : ToolEffect
{
    List<PickUpMovement> goldCubes = new List<PickUpMovement>();
    List<PickUpMovement> gottenCubes = new List<PickUpMovement>();
    ShipToolManager manager;

    public float SuckPower;
    public float MinDistance;

    public override void Activate(ShipToolManager manager)
    {
        this.manager = manager;
        transform.parent = manager.transform;
        transform.rotation = manager.transform.rotation;
        transform.position = manager.transform.position; 
    }

    public override void Deactivate(ShipToolManager manager)
    {
        Destroy(gameObject);

    }

    void FixedUpdate(){
        foreach(var cube in goldCubes){
           Suck(cube);
        }

        foreach(var cube in gottenCubes)
            goldCubes.Remove(cube);
        gottenCubes.Clear();
    }


    public void Suck(PickUpMovement cube){
            var l = math.length(cube.transform.position - transform.position);
            if(l < MinDistance){
                Get(cube);
                return;
            }
            cube.AddVelocity(cube.transform.position - transform.position * SuckPower/math.length(cube.transform.position - transform.position));
    }
    void Get(PickUpMovement cube){
        gottenCubes.Add(cube);
        Destroy(cube);
        PointSystem.Instance.AddCube();
    }


    void OnTriggerEnter(Collider col){
       if(col.TryGetComponent<PickUpMovement>(out var c))
            goldCubes.Add(c);
    }

    void OnTriggerExit(Collider col){
       if(col.TryGetComponent<PickUpMovement>(out var c))
            goldCubes.Remove(c);
    }


}
