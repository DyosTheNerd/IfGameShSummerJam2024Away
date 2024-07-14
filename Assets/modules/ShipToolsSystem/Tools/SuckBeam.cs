using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class SuckBeam : ToolEffect
{

    List<Rigidbody> goldCubes = new List<Rigidbody>();

    public override void Activate(ShipToolManager manager)
    {
        transform.parent = manager.transform;
    }

    public override void Deactivate(ShipToolManager manager)
    {
        //do nothing
    }

    public void Suck(){

    }

    void OnTriggerEnter(){

    }
    void OnTriggerExit(){

    }


}
