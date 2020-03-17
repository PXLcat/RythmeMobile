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

    private BPMTimer _bmpTimer;

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



    }

    void Start()
    {
        //_BPMtimer = new CustomTimer((double)60000 / (double)_songDTO.BPM / (double)_songDTO.IntervalsByBPM);
        //_BPMtimer.Tick += OnTimedEvent;
    }



    public void StartTimer()
    {

    }

    internal void PauseTimer()
    {
        throw new NotImplementedException();
    }

    //public void StopTimer()
    //{
    //    _BPMtimer.Stop();
    //    _currentBeat = 0;
    //    _BPMtimer.CurrentTick = 0;
    //    m_isPlaying.m_isTrue = false;
    //}
    //public void PauseTimer()
    //{
    //    _BPMtimer.Stop();
    //    m_isPlaying.m_isTrue = false;
    //}

    void Update()
    {
        if (_currentBeat > _musicBeats[0] - m_timeAhead * _songDTO.IntervalsByBPM)
        {
            Beat beat = Instantiate<Beat>(m_beatPrefab, m_beatsParent.transform);
            beat.InitializeBeat(m_timeAhead, _songDTO.BPM, _musicBeats[0]);

            _musicBeats.RemoveAt(0);
        }
    }

    internal void StopTimer()
    {
        throw new NotImplementedException();
    }

    internal void ResumeTimer()
    {
        throw new NotImplementedException();
    }

    private void OnGUI()
    {
        GUI.TextArea(new Rect(200, 100, 120, 20), "Current Beat: " + _bp .ToString());

    }

}
