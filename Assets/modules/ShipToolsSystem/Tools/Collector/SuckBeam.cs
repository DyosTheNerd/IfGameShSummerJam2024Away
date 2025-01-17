using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class SuckBeam : ToolEffect
{
    List<PickUpStartup> goldCubes = new List<PickUpStartup>();
    List<PickUpStartup> gottenCubes = new List<PickUpStartup>();
    
    public float SuckPower;
    public float MinDistance;
    
    

    public override void Activate(ShipToolManager manager)
    {
        transform.parent.rotation = Quaternion.LookRotation(manager.transform.parent.forward);
        transform.parent.position = manager.transform.position; 
        transform.parent.parent = manager.transform;
        
        SoundEffectsMaster.playSoundEffect("beam");
    }

    public override void Deactivate(ShipToolManager manager)
    {
        Destroy(gameObject.transform.parent.gameObject);
        
        SoundEffectsMaster.stopSoundEffect("beam");
    }

    void FixedUpdate(){
        foreach(var cube in goldCubes){
           Suck(cube);
        }

        foreach(var cube in gottenCubes)
            goldCubes.Remove(cube);
        gottenCubes.Clear();
    }


    public void Suck(PickUpStartup cube){
            var l = math.length(cube.transform.position - transform.position);
            if(l < MinDistance){
                Get(cube);
                
            }
            
    }
    void Get(PickUpStartup cube){
        Destroy(cube.gameObject);
        PointSystem.Instance.AddScore(this.manager.playerNumber, "collect");
        SoundEffectsMaster.playSoundEffect("point");
    }


    void OnTriggerEnter(Collider col){
        if (col.TryGetComponent<PickUpStartup>(out var c))
            Get(c);
    }

    void OnTriggerExit(Collider col){
       if(col.TryGetComponent<PickUpStartup>(out var c))
            goldCubes.Remove(c);
    }


}
