using UnityEngine;
using GameController;

public class MainMenuSceneController : SceneController {

    public string playSceneName = "viveDemoScene01";

    new void Start()
    {
        base.Start();
    }

    new void Update()
    {
        switch (GameController.GameController.State)
        {
            case GameState.Active:
                break;
            case GameState.Loading:
                _gc.SetPlaySceneName(playSceneName);
                _gc.SceneReady = true;
                break;
            case GameState.Paused:
                break;
        }
    }
}
