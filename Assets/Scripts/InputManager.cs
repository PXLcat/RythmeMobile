using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{

    public float m_swipeDistanceTreshold = 50;
    private bool _touchDown; //le premier appui

    private Vector3 _firstPos;   //First touch position
    private Vector3 _lastPos;   //Last touch position

    public SOTouchStatus m_touchStatus;

    #region Animations
    public Animator m_textAnimator;
    private int _greatHash;
    private int _missedHash;

    public Animator m_characterAnimator;
    private int _attackHash;

    public Animator m_fxAnimator;

    #endregion

    private void Awake()
    {
        _greatHash = Animator.StringToHash("GreatTrigger");
        _missedHash = Animator.StringToHash("MissedTrigger");

        _attackHash = Animator.StringToHash("Attack");
    }

    
    private void Update()
    {
        _touchDown = false;

        if (Input.touchCount>0)
        {
            Touch input = Input.GetTouch(0);

            switch (input.phase)
            {
                case TouchPhase.Began:
                    _touchDown = true;
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

        if (_touchDown)
        {
            m_characterAnimator.SetTrigger(_attackHash);

            if (m_touchStatus.m_status == BeatsManager.TouchStatus.GREAT)
            {
                Debug.Log("GREAT");
                m_textAnimator.SetTrigger(_greatHash);
                m_fxAnimator.SetTrigger(_attackHash);

            }
            else if (m_touchStatus.m_status == BeatsManager.TouchStatus.MISSED)
            {
                Debug.Log("missed");
                m_textAnimator.SetTrigger(_missedHash);

            }

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
