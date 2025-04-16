using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public static UIManager Instance;
    
    public TextMeshProUGUI rounds;
    public TextMeshProUGUI currentState;
    public TextMeshProUGUI lives;

    [Header("Paneles")]
    public GameObject menuPanel;
    public GameObject inGamePanel;
    public GameObject gameOverPanel;
    public GameObject creditsPanel;
    public GameObject howToPlayPanel;

    [Header("Variables de juego")]
    public TextMeshProUGUI timePlayed;
    public Image vignete;
    public Sprite[] currentKeyMode;
    public Image keyboardInstructions;

    [Header("Links")]
    public string linksIsa;
    public string linksCherry = "https://cherryneira.carrd.co";


    private void Awake()
    {
        Instance = this;
        rounds.text = "Nivel de ansiedad: 0 ";
        currentState.text = "Control: normal";

    }
    private void Update()
    {
        SetVignete();
    }

    public void ChangeState(int newState)
    {

        rounds.text = "Nivel de ansiedad " + PlayerControls.Instance.currentRound.ToString();

        switch (newState)
        {
            case 0:
                currentState.text = "Control: normal";
                //Activa los controles de la interfaz
                keyboardInstructions.sprite = currentKeyMode[0];
                break;
            case 1:
                currentState.text = "Control: izq=alante. drcha=atrás";
                keyboardInstructions.sprite = currentKeyMode[1];
                break;
            case 2:
                currentState.text = "Control: invertido";
                keyboardInstructions.sprite = currentKeyMode[2];
                break;
            case 3:
                currentState.text = "Control: drcha=adelante, izq=atrás";
                keyboardInstructions.sprite = currentKeyMode[3];
                break;
            default:
                // code block
                break;
        }
    }

    public void SetVignete()
    {
        Color color = vignete.color;
        if (PlayerCollissions.Instance.lives == 3)
        {
            color.a = 0f;
        }
        else if (PlayerCollissions.Instance.lives == 2)
        {
            color.a = 0.5f;
        }
        else if (PlayerCollissions.Instance.lives == 1)
        {
            color.a = 1f;
        }
        vignete.color = color;
    }

    #region("Panels")
    public void ActivateInGamePanel(bool state)
    {
        inGamePanel.SetActive(state);
        menuPanel.SetActive(!state);
        gameOverPanel.SetActive(!state);
    }
    public void ActivateCreditsPanel(bool state)
    {
        creditsPanel.SetActive(state);
    }
    public void ActivateHowToPlay(bool state)
    {
        howToPlayPanel.SetActive(state);
    }
    #endregion

    #region("Button URL")
    public void OpenURLCherry()
    {
        Application.OpenURL(linksCherry);
    }
    public void OpenURLIsa()
    {
        Application.OpenURL(linksIsa);
    }
    #endregion
    public void GameOver()
    {
        gameOverPanel.SetActive(true);
        inGamePanel.SetActive(false);
        Debug.Log("GameOver");
    }
}
