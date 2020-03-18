using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class BPMTimer : MonoBehaviour
{
    [SerializeField]
    private double m_interval = 300;
    public SOBoolean m_enabled;
    public int m_currentTick;

    private Stopwatch watch;

    public void ChangeInterval(double interval)
    {
        m_interval = interval;
    }

    public void StartTimer()
    {
        watch = Stopwatch.StartNew();
        m_enabled.value = true;
    }
    public void PauseTimer()
    {
        watch.Stop();
        m_enabled.value = false;
    }
    public void ResumeTimer()
    {
        watch.Start();
        m_enabled.value = true;
    }
    public void StopTimer()
    {
        watch.Reset();
        m_enabled.value = false;
        m_currentTick = 0;
    }

    private void Awake()
    {
        m_enabled.value = false;
    }

    private void Update()
    {
        if (m_enabled.value && watch.ElapsedMilliseconds > m_interval + m_currentTick * m_interval)
        {
            m_currentTick++; //TODO utiliser un modulo au cas au le dt était super gros
           
        }
    }
}