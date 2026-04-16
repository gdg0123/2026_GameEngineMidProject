using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    public GameObject helpPanel;
    public GameObject menuPanel;

    void Start()
    {
        helpPanel.SetActive(false);
    }

    public void GameStart()
    {
        SceneManager.LoadScene("PlayScene_1");
    }

    public void GameExit()
    {
        Application.Quit();
    }

    public void OpenHelp()
    {
        helpPanel.SetActive(true);
    }

    public void CloseHelp()
    {
        helpPanel.SetActive(false);
    }


    public void BackMenu()
    {
        SceneManager.LoadScene("TitleScene");
    }

    public void OpenMenu()
    {
        menuPanel.SetActive(true);
    }

    public void CloseMenu()
    {
        menuPanel.SetActive(false);
    }

}
