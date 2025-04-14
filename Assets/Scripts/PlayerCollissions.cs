using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollissions : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "enemy")
        {
            PlayerControls.Instance.isPlaying = false;
            UIManager.Instance.GameOver();
        }
    }
}
