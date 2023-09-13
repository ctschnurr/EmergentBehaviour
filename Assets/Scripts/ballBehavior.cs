using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class ballBehavior : MonoBehaviour
{
    // Start is called before the first frame update

    GameObject ball;
    GameObject[] ballArray;
    public GameObject antBuddy = null;
    public ballManager bMan;
    Rigidbody rb;
    List<GameObject> fiveClosest = new List<GameObject>();
    List<GameObject> ballList = new List<GameObject>();

    public int ballNumber;

    public bool isLeader = false;

    public ballManager.State state;

    public float randoInt;
    void Start()
    {
        bMan = GameObject.Find("ballManager").GetComponent<ballManager>();
        ball = this.gameObject;
        ballArray = bMan.ballArray;
        rb = this.gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        state = bMan.state;

        switch (state)
        {
            case ballManager.State.idle:

                break;

            case ballManager.State.carbonated:

                foreach (GameObject theBall in ballArray)
                {
                    Rigidbody attractMe = theBall.GetComponent<Rigidbody>();

                    Vector3 direction = rb.position - attractMe.position;

                    float distance = (ball.transform.position - attractMe.transform.position).magnitude;

                    Vector3 force = direction.normalized;

                    if (distance > 5f)
                    {
                        attractMe.AddForce(force);
                        rb.AddForce(-force);
                    }
                    else
                    {
                        attractMe.AddForce(-force * 50);
                        rb.AddForce(force * 50);
                    }
                }
                break;

            case ballManager.State.ants:

                if (isLeader)
                {
                    rb.AddForce(randoInt, 0.0f, randoInt);
                }
                else
                {
                    Rigidbody followMe = antBuddy.GetComponent<Rigidbody>();

                    Vector3 direction = rb.position - followMe.position;

                    Vector3 force = direction.normalized;

                    rb.AddForce(-force);
                }

                break;
        }
    }
}
