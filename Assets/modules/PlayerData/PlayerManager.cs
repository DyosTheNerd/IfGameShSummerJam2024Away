using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


// Manages players over the entire game experience.

public class PlayerManager : MonoBehaviour
{
    private static PlayerManager _instance;
    public static PlayerManager Instance { 
        get{if(_instance == null){Debug.Log("Missing PlayerManager");} return _instance;}
        set{if(_instance != null){Debug.Log("Extra PlayerManager in scene.");} else { _instance = value;}}
    }

    public List<PlayerInput> players;

    public int PlayerCountWait = 2;

    public PlayerInputManager playerInputManager;
    bool playing = false;
    void Start(){
        Instance = this;
        DontDestroyOnLoad(gameObject);
        playerInputManager = GetComponent<PlayerInputManager>();

    }

    void OnEnable(){
        playerInputManager.onPlayerJoined += OnPlayerJoined;
        playerInputManager.onPlayerLeft += OnPlayerLet;
    }
    void OnDisable(){
        playerInputManager.onPlayerJoined -= OnPlayerJoined;
        playerInputManager.onPlayerLeft -= OnPlayerLet;

    }

    public void OnPlayerJoined(PlayerInput player){
        DontDestroyOnLoad(player.gameObject);
        players.Add(player);
    }

    public void OnPlayerLet(PlayerInput player){
        players.Remove(player);
    }

    IEnumerator AfterSceneLoad(){
        yield return null;
        yield return null;
        var factory = FindAnyObjectByType<ShipFactory>();
            foreach(var player in players){
                var ship = factory.BuildShip(player);
            }
    }

    void Update(){

        // if game is not being played and lobby is full. Start Game.
        if(!playing && players.Count == 2)
        {
            playerInputManager.DisableJoining();
            playing = true;
            SceneManager.LoadScene("GameScene");
            StartCoroutine(AfterSceneLoad());
        }
    }

}
