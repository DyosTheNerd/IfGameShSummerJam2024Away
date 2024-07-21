using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointSystem : MonoBehaviour
{
    Dictionary<string, int> scoreValues = new Dictionary<string, int>(){{"shoot", 1}, {"mine", 10}, {"collect", 20}, {"return", 100}};
    
    
    public static PointSystem Instance;

    public void Awake(){PointSystem.Instance = this;}

    public int TotalCubes;

    public int scoreP1 = 0;
    
    public int scoreP2 = 0;
    
    
    public void AddCube(){TotalCubes++;}
    
    public void AddScore(int player, string scoreType){
        
        int value = scoreValues[scoreType];
        
        if(player == 1){
            scoreP1+=value;
        }else{
            scoreP2+=value;
        }
    }
    

}
