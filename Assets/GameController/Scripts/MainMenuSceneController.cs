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
        base.Update();

        switch (GameController.GameController.State)
        {
            case GameState.Active:
                break;
            case GameState.Loading:
                _gc.SetPlaySceneName(playSceneName);
                
                break;
            case GameState.Paused:
                break;
        }
    }
}
