using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Instrument : MonoBehaviour
{
    [SerializeField] private Text text;
    [SerializeField] private GameLogic gameLogic;
    [SerializeField] private int[] pinChangings = new int[3];
    void Start()
    {
        text.text = $"{pinChangings[0]}|{pinChangings[1]}|{pinChangings[2]}";
    }
    
    public void OnPressed()
    {
        gameLogic.ChangePinStates(pinChangings);
    }
}
