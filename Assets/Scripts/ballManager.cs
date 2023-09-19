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
    public static float fixedModifier;

    TextMeshProUGUI description;
    
    Slider variableSlider;
    TextMeshProUGUI variableSliderText;

    Slider speedSlider;
    TextMeshProUGUI speedSliderText;
    public enum State
    {
        idle,
        fixedAttraction,
        variableAttraction
    }

    public State state = State.idle;

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
        variableModifier = variableSlider.value;
        variableSliderText.text = "Distance Modifier: " + variableModifier;

        if (state == State.fixedAttraction)
        {
            description.text = "Fixed Attraction: Objects attract if further than " + variableSlider.value + " units apart, and repel if within " + variableSlider.value + " units.";
        }

        if (state == State.variableAttraction)
        {
            description.text = "Variable Attraction: Objects attract and repel with strength based on distance between objects. The change threshold between attract/repel is distance -= " + variableSlider.value;
        }
    }

    public void ResetBalls()
    {
        description.text = " ";

        // ballsNumber = 0;
        foreach (GameObject theBall in ballArray)
        {
            GameObject.Destroy(theBall);
        }

        state = State.idle;
        Array.Clear(ballArray, 0, ballArray.Length);
        Array.Resize(ref ballArray, 0);

        for (int i = -20; i <= 20; i += 4)
        {
            for (int j = 20; j >= -20; j -= 4)
            {
                System.Array.Resize(ref ballArray, ballArray.Length + 1);
                //ballArray[ballArray.Length - 1] = (Instantiate(ball, new Vector3(i, 0, j), Quaternion.identity));
                GameObject newBall = Instantiate(ball, new Vector3(i, 0, j), Quaternion.identity);
                ballArray[ballArray.Length - 1] = newBall;
            }
        }
    }

    public void SetFixedAttraction()
    {
        state = State.fixedAttraction;
    }

    public void SetVariableAttraction()
    {
        state = State.variableAttraction;
    }

}
