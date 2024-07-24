using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointSystem : MonoBehaviour
{
    private Dictionary<string, int> scoreValues;
    
    
    public static PointSystem Instance;

    public void Awake(){PointSystem.Instance = this;}
    
    // event to be triggered when points change
    public delegate void OnScoreChange(int player, int score);
    public event OnScoreChange OnScoreChangeHandler;
    

    public void Start()
    {
        ConfigurationManger config = ConfigurationManger.Instance;
        scoreValues = new Dictionary<string, int>(){{"shoot", config.scoreShoot}, {"mine", config.scoreMine}, {"collect", config.scoreCollect}, {"return", config.scoreReturn}};
    }
    
    public int TotalCubes;

    public int scoreP1 = 0;
    
    public int scoreP2 = 0;
    
    
    public void AddCube(){TotalCubes++;}
    
    public void AddScore(int player, string scoreType){
        
        int value = scoreValues[scoreType];
       
        
        if(player == 1){
            scoreP1+=value;
            OnScoreChangeHandler?.Invoke(1, scoreP1);
        }else{
            scoreP2+=value;
            OnScoreChangeHandler?.Invoke(2, scoreP2);
        }
    }
    

}
