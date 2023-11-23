using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        // Application.Quit() does not work in the editor
        // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game in the ediotor
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
}