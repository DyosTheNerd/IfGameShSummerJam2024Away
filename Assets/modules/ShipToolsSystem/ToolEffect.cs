using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolEffect : MonoBehaviour
{
    
    protected ShipToolManager manager;
    public void SetToolManager(ShipToolManager manager)
    {
        if (this.manager != null)
        {
            Debug.Log("pre" + this.manager.playerNumber);
        }

        this.manager = manager;
        Debug.Log("post" + this.manager.playerNumber);
    }
    
    // Update is called once per frame
    public virtual void Activate(ShipToolManager manager)
    {
        
    }

    public virtual void Deactivate(ShipToolManager manager)
    {
        
    }
}
