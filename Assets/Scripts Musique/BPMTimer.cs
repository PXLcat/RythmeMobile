using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class BPMTimer : MonoBehaviour
{
    [SerializeField]
    private double m_interval = 300;
    public bool m_enabled;
    public int m_currentTick;

    private Stopwatch watch;

    public void ChangeInterval(double interval)
    {
        m_interval = interval;
    }

    public void Start()
    {
        watch = Stopwatch.StartNew();
    }

    private void Update()
    {
        if (watch.ElapsedMilliseconds > m_interval + m_currentTick * m_interval)
        {
            m_currentTick++; //TODO utiliser un modulo au cas au le dt était super gros
            UnityEngine.Debug.Log("tick current = "+m_currentTick);
            
        }
    }
}