using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VisualTimer : MonoBehaviour
{
    GameController gameController;
    Image image;
    int timer;
    Text textTimer;

    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        image = GetComponent<Image>();
        textTimer = GetComponentInChildren<Text>();
        timer = 10;
        image.fillAmount = 0;
        textTimer.text = "";
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (gameController.IsGameStarted && !gameController.IsGameEnded)
        {
            if (image.fillAmount >= 1)
            {
                timer--;
                image.fillAmount = 0;
            }
            image.fillAmount += Time.deltaTime;
            textTimer.text = timer.ToString();
            if (timer <= 0)
            {
                image.fillAmount = 1;
                gameController.IsGameEnded = true;
            }
        }
    }

    public void ResetTimer()
    {
        timer = 10;
        image.fillAmount = 0;
        textTimer.text = "";
    }
}
