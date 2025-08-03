using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Game_UI : MonoBehaviour
{
    [SerializeField] private Sprite followerAvatar;
    [SerializeField] private Image avatar;
    [SerializeField] private TextMeshProUGUI stepCountText;

    [SerializeField] private GameObject pausePanel;
    private void Start()
    {
        ChangeStepCount(0);
    }

    private void Update()
    {
        ChangeAvatar();
    }

    private void ChangeAvatar()
    {
        if (!InputManager.Instance.GetIsReplay())
            return;

        avatar.sprite = followerAvatar;
    }

    public void ChangeStepCount(int count)
    {
        stepCountText.text = count.ToString("00");
    }


    public void PauseButton()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0;
    }
    public void ResumeButton()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1;
    }

}
