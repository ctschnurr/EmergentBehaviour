using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;

public class ballManager : MonoBehaviour
{
    public enum State
    {
        idle,
        fixedAttraction,
        variableAttraction,
        randomAttraction
    }

    public State state = State.fixedAttraction;

    public GameObject ball;
    public GameObject[] ballArray = new GameObject[0];

    public static float variableModifier;
    public static float randomModifier;

    TextMeshProUGUI description;
    
    Slider variableSlider;
    TextMeshProUGUI variableSliderText;

    public bool on = false;

    void Start()
    {
        description = GameObject.Find("Description").GetComponent<TextMeshProUGUI>();
        variableSlider = GameObject.Find("VariableModifier").GetComponent<Slider>();
        variableSliderText = GameObject.Find("VariableSliderText").GetComponent<TextMeshProUGUI>();

        ResetBalls();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDescription();
        variableModifier = variableSlider.value;
        variableSliderText.text = "Distance Modifier: " + variableModifier;
    }

    void UpdateDescription() //  our GUI displays these descriptions based on the mode of the simulation
    {
        switch(state)
        {
            case State.idle:
                break;
            case State.fixedAttraction:
                description.text = "Fixed Attraction:\n\nObjects attract if further than " + variableSlider.value + " units apart, and repel if within " + variableSlider.value + " units.\n\nAt lower distance settings create a stable ring shape.\n\nAt higher distance settings, creates an interesting diamond behavior.";
                break;
            case State.variableAttraction:
                description.text = "Variable Attraction:\n\nObjects attract and repel with force based on distance between objects.\n\nAttraction is calulated with force * distance, and distance is adjusted by modifier: distance -= " + variableSlider.value;
                break;
            case State.randomAttraction:
                description.text = "Random Attraction:\n\nObjects attract and repel with strength based on distance with modifier " + variableSlider.value + ", plus each ball has an additional modifier between 1 and 3.\n\nAt higher distance settings, has an interesting magnet-like spinning behavior.";
                break;
        }
    }

    public void ResetBalls() // we use this method to clear all the balls from the simulation and instantiate new ones in a nice tidy grid
    {
        on = false;

        description.text = " ";

        foreach (GameObject theBall in ballArray)
        {
            GameObject.Destroy(theBall);
        }

        Array.Clear(ballArray, 0, ballArray.Length);
        Array.Resize(ref ballArray, 0);

        for (int i = -20; i <= 20; i += 4)
        {
            for (int j = 20; j >= -20; j -= 4)
            {
                System.Array.Resize(ref ballArray, ballArray.Length + 1);
                GameObject newBall = Instantiate(ball, new Vector3(i, 0, j), Quaternion.identity);
                ballBehavior newBallBehavior = newBall.GetComponent<ballBehavior>();
                newBallBehavior.randomModifier = UnityEngine.Random.Range(1, 4);
                ballArray[ballArray.Length - 1] = newBall;
            }
        }
    }

    public void Go() // this controlls whether or not the balls are active after a reset
    {
        on = true;
    }

    public void SetFixedAttraction()
    {
        state = State.fixedAttraction;
    }

    public void SetVariableAttraction()
    {
        state = State.variableAttraction;
    }

    public void SetRandomAttraction()
    {
        state = State.randomAttraction;
    }

    public void ExitApplication()
    {
        Application.Quit();
    }
}
