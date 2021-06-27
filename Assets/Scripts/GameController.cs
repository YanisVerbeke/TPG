using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public enum State { MENU, PLAYER, GAME, RESULT };

    public int Player1Id { get; set; }
    public int Player2Id { get; set; }
    public int ScorePlayer1 { get; set; }
    public int ScorePlayer2 { get; set; }
    public State CurrentState { get; set; }
    public bool IsGameStarted { get; set; }
    public bool IsGameEnded { get; set; }
    public string ActualTurn { get; set; }

    private GameObject _gamePanel, _playerPanel, _menuPanel, _resultPanel, _papersObject, _endText, _endButton, _emptyRoll;
    private List<GameObject> _papersList;
    private Text _countdownText;
    private float _countdownTimer;
    private VisualTimer _visualTimer;
    [SerializeField]
    private List<Sprite> playerImages;

    private int _messageId = 0;


    // Start is called before the first frame update
    void Start()
    {
        _gamePanel = GameObject.Find("GamePanel");
        _playerPanel = GameObject.Find("PlayerPanel");
        _menuPanel = GameObject.Find("MenuPanel");
        _resultPanel = GameObject.Find("ResultPanel");
        _papersObject = GameObject.Find("PapersObject");
        _countdownText = GameObject.Find("CountdownText").GetComponent<Text>();
        _endText = GameObject.Find("EndText");
        _endButton = GameObject.Find("EndButton");
        _visualTimer = GameObject.Find("VisualTimer").GetComponent<VisualTimer>();
        _emptyRoll = GameObject.Find("EmptyRoll");
        FillPaperList();
        Player1Id = 0;
        Player2Id = 0;
        //ResetGame();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (CurrentState == State.GAME)
        {
            StartGameCountdown();
            if (IsGameEnded && _papersList[0].GetComponent<Rigidbody>().velocity.y > -2)
            {
                SetActiveEndUI(true);
            }
        }
    }

    public void UpdateCurrentScene()
    {
        _gamePanel.SetActive(false);
        _playerPanel.SetActive(false);
        _menuPanel.SetActive(false);
        _papersObject.SetActive(false);
        _resultPanel.SetActive(false);
        _emptyRoll.SetActive(false);
        switch (CurrentState)
        {
            case State.MENU:
                _menuPanel.SetActive(true);
                break;
            case State.PLAYER:
                _playerPanel.SetActive(true);
                break;
            case State.GAME:
                NewGame();
                _gamePanel.SetActive(true);
                _papersObject.SetActive(true);
                break;
            case State.RESULT:
                _resultPanel.SetActive(true);
                _emptyRoll.SetActive(true);
                break;
            default:
                break;
        }
    }

    private void StartGameCountdown()
    {
        if (_countdownTimer > 1)
        {
            _countdownTimer -= Time.deltaTime;
            _countdownText.text = Math.Truncate(_countdownTimer).ToString();
            _countdownText.color = new Color { r = 0, g = 0, b = 0, a = ((float)(_countdownTimer - Math.Truncate(_countdownTimer))) };
        }
        else if (_countdownTimer >= 0)
        {
            _countdownTimer -= Time.deltaTime;
            IsGameStarted = true;
            _countdownText.text = "GO !";
            _countdownText.color = new Color { r = 0, g = 0, b = 0, a = ((float)(_countdownTimer - Math.Truncate(_countdownTimer))) };
        }
        else
        {
            _countdownText.text = "";
        }
    }

    private void SetActiveEndUI(bool value)
    {
        _endText.SetActive(value);
        _endButton.SetActive(value);
    }

    public void NewGame()
    {
        IsGameStarted = false;
        IsGameEnded = false;
        _countdownText.text = "";
        _countdownTimer = 4;
        _visualTimer.ResetTimer();
        ResetPapersPosition();
        SetActiveEndUI(false);
    }

    public void ResetGame()
    {
        NewGame();
        //Player1Id = UnityEngine.Random.Range(0,9);
        //Player2Id = UnityEngine.Random.Range(0, 9);
        ScorePlayer1 = 0;
        ScorePlayer2 = 0;
        ActualTurn = "Player1";
        CurrentState = State.MENU;
        UpdateCurrentScene();
    }

    private void FillPaperList()
    {
        _papersList = new List<GameObject>();
        _papersList.Add(GameObject.Find("Paper"));
        _papersList.Add(GameObject.Find("Paper (1)"));
        _papersList.Add(GameObject.Find("Paper (2)"));
        _papersList.Add(GameObject.Find("Paper (3)"));
    }

    private void ResetPapersPosition()
    {
        for (int i = 0; i < _papersList.Count; i++)
        {
            _papersList[i].transform.position = new Vector3(0, 12 * i, -5);
        }
    }

    public Sprite GetPlayerImage(int id)
    {
        return playerImages[id];
    }

    public void GetFlutterSettings(string data)
    {
        FlutterUnityPlugin.Message message = FlutterUnityPlugin.Messages.Receive(data);

        Player1Id = int.Parse(message.data.Split(',')[0]);
        Player2Id = int.Parse(message.data.Split(',')[1]);
        _messageId = int.Parse(message.data.Split(',')[2]);

        ResetGame();
    }

    public void SendFlutterResult()
    {
        string data = "";

        if (ScorePlayer1 > ScorePlayer2)
        {
            data = "player1,";
        }
        else if (ScorePlayer2 > ScorePlayer1)
        {
            data = "player2,";
        }
        else
        {
            data = "draw,";
        }

        data += (_messageId + 1).ToString();

        FlutterUnityPlugin.Message message = new FlutterUnityPlugin.Message { id = _messageId, data = data };
        FlutterUnityPlugin.Messages.Send(message);
    }
}
