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

    public ballManager.State state;

    float distance;

    float variableModifier;
    float fixedModifier;

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
        variableModifier = ballManager.variableModifier;
        fixedModifier = ballManager.fixedModifier;

        state = bMan.state;

        switch (state)
        {
            case ballManager.State.idle:

                break;

            case ballManager.State.fixedAttraction:

                foreach (GameObject theBall in ballArray)
                {
                    if (theBall != this)
                    {
                        Rigidbody attractMe = theBall.GetComponent<Rigidbody>();

                        Vector3 direction = rb.position - attractMe.position;

                        float distance = (ball.transform.position - attractMe.transform.position).magnitude;

                        Vector3 force = direction.normalized;

                        if (distance > variableModifier)
                        {
                            attractMe.AddForce(force);
                            rb.AddForce(-force);
                        }
                        else
                        {
                            attractMe.AddForce(-force);
                            rb.AddForce(force);
                        }
                    }

                }
                break;

            case ballManager.State.variableAttraction:

                foreach (GameObject theBall in ballArray)
                {
                    if(theBall != this)
                    {
                        Rigidbody attractMe = theBall.GetComponent<Rigidbody>();

                        Vector3 direction = rb.position - attractMe.position;

                        distance = (ball.transform.position - attractMe.transform.position).magnitude;

                        Vector3 force = direction.normalized;

                        distance -= variableModifier;

                        attractMe.AddForce(force * distance);
                        rb.AddForce(-force * distance);
                    }
                }
                break;
        }
    }
}

// save this code:
//                 case ballManager.State.variableRepel:
// 
//                 if (isLeader)
//                 {
//                     rb.transform.Rotate(new Vector3(0, Random.Range(-90, 90), 0) );
// rb.AddForce(transform.forward * 0.5f);
//                 }
//                 else
// {
//     Rigidbody followMe = antBuddy.GetComponent<Rigidbody>();
// 
//     Vector3 direction = rb.position - followMe.position;
//     // 
//     Vector3 force = direction.normalized;
//     // 
//     // rb.AddForce(-force / 5);
// 
//     rb.transform.Rotate(force);
//     rb.AddForce(transform.forward * 0.5f);
// }
// 
// break;
