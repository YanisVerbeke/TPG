using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public int Score { get; set; }
    public enum State { MENU, COUNTDOWN, GAME, RESULT };
    public State CurrentState { get; set; }
    private GameObject _gamePanel, _menuPanel, _resultPanel, _papersObject;

    // Start is called before the first frame update
    void Start()
    {
        Score = 0;
        CurrentState = State.GAME;
        _gamePanel = GameObject.Find("GamePanel");
        _menuPanel = GameObject.Find("MenuPanel");
        //_resultPanel = GameObject.Find("ResultPanel");
        _papersObject = GameObject.Find("PapersObject");
        UpdateCurrentScene();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateCurrentScene()
    {
        _gamePanel.SetActive(false);
        _menuPanel.SetActive(false);
        _papersObject.SetActive(false);
        //_resultPanel.SetActive(true);
        switch(CurrentState)
        {
            case State.MENU:
                _menuPanel.SetActive(true);
                break;
            case State.COUNTDOWN:
                _gamePanel.SetActive(true);
                _papersObject.SetActive(true);
                break;
            case State.GAME:
                _gamePanel.SetActive(true);
                _papersObject.SetActive(true);
                break;
            case State.RESULT:
                //_resultPanel.SetActive(true);
                break;
            default:
                break;
        }
    }
}
