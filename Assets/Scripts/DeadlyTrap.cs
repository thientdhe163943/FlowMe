using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeadlyTrap : MonoBehaviour
{
    // Kích hoạt khi có vật thể chạm vào trap
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player chết vì trap!");
            GameOver();
        }
    }

    // Cho phép dev test bằng phím G
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            Debug.Log(" Dev nhấn G để test Game Over");
            GameOver();
        }
    }

    void GameOver()
    {
        // Load lại màn chơi hiện tại
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
