using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;


public class ShipTool : MonoBehaviour
{
    public int maxAmmo = 100;

    public int constAmmoLoss = 1;
    public int activationCost = 10;
    
    public GameObject ProjectilePrefab;

    private GameObject currentProjectile;

    private ToolEffect effect;
    
    public void ActivateTool(ShipToolManager manager)
    {
        currentProjectile = Instantiate(ProjectilePrefab);
        effect = currentProjectile.GetComponentInChildren<ToolEffect>();
        effect.SetToolManager(manager);
        effect.Activate(manager);
    }

    public void DeactivateTool(ShipToolManager manager)
    {
        if(currentProjectile == null) return;
        
        if (effect != null)
            effect.Deactivate(manager);

        currentProjectile = null;
        effect = null;
    }
}