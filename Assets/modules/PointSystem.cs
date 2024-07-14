using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointSystem : MonoBehaviour
{

    public static PointSystem Instance;

    public void Start(){PointSystem.Instance = this;}

    public int TotalCubes;
    
    
    public void AddCube(){TotalCubes++;}

}
