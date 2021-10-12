using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour
{    private const int _minPinValue = 0;
     private const int _maxPinValue = 10;
     
    [SerializeField] private int _startTimerValue;
    [SerializeField] private Text _timerText;
    [SerializeField] private GameObject _endGamePanel;
    [SerializeField] private Text _endText;
    [SerializeField] private Text[] _pinTexts = new Text[3];
    [SerializeField] private int[] _pinStartValues = new int[3];
    private int[] _pinValues = new int[3];
    private float _startTime;
    private bool _gameOn;

    private void Start()
    {
        StartGame();
    }

    private void Update()
    {
        if (!_gameOn)
        {
            return;
        }

        float deltaTime = Time.time - _startTime;
        float timeLeft = _startTimerValue - Mathf.Round(deltaTime);
        _timerText.text = "Time left: " + timeLeft;
        if (timeLeft <= 0)
        {
            OnPlayerLoose();
        }
    }
    
    public void ChangePinStates(int[] changes)
    {
        for (var i = 0; i < 3; i++)
        {
            int newPinValue = _pinValues[i] + changes[i];
            _pinValues[i] = Mathf.Clamp(newPinValue, _minPinValue, _maxPinValue);
        }
        UpdatePins();
        CheckWinCombination();
    }
    
    public void OnRestart()
    {
        _endGamePanel.SetActive(false);
        StartGame();
    }

    private void UpdatePins()
    {
        for (var i = 0; i < 3; i++)
        {
            _pinTexts[i].text = _pinValues[i].ToString();
        }
    }
    
    private void CheckWinCombination()
    {
        foreach (var pinValue in _pinValues)
        {
            if (pinValue != 5)
            {
                return;
            }
        }
        OnPlayerWin();
    }

    private void OnPlayerWin()
    {
        ShowEndGamePanel(true);
    }
    
    private void OnPlayerLoose()
    {
        ShowEndGamePanel(false);
    }

    private void ShowEndGamePanel(bool win)
    {
        _endText.text = win ? "You win!" : "You loose!";

        _endGamePanel.SetActive(true);
        _gameOn = false;
    }

    private void StartGame()
    {
        _timerText.text = _startTimerValue.ToString();
        for (var i = 0; i < 3; i++)
        {
            _pinValues[i] = _pinStartValues[i];
        }
        UpdatePins();
        _startTime = Time.time;
        _gameOn = true;
    }
}
