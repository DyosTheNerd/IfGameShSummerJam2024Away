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
        score2.text = "0";

        timer.text = ""+GameTimerSystem.instance.GetStartingTime();
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
        StartCoroutine(SubscribeToShipToolManagers());
    }
    
    IEnumerator SubscribeToShipToolManagers()
    {
        yield return new WaitForSeconds(0.1f);
        ShipToolManager[] toolsManagers =  FindObjectsByType<ShipToolManager>(FindObjectsSortMode.None);
        foreach (ShipToolManager toolsManager in toolsManagers)
        {
            if(toolsManager.playerNumber == 1){
                toolsManager.OnAmmoUpdate += (ammoReserves, playerId) =>
                {
                    rockets1.value = ammoReserves[0];
                    beam1.value = ammoReserves[1];
                };
                rockets1.value = toolsManager.ammoReserves[0];
                beam1.value = toolsManager.ammoReserves[1];
                beam1.highValue = toolsManager.tools[1].maxAmmo;
                rockets1.highValue = toolsManager.tools[0].maxAmmo;
            }else{
                toolsManager.OnAmmoUpdate += (ammoReserves, playerId) =>
                {
                    rockets2.value = ammoReserves[0];
                    beam2.value = ammoReserves[1];
                };
                rockets2.value = toolsManager.ammoReserves[0];
                beam2.value = toolsManager.ammoReserves[1];
                beam2.highValue = toolsManager.tools[1].maxAmmo;
                rockets2.highValue = toolsManager.tools[0].maxAmmo;
            }
        }
    }

    
    private string toTwoDigitsString(int number)
    {
        return number < 10 ? "0" + number : number.ToString();
    }
    

}
