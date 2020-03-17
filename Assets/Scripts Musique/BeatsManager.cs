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
    List<int> _musicBeats ;
    [SerializeField]
    List<int> _leftBeats ;
    [SerializeField]
    List<int> _rightBeats ;

    #endregion

    private MusicState _currentMusicState;
    public MusicState CurrentMusicState { get => _currentMusicState; set => _currentMusicState = value; }

    public int m_timeAhead = 4;


    public GameObject m_beatsParent;
    public Beat m_beatPrefab;
    public Beat m_leftPrefab;
    public Beat m_rightPrefab;

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

        PopulateBeatLists();

        #endregion
        #region Récupération de la taille de la ligne
        Vector3 barreTempsPos = m_camera.ScreenToWorldPoint(
            new Vector3(100, Screen.height - m_barreTempsTransform.GetComponent<SpriteRenderer>().sprite.texture.height*4/2, 0)); 
            //*4 parce que dans gameoObject qui scale à 4, et /2 parce qu'on veut descendre de moitié
        m_barreTempsTransform.position = new Vector3(barreTempsPos.x, barreTempsPos.y, 0);
        _lineSize = Screen.width - (decimal)m_camera.WorldToScreenPoint(m_barreTempsTransform.position).x;
        Debug.Log("linesize = " + _lineSize);
        #endregion


        _bpmTimer = GetComponent<BPMTimer>();
        double timer = (double)60000 / (double)_songDTO.BPM / (double)_songDTO.IntervalsByBPM;
        Debug.Log("timer = " + timer);
        _bpmTimer.ChangeInterval(timer);

    }

    private void PopulateBeatLists()
    {
        _musicBeats = new List<int>();
        _leftBeats = new List<int>();
        _rightBeats = new List<int>();
        foreach (int i in _songDTO.MusicLine)
        {
            _musicBeats.Add(i);
        }
        foreach (int i in _songDTO.LeftBeats)
        {
            _leftBeats.Add(i);
        }
        foreach (int i in _songDTO.RightBeats)
        {
            _rightBeats.Add(i);
        }
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
        Transform[] beatsCreated = m_beatsParent.GetComponentsInChildren<Transform>();
        for (int i = 1; i < beatsCreated.Length; i++) //on omet volontairement le [0] qui est celui du parent
        {
            Destroy(beatsCreated[i].gameObject);
        }
        PopulateBeatLists();
    }


    void Update()
    {
        #region Initialisation des beats
        if (_musicBeats.Count > 0 && _bpmTimer.m_currentTick > _musicBeats[0] - m_timeAhead * _songDTO.IntervalsByBPM)
        {
            Beat beat = Instantiate(m_beatPrefab, m_beatsParent.transform);
            beat.InitializeBeat(m_timeAhead, _songDTO.BPM, _musicBeats[0], _lineSize);

            _musicBeats.RemoveAt(0);
        }
        if (_leftBeats.Count > 0 && _bpmTimer.m_currentTick > _leftBeats[0] - m_timeAhead * _songDTO.IntervalsByBPM)
        {
            Beat beat = Instantiate(m_leftPrefab, m_beatsParent.transform);
            beat.InitializeBeat(m_timeAhead, _songDTO.BPM, _musicBeats[0], _lineSize);

            _leftBeats.RemoveAt(0);
        }
        if (_rightBeats.Count > 0 && _bpmTimer.m_currentTick > _rightBeats[0] - m_timeAhead * _songDTO.IntervalsByBPM)
        {
            Beat beat = Instantiate(m_rightPrefab, m_beatsParent.transform);
            beat.InitializeBeat(m_timeAhead, _songDTO.BPM, _musicBeats[0], _lineSize);

            _rightBeats.RemoveAt(0);
        }//TODO enlever redondance
        #endregion

    }


    private void OnGUI()
    {
        GUI.TextArea(new Rect(200, 100, 120, 20), "Current Beat: " + _bpmTimer.m_currentTick.ToString());

    }

}
