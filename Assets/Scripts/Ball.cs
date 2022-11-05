using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public GameManager gameManager;
    private bool playStrikeSound = true;

    public bool Launched = false;
    public bool IsBallArrested = false;
    public int ballArrestedFramesThreshold;
    private int ballArrestedFramesCount = 0;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Launched && rb.velocity.magnitude < 1f)
        {
            ballArrestedFramesCount++;
        }

        if(ballArrestedFramesCount >= ballArrestedFramesThreshold)
        {
            gameManager.RemoveFallenPinsAndResetBall();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "BottomFloor")
        {
            gameManager.RemoveFallenPinsAndResetBall();
        }

        if (collision.gameObject.CompareTag("Pin"))
        {
            if (playStrikeSound)
            {
                SoundManager.Instance.PlayStrike();
                playStrikeSound = false;
            }
        }
    }
}
