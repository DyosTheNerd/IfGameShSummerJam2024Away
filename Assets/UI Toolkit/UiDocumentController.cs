using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UiDocumentController : MonoBehaviour
{
    private Label score1;
    private Label score2;
    private Label rockets1;
    private Label rockets2;
    private Label beam1;
    private Label beam2;
    private VisualElement portrait1;
    private VisualElement portrait2;
    private Label timer;
    
    
    private VisualElement root;
    // Start is called before the first frame update
    void OnEnable()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        score1 = root.Q<Label>("Score1");
        score2 = root.Q<Label>("Score2");
        rockets1 = root.Q<Label>("Rockets1");
        rockets2 = root.Q<Label>("Rockets2");
        beam1 = root.Q<Label>("Beam1");
        beam2 = root.Q<Label>("Beam2");
        portrait1 = root.Q<VisualElement>("Portrait1");
        portrait2 = root.Q<VisualElement>("Portrait2");
        timer = root.Q<Label>("Timer");
        
        
        score1.text = "0";
        score2.text = "1";
        rockets1.text = "2";
        rockets2.text = "3";
        beam1.text = "4";
        beam2.text = "5";
        timer.text = "8";
        portrait1.Blur();
        portrait2.Blur();
        
        // subscribe to GameTimer onTimerTick event with a lambda function
        GameTimerSystem.instance.OnTimerTick += (remainingSeconds) =>
        {
            timer.text = remainingSeconds / 60 + ":" + remainingSeconds % 60;
        };
        
    }


}
