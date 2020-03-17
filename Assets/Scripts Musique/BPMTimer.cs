using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class BPMTimer : MonoBehaviour
{
    [SerializeField]
    private double m_interval = 300;
    [SerializeField]
    private bool m_enabled;
    public int m_currentTick;

    private Stopwatch watch;

    public void ChangeInterval(double interval)
    {
        m_interval = interval;
    }

    public void StartTimer()
    {
        watch = Stopwatch.StartNew();
        m_enabled = true;
    }
    public void PauseTimer()
    {
        watch.Stop();
        m_enabled = false;
    }
    public void ResumeTimer()
    {
        watch.Start();
        m_enabled = true;
    }
    public void StopTimer()
    {
        watch.Reset();
        m_enabled = false;
        m_currentTick = 0;
    }


    private void Update()
    {
        if (m_enabled && watch.ElapsedMilliseconds > m_interval + m_currentTick * m_interval)
        {
            m_currentTick++; //TODO utiliser un modulo au cas au le dt était super gros
           
        }
    }
}