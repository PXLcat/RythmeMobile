using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Beat : MonoBehaviour
{
    private Camera _cam;

    [SerializeField]
    private BeatType _type;

    public int _beatNumber; //public pour être récup dans le BeatManager
    [SerializeField]
    private int _distanceFromRightBorder;//c'est en pixels

    private decimal _decimalesDeplacement;

    [SerializeField]
    private bool _visible;

    private decimal _vitesse;

    public SOBoolean _trackPlaying;

    #region Track attributes
    private int _timeAhead; //temps d'avance, déterminé par le BeatsManager lors du InitializeBeat
    private int _bpm;
    #endregion

    private Transform _transform;
    private SpriteRenderer _sprite;



    private void Awake()
    {
        _cam = Camera.main;
        _transform = GetComponent<Transform>();
        _sprite = GetComponent<SpriteRenderer>();
    }

    private void Start() //Utiliser InitializeBeat plutôt que le Start
    {

    }

    public void InitializeBeat(int timeAhead, int bpm, int beatNumber, decimal lineSize)
    {
        _beatNumber = beatNumber;
        _timeAhead = timeAhead;
        _bpm = bpm;

        decimal distanceParTemps = lineSize / _timeAhead;
        float bps = bpm / 60f;
        _vitesse = distanceParTemps * (decimal)bps;
        
    }

    private void Update()
    {
        if (_trackPlaying.value)
        {
            #region Calcul déplacement

            decimal deplacement = _vitesse * (decimal)Time.deltaTime;

            _decimalesDeplacement += deplacement - Math.Floor(deplacement);

            while (_decimalesDeplacement >= 1)
            {
                _decimalesDeplacement -= 1;
                _distanceFromRightBorder += 1;
            }

            _distanceFromRightBorder += (int)Math.Floor(deplacement);
            //DistanceFromRightBorder étant un nombre à virgule, on stocke les décimales pour ne pas causer de problème avec le nombre de pixels qui doit être en int
            #endregion
            Vector3 worldPoint = _cam.ScreenToWorldPoint(new Vector3(Screen.width - _distanceFromRightBorder, Screen.height - 100, 0));
            _transform.position = new Vector3(worldPoint.x, worldPoint.y, 0);

            //_transform.position -= new Vector3((float)deplacement, 0, 0); //pck Unity = 100px/unit

            if (_sprite.isVisible == false)
            {
                Destroy(transform.gameObject);
            } 
        }
    }



}
public enum BeatType
{
    MUSIC,
    RYTHM
}
