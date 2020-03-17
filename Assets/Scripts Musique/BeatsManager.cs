using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Timers;
using System;

public class BeatsManager : MonoBehaviour
{
    #region Propriétés de la chanson du json
    public TextAsset m_trackJson;
    private SongDTO _songDTO;

    [SerializeField]
    List<int> _musicBeats = new List<int>();
    [SerializeField]
    List<int> _rythmBeats = new List<int>();

    #endregion

    private MusicState _currentMusicState;
    public MusicState CurrentMusicState { get => _currentMusicState; set => _currentMusicState = value; }

    public int m_timeAhead = 4;

    public GameObject m_beatsParent;
    public Beat m_beatPrefab;

    private BPMTimer _bpmTimer;

    #region Propriétés relatives à la taille de la ligne
    public Camera m_camera;
    public Transform m_barreTempsTransform;
    private decimal _lineSize;
    #endregion

    private void Awake()
    {
        #region Récupération des données du json
        _songDTO = JsonUtility.FromJson<SongDTO>(m_trackJson.text);
        Debug.Log(_songDTO.Name);

        foreach (int i in _songDTO.MusicLine)
        {
            _musicBeats.Add(i);
        }
        foreach (int i in _songDTO.RythmLine)
        {
            _rythmBeats.Add(i);
        }
        #endregion
        #region Récupération de la taille de la ligne
        _lineSize = Screen.width - (decimal)m_camera.WorldToScreenPoint(m_barreTempsTransform.position).x;
        Debug.Log("linesize = " + _lineSize);
        #endregion


        _bpmTimer = GetComponent<BPMTimer>();
        double timer = (double)60000 / (double)_songDTO.BPM / (double)_songDTO.IntervalsByBPM;
        Debug.Log("timer = " + timer);
        _bpmTimer.ChangeInterval(timer);

    }



    public void StartTimer()
    {
        _bpmTimer.StartTimer();
    }

    public void PauseTimer()
    {
        _bpmTimer.PauseTimer();
    }

    public void ResumeTimer()
    {
        _bpmTimer.ResumeTimer();
    }

    public void StopTimer()
    {
        _bpmTimer.StopTimer();
    }


    void Update()
    {
        #region Initialisation des beats
        if (_musicBeats.Count > 0 && _bpmTimer.m_currentTick > _musicBeats[0] - m_timeAhead * _songDTO.IntervalsByBPM)
        {
            Beat beat = Instantiate<Beat>(m_beatPrefab, m_beatsParent.transform);
            beat.InitializeBeat(m_timeAhead, _songDTO.BPM, _musicBeats[0], _lineSize);

            _musicBeats.RemoveAt(0);
        }
        #endregion

    }


    private void OnGUI()
    {
        GUI.TextArea(new Rect(200, 100, 120, 20), "Current Beat: " + _bpmTimer.m_currentTick.ToString());

    }

}
