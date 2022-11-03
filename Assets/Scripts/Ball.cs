using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public GameManager gameManager;
    private bool playStrike = true;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "BottomFloor")
        {
            gameManager.RemoveFallenPinsAndResetBall();
        }
        if (collision.gameObject.CompareTag("Pin"))
        {
            if (playStrike)
            {
                SoundManager.Instance.PlayStrike();
                playStrike = false;
            }
        }
    }
}
