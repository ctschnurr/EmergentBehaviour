using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ballBehavior : MonoBehaviour
{
    // Start is called before the first frame update

    GameObject ball;
    GameObject[] ballArray;
    public ballManager bMan;
    Rigidbody rb;
    List<GameObject> fiveClosest = new List<GameObject>();
    List<GameObject> ballList = new List<GameObject>();

    public float timer = 5;
    void Start()
    {
        bMan = GameObject.Find("ballManager").GetComponent<ballManager>();
        ball = this.gameObject;
        ballArray = bMan.ballArray;
        rb = this.gameObject.GetComponent<Rigidbody>();

        timer += Time.deltaTime;
        //GetFiveClosest();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < 0)
        {
            GetFiveClosest();
            timer = 5;
        }
        else timer -= Time.deltaTime;

        if (ballArray.Length > 0)
        {
            foreach(GameObject theBall in ballArray)
            {
                Rigidbody attractMe = theBall.GetComponent<Rigidbody>();
            
                Vector3 direction = rb.position - attractMe.position;
            
                float distance = (ball.transform.position - theBall.transform.position).magnitude;
            
                Vector3 force = direction.normalized;
            
                if (distance > 5f)
                {
                    attractMe.AddForce(force);
                    rb.AddForce(-force);
                }
                else
                {
                    attractMe.AddForce(-force*50);
                    rb.AddForce(force*50);
                }
            }
        }
    }

    public void GetFiveClosest()
    {
        if (ballList.Count != 0) ballList = new List<GameObject>();

        if (fiveClosest.Count != 0) fiveClosest = new List<GameObject>();

        foreach (GameObject theBall in ballArray)
        {
            ballList.Add(theBall);
        }

        int limit = 6;
        if (ballArray.Length < 6) limit = ballArray.Length;

        while (fiveClosest.Count < limit)
        {
            float closestDist = Mathf.Infinity;
            GameObject closestBall = null;

            foreach (GameObject theBall in ballList)
            {
                float distance = (ball.transform.position - theBall.transform.position).magnitude;
                if (distance < closestDist)
                {
                    closestDist = distance;
                    closestBall = theBall;
                }                    
            }

            fiveClosest.Add(closestBall);
            ballList.Remove(closestBall);
        }
    }
}
