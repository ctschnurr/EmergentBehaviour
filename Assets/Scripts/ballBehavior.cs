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
    public float randomModifier = 0;

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

        state = bMan.state;

        if (bMan.on) // the ball manager controls whether or not the balls are active
        {
            switch (state)
            {
                case ballManager.State.idle:

                    break;

                case ballManager.State.fixedAttraction: // fixed attraction causes the balls to repel if within the modifier threshold and attract if outside of that threshold

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

                case ballManager.State.variableAttraction: // variable attraction uses the modifier threshold in a different manner for a different effect

                    foreach (GameObject theBall in ballArray)
                    {
                        if (theBall != this)
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

                case ballManager.State.randomAttraction:  // balls are assigned a 1, 2, or 3 additional modifier to add a random factor

                    foreach (GameObject theBall in ballArray)
                    {
                        if (theBall != this)
                        {
                            Rigidbody attractMe = theBall.GetComponent<Rigidbody>();

                            Vector3 direction = rb.position - attractMe.position;

                            distance = (ball.transform.position - attractMe.transform.position).magnitude;

                            Vector3 force = direction.normalized;

                            distance -= (variableModifier * randomModifier);

                            attractMe.AddForce(force * distance);
                            rb.AddForce(-force * distance);
                        }
                    }
                    break;
            }
        }
        
    }
}
