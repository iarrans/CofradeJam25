using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollissions : MonoBehaviour
{
    public int lives = 3;

    private void Start()
    {
        UIManager.Instance.lives.text = "Lives: " + lives;
    }


    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "enemy" && PlayerControls.Instance.isPlaying)
        {
            lives--;
            if (lives == 0)
            {
                PlayerControls.Instance.isPlaying = false;
                PlayerControls.Instance.anim.SetBool("isDead", true);
                UIManager.Instance.GameOver();
                UIManager.Instance.lives.text = "Game Over";
            }
            else
            {
                UIManager.Instance.lives.text = "Lives: " + lives;
            }         
        }
    }
}
