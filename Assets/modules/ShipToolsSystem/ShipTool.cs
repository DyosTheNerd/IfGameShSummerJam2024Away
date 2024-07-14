using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "ShipTool", menuName = "ScriptableObjects/ShipTools/ShipTool", order = 0)]
public class ShipTool : ScriptableObject
{


    public GameObject ProjectilePrefab;

    GameObject currentProjectile;

    public void ActivateTool(ShipToolManager manager)
    {
        currentProjectile = Instantiate(ProjectilePrefab);
        currentProjectile.GetComponent<ToolEffect>().Activate(manager);
    }

    public void DeactivateTool(ShipToolManager manager)
    {
        if(currentProjectile.IsDestroyed()) return;
        
        currentProjectile?.GetComponent<ToolEffect>()?.Deactivate(manager);
        currentProjectile = null;
    }


}
