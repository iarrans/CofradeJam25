using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    public static UIManager Instance;
    
    public TextMeshProUGUI rounds;
    public TextMeshProUGUI currentState;
    public TextMeshProUGUI lives;

    private void Awake()
    {
        Instance = this;
        rounds.text = "Nivel de ansiedad: 0 ";
        currentState.text = "Control: normal";

    }


    public void ChangeState(int newState)
    {

        rounds.text = "Nivel de ansiedad " + PlayerControls.Instance.currentRound.ToString();

        switch (newState)
        {
            case 0:
                currentState.text = "Control: normal";
                break;
            case 1:
                currentState.text = "Control: izq=alante. drcha=atrás";
                break;
            case 2:
                currentState.text = "Control: invertido";
                break;
            case 3:
                currentState.text = "Control: drcha=adelante, izq=atrás";
                break;
            default:
                // code block
                break;
        }
    }

    public void GameOver()
    {
        Debug.Log("GameOver");
    }
}
