using UnityEngine;

public class Selection_UI : MonoBehaviour
{
    public void OpenUI(GameObject ui)
    {
        gameObject.SetActive(false);
        ui.SetActive(true);
    }
}
