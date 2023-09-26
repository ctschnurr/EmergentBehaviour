using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;

public class ballManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject ball;
    public GameObject[] ballArray = new GameObject[0];

    public static float variableModifier;
    public static float randomModifier;

    TextMeshProUGUI description;
    
    Slider variableSlider;
    TextMeshProUGUI variableSliderText;

    Slider speedSlider;
    TextMeshProUGUI speedSliderText;
    public enum State
    {
        idle,
        fixedAttraction,
        variableAttraction,
        randomAttraction
    }

    public bool on = false;

    public State state = State.fixedAttraction;

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
        UpdateDisctiption();
        variableModifier = variableSlider.value;
        variableSliderText.text = "Distance Modifier: " + variableModifier;
    }

    void UpdateDisctiption()
    {
        switch(state)
        {
            case State.idle:
                break;
            case State.fixedAttraction:
                description.text = "\nFixed Attraction:\n\nObjects attract if further than " + variableSlider.value + " units apart, and repel if within " + variableSlider.value + " units.\n\nAt higher distance settings, creates an interesting diamond behavior.";
                break;
            case State.variableAttraction:
                description.text = "\nVariable Attraction:\n\nObjects attract and repel with force based on distance between objects.\n\nAttraction is calulated with force * distance, and distance is adjusted by modifier: distance -= " + variableSlider.value;
                break;
            case State.randomAttraction:
                description.text = "\nRandom Attraction:\n\nObjects attract and repel with strength based on distance with modifier " + variableSlider.value + ", plus each ball has an additional modifier between 1 and 3.\n\nAt higher distance settings, has an interesting magnet-like spinning behavior.";
                break;
        }
    }

    public void ResetBalls()
    {
        on = false;

        description.text = " ";

        // ballsNumber = 0;
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
                //ballArray[ballArray.Length - 1] = (Instantiate(ball, new Vector3(i, 0, j), Quaternion.identity));
                GameObject newBall = Instantiate(ball, new Vector3(i, 0, j), Quaternion.identity);
                ballBehavior newBallBehavior = newBall.GetComponent<ballBehavior>();
                newBallBehavior.randomModifier = UnityEngine.Random.Range(1, 4);
                ballArray[ballArray.Length - 1] = newBall;
            }
        }
    }

    public void Go()
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

}
