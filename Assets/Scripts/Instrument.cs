using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Instrument : MonoBehaviour
{
    [SerializeField] private Text _text;
    [SerializeField] private GameLogic _gameLogic;
    [SerializeField] private int[] _pinChangings = new int[3];
    
    private void Start()
    {
        _text.text = $"{_pinChangings[0]}|{_pinChangings[1]}|{_pinChangings[2]}";
    }
    
    public void OnPressed()
    {
        _gameLogic.ChangePinStates(_pinChangings);
    }
}
