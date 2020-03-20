using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatBehaviour : MonoBehaviour
{
    private Transform _transform;

    private void Awake()
    {
        _transform = GetComponent<Transform>();
    }

    private void Update()
    {
        transform.position += new Vector3(-0.1f, 0, 0);
    }

    private void GetHit()
    {
        //trigger animation hurt
        //
    }
}
