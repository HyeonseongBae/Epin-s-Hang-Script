using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStartButton : ObjectControll
{
	void Update ()
    {
        NextScene();
	}

    void NextScene()
    {
        if (isTouch)
        {
            SceneManager.LoadScene(1);
        }
    }
}
