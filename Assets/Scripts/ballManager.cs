using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject ball;
    public GameObject[] ballArray = new GameObject[0];
    public Vector3 mousePos;
    public int ballsCount;

    void Start()
    {
        
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
}
