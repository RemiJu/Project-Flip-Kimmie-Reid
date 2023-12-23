using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string newGameScene;
    public string loadGameScene;
    public string shortcut1Scene;
    public string shortcut2Scene;
    public string shortcut3Scene;
    public string shortcut4Scene;
    public string shortcut5Scene;

    private void Start()
    {
        print("hello");
    }

    public void NewGame()
    {
        print("clicked");
        SceneManager.LoadScene(newGameScene);
    }

    public void LoadGame()
    {
        print("LoadGame button was clicked.");
    }

    public void Shortcut1()
    {
        print("Shortcut1 button was clicked.");
        SceneManager.LoadScene(shortcut1Scene);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
