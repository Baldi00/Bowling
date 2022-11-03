using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public GameManager gameManager;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "BottomFloor")
        {
            gameManager.RemoveFallenPinsAndResetBall();
        }
    }
}
