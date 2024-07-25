using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "ShipTool", menuName = "ScriptableObjects/ShipTools/ShipTool", order = 0)]
public class ShipTool : ScriptableObject
{
    public int maxAmmo = 100;

    public int constAmmoLoss = 1;
    public int activationCost = 10;
    
    public GameObject ProjectilePrefab;

    GameObject currentProjectile;

    public void ActivateTool(ShipToolManager manager)
    {

        
        currentProjectile = Instantiate(ProjectilePrefab);
        ToolEffect effect = currentProjectile.GetComponentInChildren<ToolEffect>();
        effect.SetToolManager(manager);
        effect.Activate(manager);
    }

    public void DeactivateTool(ShipToolManager manager)
    {
        
        
        if (currentProjectile.IsDestroyed() ) return;

        var tool = currentProjectile.GetComponentInChildren<ToolEffect>();

        if (tool != null)
            tool.Deactivate(manager);

        currentProjectile = null;
    }
}