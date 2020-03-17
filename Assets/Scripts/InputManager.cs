using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{

    public float m_swipeDistanceTreshold = 50;
    private bool _touchDown; //le premier appui

    private Vector3 _firstPos;   //First touch position
    private Vector3 _lastPos;   //Last touch position

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.touchCount>0)
        {
            Touch input = Input.GetTouch(0);

            switch (input.phase)
            {
                case TouchPhase.Began:
                    _touchDown = true;
                    Debug.Log("touch unique");
                    _firstPos = input.position;
                    _lastPos = input.position;
                    break;
                case TouchPhase.Moved:
                    _lastPos = input.position;
                    break;
                case TouchPhase.Stationary:
                    break;
                case TouchPhase.Ended:
                    _lastPos = input.position;
                    break;
                case TouchPhase.Canceled:
                    break;
                default:
                    break;
            }

            AnalyzeGesture(_firstPos, _lastPos);
        }


    }

    private void AnalyzeGesture(Vector2 start, Vector2 end)
    {
        // Distance
        if (Vector2.Distance(start, end) > m_swipeDistanceTreshold)
        {
            if ((start.x < end.x))  
            {   //Right swipe
                Debug.Log("Right Swipe");
            }
            else
            {   //Left swipe
                Debug.Log("Left Swipe");
            }

        }
    }

}
