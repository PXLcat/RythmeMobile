using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackManagerScript : MonoBehaviour
{
    /*
        Gère l'AUDIO et les Stop/Play/Pause. Doit envoyer les statuts mais pas gérer directement les beats.
    */
    private AudioSource _levelTrack;
    private MusicState _currentMusicState;
    private BeatsManager _beatManager;

    public MusicState CurrentMusicState
    {
        get { return _currentMusicState; }
        set { _currentMusicState = value;
            _beatManager.CurrentMusicState = value;}
    }

    private void Awake()
    {
        _levelTrack = GetComponent<AudioSource>();
        _beatManager = GetComponent<BeatsManager>();
    }

#if UNITY_EDITOR
    private void OnGUI()
    {
        //Bouton PLAY/PAUSE
        if (GUI.Button(new Rect(800, 140, 120, 60), (_currentMusicState == MusicState.PLAYING) ? "PAUSE" : "PLAY"))
        {
            switch (_currentMusicState)
            {
                case MusicState.STOPPED:
                    PlayMusic();
                    break;
                case MusicState.PLAYING:
                    PauseMusic();
                    break;
                case MusicState.PAUSED:
                    ResumeMusic();
                    break;
                default:
                    break;
            }
        }
        //Bouton STOP
        if (GUI.Button(new Rect(470, 70, 50, 30), "STOP"))
        {
            _levelTrack.Stop();
            CurrentMusicState = MusicState.STOPPED;
            StopMusic();
        }
    }
#endif
    private void PlayMusic()
    {
        CurrentMusicState = MusicState.PLAYING;
        _levelTrack.Play();
        _beatManager.StartTimer();
    }
    private void PauseMusic()
    {
        CurrentMusicState = MusicState.PAUSED;
        _levelTrack.Pause();
        _beatManager.PauseTimer();
    }
    private void ResumeMusic()
    {
        CurrentMusicState = MusicState.PLAYING;
        _levelTrack.Play();
        _beatManager.ResumeTimer();

    }
    private void StopMusic()
    {
        CurrentMusicState = MusicState.STOPPED;
        _levelTrack.Stop();
        _beatManager.StopTimer();
    }
}
public enum MusicState
{
    STOPPED,
    PLAYING,
    PAUSED
}
