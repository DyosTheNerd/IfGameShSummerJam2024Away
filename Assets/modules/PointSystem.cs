using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointSystem : MonoBehaviour
{

    private static PointSystem _instance;
    public static PointSystem Instance { 
        get{if(_instance == null){Debug.Log("Missing PointSystem");} return _instance;}
        set{if(_instance != null){Debug.Log("Extra PointSystem in scene.");}}
    }

    public void Start(){Instance = this;}

    public int TotalCubes;
    
    
    public void AddCube(){TotalCubes++;}

}
