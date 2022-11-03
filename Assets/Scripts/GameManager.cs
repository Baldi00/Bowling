using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject arrow;
    public GameObject ball;

    public float phase1Speed;
    public float phase2Speed;
    public float phase3Speed;

    private int phase;
    private int ballDirection;
    private int arrowAngleDirection;
    private int arrowForceDirection;

    private Rigidbody ballRigidBody;

    private void Awake()
    {
        ballRigidBody = ball.GetComponent<Rigidbody>();
    }

    private void Start()
    {
        phase = 0;
        ballDirection = -1;
        arrowAngleDirection = -1;
        arrowForceDirection = -1;
        arrow.SetActive(false);
        ballRigidBody.isKinematic = true;
        ballRigidBody.detectCollisions = false;
    }

    void Update()
    {
        switch (phase)
        {
            case 0:
                if (ballDirection == -1 && ball.transform.position.x > -2)
                    ballRigidBody.MovePosition(ball.transform.position + phase1Speed * Time.deltaTime * Vector3.left);
                else if (ballDirection == 1 && ball.transform.position.x < 1.15)
                    ballRigidBody.MovePosition(ball.transform.position + phase1Speed * Time.deltaTime * Vector3.right);
                else
                    ballDirection = -ballDirection;
                break;

            case 1:
                if (arrowAngleDirection == -1 && arrow.transform.eulerAngles.y > 80)
                    arrow.transform.Rotate(phase2Speed * Time.deltaTime * Vector3.forward);
                else if (arrowAngleDirection == 1 && arrow.transform.eulerAngles.y < 100)
                    arrow.transform.Rotate(phase2Speed * Time.deltaTime * -Vector3.forward);
                else
                    arrowAngleDirection = -arrowAngleDirection;
                break;

            case 2:
                if (arrowForceDirection == -1 && arrow.transform.localScale.x > 0.5)
                    arrow.transform.localScale -= new Vector3(phase3Speed * Time.deltaTime, 0f, 0f);
                else if (arrowForceDirection == 1 && arrow.transform.localScale.x < 1.5)
                    arrow.transform.localScale += new Vector3(phase3Speed * Time.deltaTime, 0f, 0f);
                else
                    arrowForceDirection = -arrowForceDirection;
                break;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (phase == 0)
            {
                phase = 1;
                ballRigidBody.isKinematic = false;
                ballRigidBody.detectCollisions = true;
                arrow.SetActive(true);
            }
            else if (phase == 1)
            {
                phase = 2;
            }
            else if (phase == 2)
            {
                phase = 3;
                arrow.SetActive(false);
                LaunchBall();
            }
        }
    }

    private void LaunchBall()
    {
        float angle = arrow.transform.eulerAngles.y;
        float force = 3000 * arrow.transform.localScale.x * 2f;
        Vector3 direction = Quaternion.Euler(0, angle, 0) * Vector3.left;
        ballRigidBody.AddForce(direction * force);
    }
}
