using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
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
        if (gameObject.name == "Player1")
        {
            gameObject.GetComponent<Image>().sprite = gameController.GetPlayerImage(gameController.Player1Id);
        }
        else if (gameObject.name == "Player2")
        {
            gameObject.GetComponent<Image>().sprite = gameController.GetPlayerImage(gameController.Player2Id);
        }
        else if (gameObject.name == "ActualPlayer")
        {
            if (gameController.ActualTurn.EndsWith("1"))
            {
                gameObject.GetComponent<Image>().sprite = gameController.GetPlayerImage(gameController.Player1Id);
            }
            else if (gameController.ActualTurn.EndsWith("2"))
            {
                gameObject.GetComponent<Image>().sprite = gameController.GetPlayerImage(gameController.Player2Id);
            }
        }
    }
}
