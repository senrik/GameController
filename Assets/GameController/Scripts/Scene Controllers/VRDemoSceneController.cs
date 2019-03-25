using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameController;

public class VRDemoSceneController : SceneController {

	// Use this for initialization
	new void Start () {
        base.Start();
	}
	

    new void LoadingUpdate()
    {
        base.LoadingUpdate();

        if (!sceneReady && playerPlaced)
        {
            sceneReady = true;
        }
        else
        {
            if (!playerPlaced)
            {
                PlacePlayer();
            }
        }

        _player.ToggleTeleport(true);
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
