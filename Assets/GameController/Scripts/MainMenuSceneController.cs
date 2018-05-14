using UnityEngine;
using GameController;

public class MainMenuSceneController : SceneController {


    private void Start()
    {
        base.Start();
    }

    private void Update()
    {
        switch (GameController.GameController.State)
        {
            case GameState.Active:
                break;
            case GameState.Loading:
                break;
            case GameState.Paused:
                break;
        }
    }
}
