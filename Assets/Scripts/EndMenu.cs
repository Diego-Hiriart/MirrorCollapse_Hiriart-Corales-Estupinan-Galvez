using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndMenu : MonoBehaviour
{
    [SerializeField] Button mainMenu;
    [SerializeField] Button exitButton;

    [SerializeField] MainMenuController mainMenuController;

    public void ContinueGame()
    {
        mainMenuController.LoadLastGame();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

}
