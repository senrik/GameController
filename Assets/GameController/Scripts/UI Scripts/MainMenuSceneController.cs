using UnityEngine;
using GameController;

public class MainMenuSceneController : SceneController {

    public string playSceneName = "viveDemoScene01";
    public MenuController mainMenu;
    private bool mainMenuBound;
    new void Start()
    {
        base.Start();
    }

    private void BindMainMenuActions()
    {
        //Debug.Log("Binding Main Menu Buttons");
        try
        {
            mainMenu.GetButtonByTag("PlayButton").OnInteract.AddListener(delegate { _gc.LoadMe(playSceneName); });
        }
        catch (System.Exception e)
        {
            //Debug.Log(string.Format("Exception \"{0}\" encountered while trying to set the play button action.", e.Message));
        }

        mainMenuBound = true;

    }

    new void LoadingUpdate()
    {
        base.LoadingUpdate();

        if (!sceneReady && playerPlaced)
        {
            if (!mainMenuBound)
            {
                BindMainMenuActions();
            }
            else
            {
                sceneReady = true;
            }
        }
        else
        {
            if (!playerPlaced)
            {
                PlacePlayer();
            }
        }
    }

    new void Update()
    {
        base.Update();

        switch (GameController.GameController.State)
        {
            case GameState.Active:
                #region
                ActiveUpdate();
                #endregion
                break;
            case GameState.Loading:
                #region
                LoadingUpdate();
                #endregion
                break;
            case GameState.Paused:
                #region
                LoadingUpdate();
                #endregion
                break;
        }
    }
}
