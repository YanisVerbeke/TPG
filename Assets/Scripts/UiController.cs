using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{
    Text textComponent;
    GameController gameController;

    // Start is called before the first frame update
    void Start()
    {
        textComponent = this.gameObject.GetComponent<Text>();
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        textComponent.text = $"Score : {gameController.Score}";
    }
}
