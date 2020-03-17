using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private bool _previouslyTouching;
    private bool _currentlyTouching;

    private bool _touchDown; //le premier appui

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.touchCount > 0)
        {
            _currentlyTouching = true;
        }
        else
        {
            _currentlyTouching = false;
        }
        if (!_previouslyTouching && _currentlyTouching)
        {
            _touchDown = true;
            Debug.Log("touch unique");
        }
        else
        {
            _touchDown = false;
        }

        _previouslyTouching = _currentlyTouching;
    }
}
