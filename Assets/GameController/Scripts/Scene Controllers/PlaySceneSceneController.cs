using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySceneSceneController : GameController.SceneController {

	// Use this for initialization
	new void Start () {
        base.Start();
	}
	
    new void LoadingUpdate()
    {
        base.LoadingUpdate();

        if (!sceneReady)
        {
            sceneReady = true;
        }   
    }

	// Update is called once per frame
	new void Update () {
        base.Update();

		switch(GameController.GameController.State)
        {
            case GameController.GameState.Active:
                ActiveUpdate();
                break;
            case GameController.GameState.Loading:
                LoadingUpdate();
                break;
            case GameController.GameState.Paused:
                PausedUpdate();
                break;
        }
	}
}
