using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Setting_Ui : MonoBehaviour
{
    [SerializeField] private GameObject settingsUI;
    [SerializeField] private GameObject creditsUI;
    [SerializeField] private GameObject helpUI;

    public void BackToMenu(GameObject ui)
    {
        gameObject.SetActive(false);
        ui.SetActive(true);
    }

    public void SwitchTo(GameObject ui)
    {
        if (ui == settingsUI)
        {
            settingsUI.SetActive(true);
            creditsUI.SetActive(false);
            helpUI.SetActive(false);
        }
        else if (ui == creditsUI)
        {
            settingsUI.SetActive(false);
            creditsUI.SetActive(true);
            helpUI.SetActive(false);
        }
        else
        {
            settingsUI.SetActive(false);
            creditsUI.SetActive(false);
            helpUI.SetActive(true);
        }
    }
}
