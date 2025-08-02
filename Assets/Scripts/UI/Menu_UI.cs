using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu_UI : MonoBehaviour
{
    public void OpenUI(GameObject ui)
    {
        gameObject.SetActive(false);
        ui.SetActive(true);
    }
}
