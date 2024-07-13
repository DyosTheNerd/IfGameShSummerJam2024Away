using System.Collections;
using System.Collections.Generic;
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
        currentProjectile?.GetComponent<ToolEffect>()?.Deactivate(manager);
        currentProjectile = null;
    }


}
