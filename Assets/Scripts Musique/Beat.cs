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
    [SerializeField]
    private int _beatNumber;
    [SerializeField]
    private int _distanceFromRightBorder;//c'est en pixels

    private decimal _decimalesDeplacement;
    [SerializeField]
    private decimal _lineSize;

    [SerializeField]
    private bool _visible;

    private decimal _vitesse;


    #region Track attributes
    private int _timeAhead; //temps d'avance, déterminé par le BeatsManager lors du InitializeBeat
    private int _bpm;
    #endregion

    private Transform _transform;
    private SpriteRenderer _sprite; 

    private void Awake()
    {
        Debug.Log("nouveau beat");
        _cam = Camera.main;
        _transform = GetComponent<Transform>();
        _sprite = GetComponent<SpriteRenderer>();
    }

    private void Start() //Utiliser InitializeBeat plutôt que le Start
    {

    }

    public void InitializeBeat(int timeAhead, int bpm, int beatNumber)
    {
        _beatNumber = beatNumber;
        _timeAhead = timeAhead;
        _bpm = bpm;

        _lineSize = Screen.width - 100;//TODO récup la position de la barreTemps
        decimal distanceParTemps = _lineSize / _timeAhead;
        float bps = bpm / 60f;
        _vitesse = distanceParTemps * (decimal)bps;
        
    }

    private void Update()
    {
        if (true)
        {
            #region Calcul déplacement

            decimal deplacement = _vitesse * (decimal)Time.deltaTime;

            _decimalesDeplacement += deplacement - Math.Floor(deplacement);

            while (_decimalesDeplacement >= 1)
            {
                _decimalesDeplacement -= 1;
                _distanceFromRightBorder += 1;
            }

            //_distanceFromRightBorder += (int)Math.Floor(deplacement);
            //DistanceFromRightBorder étant un nombre à virgule, on stocke les décimales pour ne pas causer de problème avec le nombre de pixels qui doit être en int
            #endregion

            //transform.position = _cam.ScreenToWorldPoint(new Vector3(Screen.width-_distanceFromRightBorder, -100, 0));

            _transform.position -= new Vector3((float)Math.Floor(deplacement) / 100, 0, 0); //pck Unity = 100px/unit

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
