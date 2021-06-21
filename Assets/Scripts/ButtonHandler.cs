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
        gameController.CurrentState = GameController.State.PLAYER;
        gameController.UpdateCurrentScene();
    }

    public void StartButton()
    {
        gameController.CurrentState = GameController.State.COUNTDOWN;
        gameController.UpdateCurrentScene();
    }
}
