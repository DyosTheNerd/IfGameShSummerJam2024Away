using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class ShipFactory : MonoBehaviour
{

    public List<GameObject> ships;

    public int shipCounter = 0;

    public GameObject BuildShip(PlayerInput input){

        var ship = ships[shipCounter];
        shipCounter++;

        BindShipPlayerControl(input, ship);

        return ship;
    }

    public void BindShipPlayerControl(PlayerInput player, GameObject playerShip)
    {
        var movement = playerShip.GetComponent<ShipMovement>();
        var tool = playerShip.GetComponentInChildren<ShipToolManager>();


        movement.BindControls(player);
        tool.BindControls(player);
    }
}
