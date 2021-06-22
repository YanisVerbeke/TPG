using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHandler : MonoBehaviour
{
    GameController gameController;

    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void NextButton()
    {
        if (gameController.ActualTurn == "Player1" && gameController.CurrentState == GameController.State.GAME)
        {
            gameController.CurrentState = GameController.State.PLAYER;
            gameController.ActualTurn = "Player2";
        }
        else if (gameController.ActualTurn == "Player2" && gameController.CurrentState == GameController.State.GAME)
        {
            gameController.CurrentState = GameController.State.RESULT;
            gameController.ActualTurn = "Player1";
        }
        else
        {
            gameController.CurrentState = GameController.State.PLAYER;
        }

        gameController.UpdateCurrentScene();
    }

    public void StartButton()
    {
        gameController.CurrentState = GameController.State.GAME;
        gameController.UpdateCurrentScene();
    }

    public void QuitButton()
    {
        gameController.ResetGame();
    }
}
