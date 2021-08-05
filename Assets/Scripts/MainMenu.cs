using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private EventSystem _eventSystem;
    [FormerlySerializedAs("_mainMenu")] [SerializeField] private GameObject mainMenu;
    [FormerlySerializedAs("_optionsMenu")] [SerializeField] private GameObject optionsMenu;

    public void StartGame()
    {
        SceneManager.LoadSceneAsync("SampleScene", LoadSceneMode.Single);
    }

    public void QuitGame()
    {
        Application.Quit(1);
    }

    public void ToogleOptions(bool isOptions)
    {
        if (isOptions)
        {
            mainMenu.SetActive(false);
            optionsMenu.SetActive(true);
        }
        else
        {
            mainMenu.SetActive(true);
            optionsMenu.SetActive(false);
        }
    }

    public void SetFirstSelectedEventSystem(GameObject objectSelected)
    {
        _eventSystem.SetSelectedGameObject(null);
        _eventSystem.SetSelectedGameObject(objectSelected);
    }
}