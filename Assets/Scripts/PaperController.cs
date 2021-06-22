using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperController : MonoBehaviour
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
        if (transform.position.y < -25)
        {
            transform.position += new Vector3(0, 48, 0);
            if (gameController.ActualTurn == "Player1")
            {
                gameController.ScorePlayer1++;
            }
            else if (gameController.ActualTurn == "Player2")
            {
                gameController.ScorePlayer2++;
            }

        }
    }

}
