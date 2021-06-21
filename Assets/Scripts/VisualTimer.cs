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
        timer = 10;
        image.fillAmount = 0;
        textTimer = GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (gameController.IsTimerActive)
        {
            if (image.fillAmount >= 1)
            {
                timer--;
                image.fillAmount = 0;
            }
            image.fillAmount += Time.deltaTime;
        }
        textTimer.text = timer.ToString();
        if (timer <= 0)
        {
            image.fillAmount = 1;
            gameController.IsTimerActive = false;
        }
    }
}
