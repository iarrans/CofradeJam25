using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollissions : MonoBehaviour
{

    public int lives = 3;
    public static PlayerCollissions Instance;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {

    }


    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "enemy" && PlayerControls.Instance.isPlaying)
        {
            lives--;
            UIManager.Instance.SetVignete();
            if (lives == 0)
            {
                PlayerControls.Instance.isPlaying = false;
                PlayerControls.Instance.anim.SetBool("isDead", true);
                PlayerControls.Instance.speed = 0;
                MusicManager.instance.PlaySFX(MusicManager.instance.deathTerry);
                UIManager.Instance.GameOver();

            }
            else
            {
                MusicManager.instance.PlaySFX(MusicManager.instance.damageTerry);
            }   
        }
    }
}
