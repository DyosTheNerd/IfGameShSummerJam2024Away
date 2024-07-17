using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class ThrusterVfxControl : MonoBehaviour
{
    ShipMovement myShipMovement;
    VisualEffect myEffect;
    
    // Start is called before the first frame update
    void Start()
    {
        myShipMovement = this.GetComponentInParent<ShipMovement>();
        myEffect = GetComponent<VisualEffect>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (myShipMovement.fire)
        {
            myEffect.SetInt("Rate", 32);
        }
        else
        {
            myEffect.SetInt("Rate", 0);
        }
    }
}
