using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// Manages players over the entire game experience.

public class PlayerManager : MonoBehaviour
{
    private static PlayerManager _instance;
    public static PlayerManager Instance { 
        get{if(_instance == null){Debug.Log("Missing PlayerManager");} return _instance;}
        set{if(_instance != null){Debug.Log("Extra PlayerManager in scene.");} else { _instance = value;}}
    }

    public List<PlayerData> players;

    void Start(){
        Instance = this;
    }


    public List<PlayerData> playerList;

    public void BindShipPlayerControl(int playerID, GameObject playerShip)
    {
        var input = playerShip.GetComponentInChildren<PlayerInput>();
        var player = players[playerID];
    }


}
