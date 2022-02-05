using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadWinScene : MonoBehaviour
{
    private SceneTransitions sceneTransitions;

    private void Start()
    {
        sceneTransitions = FindObjectOfType<SceneTransitions>();
    }

    void Update()
    {
        HandleWinSceneTransition();
    }

    private void HandleWinSceneTransition()
    {
        if (GameObject.FindGameObjectsWithTag("Enemy").Length <= 0)
        {
            sceneTransitions.LoadScene("WinScene");
        }
    }
}
