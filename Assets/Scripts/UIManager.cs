using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    
    [Header("Paneles")]
    public GameObject menuPanel;
    public GameObject inGamePanel;
    public GameObject gameOverPanel;
    public GameObject creditsPanel;
    public GameObject howToPlayPanel;

    [Header("Variables de juego")]
    public Image vignete;
    public Sprite[] currentKeyMode;
    public Image keyboardInstructions;

    [Header("Links")]
    public string linkIsa = "https://isabel-arrans.itch.io/";
    public string linkCherry = "https://cherryneira.carrd.co";
    public string linkAlito = "https://linktr.ee/SrAlistoteles";

    [Header("Temporizador")]
    public TextMeshProUGUI timePlayedGOtxt;
    public TextMeshProUGUI timePlayedIGtxt;
    public float timePassed;

    private void Awake()
    {
        Instance = this;
        menuPanel.SetActive(true);
        MusicManager.instance.PlayBackgroundMusic(MusicManager.instance.musicMenu);
        Time.timeScale = 0f;
    }

    private void Update()
    {
        Temporizador();
    }

    public void ChangeState(int newState)
    {
        switch (newState)
        {
            case 0:
                //Activa los controles de la interfaz
                keyboardInstructions.sprite = currentKeyMode[0];
                break;
            case 1:
                keyboardInstructions.sprite = currentKeyMode[1];
                break;
            case 2:
                keyboardInstructions.sprite = currentKeyMode[2];
                break;
            case 3:
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
        Application.OpenURL(linkCherry);
    }
    public void OpenURLIsa()
    {
        Application.OpenURL(linkIsa);
    }
    public void OpenURLAlito()
    {
        Application.OpenURL(linkAlito);
    }
    #endregion

    #region("Game states")
    public void StartGame()
    {
        ActivateInGamePanel(true);
        Time.timeScale = 1f;
        MusicManager.instance.PlayBackgroundMusic(MusicManager.instance.musicInGame);
    }
    public void GameOver()
    {
        Debug.Log("GameOver");
        StartCoroutine(PanelesGameOver());
    }
    public void Retry()
    {
        SceneManager.LoadScene("Gameplay");
    }
    IEnumerator PanelesGameOver()
    {
        yield return new WaitForSeconds(1f);
        gameOverPanel.SetActive(true);
        inGamePanel.SetActive(false);
    }

    #endregion

    public void Temporizador()
    {
        if (PlayerControls.Instance.isPlaying == true)
        {
            timePassed += Time.deltaTime;
            int min = Math.Max(Mathf.FloorToInt(timePassed / 60), 0);
            int sec = Math.Max(Mathf.FloorToInt(timePassed % 60), 0);

            if (sec >= 10)
            {
                timePlayedGOtxt.text = min + ":" + sec;
                timePlayedIGtxt.text = min + ":" + sec;
            }
            else
            {
                timePlayedGOtxt.text = min + ":" + 0 + sec;
                timePlayedIGtxt.text = min + ":" + 0 + sec;
            }
        }
    }
    
}
