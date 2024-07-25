using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigurationManger : MonoBehaviour
{
    public int gameLengthInSeconds = 60;
    public int scoreShoot = 1;
    public int scoreMine = 10;
    public int scoreCollect = 20;
    public int scoreReturn = 100;
    public int maxSpeed = 10;
    public int maxRotation = 10;
    public int maxRockets = 5;
    public int maxBeam = 5;
    

    
    
    private static ConfigurationManger _instance;
    
    public static ConfigurationManger Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<ConfigurationManger>();
            }

            return _instance;
        }
    }
    
    void Awake()
    {
        _instance = this;
    }

}
