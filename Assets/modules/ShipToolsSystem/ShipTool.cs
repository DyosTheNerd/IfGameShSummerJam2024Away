using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "ShipTool", menuName = "ScriptableObjects/ShipTools/ShipTool", order = 0)]
public class ShipTool : ScriptableObject
{
    public int ammo = 10;
    public int maxAmmo = 100;

    public GameObject ProjectilePrefab;

    GameObject currentProjectile;

    public void ActivateTool(ShipToolManager manager)
    {
        if (ammo > 0)
        {
            ammo--;
            
        }
        else
        {
            empty = true;
            currentProjectile = null;
            return;
        }
        
        currentProjectile = Instantiate(ProjectilePrefab);
        ToolEffect effect = currentProjectile.GetComponentInChildren<ToolEffect>();
        effect.SetToolManager(manager);
        effect.Activate(manager);
    }

    bool empty = false;
    
    public void DeactivateTool(ShipToolManager manager)
    {
        
        
        if (currentProjectile.IsDestroyed() || empty ) return;

        var tool = currentProjectile.GetComponentInChildren<ToolEffect>();

        if (tool != null)
            tool.Deactivate(manager);

        currentProjectile = null;
    }
}