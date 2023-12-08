using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuToggleController : MonoBehaviour
{
    [Header("Sounds Settings")]
    public string openMenuSound = "Confirmation";
    public string closeMenuSound = "Confirmation";

    public GameObject mainMenu;
    public GameObject miniMenu;

    private void Start()
    {
        mainMenu.SetActive(true);
        miniMenu.SetActive(false);
    }

    public void ShowMainMenu()
    {
        mainMenu.SetActive(true);
        miniMenu.SetActive(false);

        // Play the open menu sound
        SoundManager.Instance.PlayActionSound(openMenuSound);
    }

    public void HideMainMenu()
    {
        mainMenu.SetActive(false);
        miniMenu.SetActive(true);

        // Play the close menu sound
        SoundManager.Instance.PlayActionSound(closeMenuSound);
    }
}
