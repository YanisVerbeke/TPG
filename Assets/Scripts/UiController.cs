using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{
    Text ScoreP1Text, ScoreP2Text;
    GameController gameController;
    int scoreP1Value, scoreP2Value;

    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        ScoreP1Text = GameObject.Find("ScoreP1Text").GetComponent<Text>();
        ScoreP2Text = GameObject.Find("ScoreP2Text").GetComponent<Text>();
        scoreP1Value = 0;
        scoreP2Value = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameController.CurrentState == GameController.State.RESULT)
        {
            if (scoreP1Value < gameController.ScorePlayer1)
            {
                scoreP1Value++;
            }
            if (scoreP2Value < gameController.ScorePlayer2)
            {
                scoreP2Value++;
            }
            ScoreP1Text.text = scoreP1Value.ToString();
            ScoreP2Text.text = scoreP2Value.ToString();
        } else
        {
            scoreP1Value = 0;
            scoreP2Value = 0;
        }
    }
}
