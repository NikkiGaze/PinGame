using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour
{
    [SerializeField] private int startTimerValue;
    [SerializeField] private Text timerText;
    [SerializeField] private GameObject endGamePanel;
    [SerializeField] private Text endText;
    [SerializeField] private Text[] pinTexts = new Text[3];
    [SerializeField] private int[] pinStartValues = new int[3];
    private int[] _pinValues = new int[3];
    private float _startTime;
    private bool _gameOn;
    // Start is called before the first frame update
    
    public void ChangePinStates(int[] changes)
    {
        for (int i = 0; i < 3; i++)
        {
            int newPinValue = _pinValues[i] + changes[i];
            if (newPinValue < 0)
            {
                newPinValue = 0;
            }
            else if (newPinValue > 10)
            {
                newPinValue = 10;
            }

            _pinValues[i] = newPinValue;
        }
        UpdatePins();
        CheckWinCombination();
    }
    public void OnRestart()
    {
        endGamePanel.SetActive(false);
        StartGame();
    }
    void Start()
    {
        StartGame();
    }

    void Update()
    {
        if (!_gameOn)
        {
            return;
        }

        float deltaTime = Time.time - _startTime;
        float timeLeft = startTimerValue - Mathf.Round(deltaTime);
        timerText.text = "Time left: " + (timeLeft).ToString();
        if (timeLeft <= 0)
        {
            OnPlayerLoose();
        }
    }

    void UpdatePins()
    {
        for (int i = 0; i < 3; i++)
        {
            pinTexts[i].text = _pinValues[i].ToString();
        }
    }
    
    void CheckWinCombination()
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

    void OnPlayerWin()
    {
        ShowEndGamePanel(true);
    }
    
    void OnPlayerLoose()
    {
        ShowEndGamePanel(false);
    }

    void ShowEndGamePanel(bool win)
    {
        if (win)
        {
            endText.text = "You win!";
        }
        else
        {
            endText.text = "You loose!";
        }

        endGamePanel.SetActive(true);
        _gameOn = false;
    }

    private void StartGame()
    {
        timerText.text = startTimerValue.ToString();
        for (int i = 0; i < 3; i++)
        {
            _pinValues[i] = pinStartValues[i];
        }
        UpdatePins();
        _startTime = Time.time;
        _gameOn = true;
    }
}
