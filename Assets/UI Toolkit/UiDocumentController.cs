using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UiDocumentController : MonoBehaviour
{
    private Label score1;
    private Label score2;
    private ProgressBar rockets1;
    private ProgressBar rockets2;
    private ProgressBar beam1;
    private ProgressBar beam2;
    private VisualElement portrait1;
    private VisualElement portrait2;
    private Label timer;
    
    
    private VisualElement root;
    // Start is called before the first frame update
    void Start()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        score1 = root.Q<Label>("Score1");
        score2 = root.Q<Label>("Score2");
        rockets1 = root.Q<ProgressBar>("Rockets1");
        rockets2 = root.Q<ProgressBar>("Rockets2");
        beam1 = root.Q<ProgressBar>("Beam1");
        beam2 = root.Q<ProgressBar>("Beam2");
        portrait1 = root.Q<VisualElement>("Portrait1");
        portrait2 = root.Q<VisualElement>("Portrait2");
        timer = root.Q<Label>("Timer");
        
        
        score1.text = "0";
        score2.text = "1";
        rockets1.value = 20;
        rockets2.value = 30;
        beam1.value = 40;
        beam2.value = 50;
        timer.text = "8";
        portrait1.Blur();
        portrait2.Blur();
        
        // subscribe to GameTimer onTimerTick event with a lambda function
        GameTimerSystem.instance.OnTimerTick += (remainingSeconds) =>
        {
            timer.text =  toTwoDigitsString(remainingSeconds / 60) + ":" + toTwoDigitsString(remainingSeconds % 60);
        };
        
        PointSystem.Instance.OnScoreChangeHandler += (player, score) =>
        {
            if(player == 1){
                score1.text = score.ToString();
            }else{
                score2.text = score.ToString();
            }
        };
        
    }

    
    private string toTwoDigitsString(int number)
    {
        return number < 10 ? "0" + number : number.ToString();
    }
    

}
