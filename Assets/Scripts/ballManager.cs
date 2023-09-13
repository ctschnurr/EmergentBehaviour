using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ballManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject ball;
    public GameObject[] ballArray = new GameObject[0];
    public Vector3 mousePos;
    public int ballsCount;
    public int ballsNumber;
    public enum State
    {
        idle,
        carbonated,
        ants
    }

    public State state = State.idle;

    void Start()
    {
        ResetBalls();
    }

    // Update is called once per frame
    void Update()
    {

        ballsCount = ballArray.Length;

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100))
            {
                System.Array.Resize(ref ballArray, ballArray.Length + 1);
                ballArray[ballArray.Length - 1] = (Instantiate(ball, hit.point, Quaternion.identity));
            }
        }

    }

    public void ResetBalls()
    {
        ballsNumber = 0;
        foreach (GameObject theBall in ballArray)
        {
            GameObject.Destroy(theBall);
        }

        state = State.idle;
        Array.Clear(ballArray, 0, ballArray.Length);
        Array.Resize(ref ballArray, 0);

        for (int i = -40; i <= 40; i += 4)
        {
            for (int j = 20; j >= -20; j -= 4)
            {
                System.Array.Resize(ref ballArray, ballArray.Length + 1);
                //ballArray[ballArray.Length - 1] = (Instantiate(ball, new Vector3(i, 0, j), Quaternion.identity));
                GameObject newBall = Instantiate(ball, new Vector3(i, 0, j), Quaternion.identity);
                newBall.GetComponent<ballBehavior>().ballNumber = ballsNumber;
                newBall.name = "Ball " + ballsNumber;
                ballArray[ballArray.Length - 1] = newBall;

                if (ballsNumber == 0) newBall.GetComponent<ballBehavior>().isLeader = true;
                else
                {
                    foreach (GameObject theBall in ballArray)
                    {
                        if (theBall.GetComponent<ballBehavior>().ballNumber == ballsNumber - 1) newBall.GetComponent<ballBehavior>().antBuddy = theBall;
                    }
                }

                ballsNumber++;
            }
        }
    }

    public void SetCarbonated()
    {
        state = State.carbonated;
    }

    public void SetAnts()
    {
        state = State.ants;
    }
}
