using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishZone : MonoBehaviour
{
    private int playersInFinishZone = 0;
    private int totalPlayers;
    private AudioSource finishSound;

    private void Start()
    {
        // Автоматически определяем количество игроков с тегом "Player"
        GameObject[] players = GameObject.FindGameObjectsWithTag("player");
        totalPlayers = players.Length;
        finishSound = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("player"))
        {
            // Отключаем игрока
            GameObject player = other.gameObject;
            player.SetActive(false);

            // Отключаем камеру, если она есть
            Camera playerCamera = player.GetComponentInChildren<Camera>();
            if (playerCamera != null)
            {
                playerCamera.gameObject.SetActive(false);
            }

            playersInFinishZone++;
            finishSound.Play();

            // Проверяем, если все игроки вошли в зону финиша
            if (playersInFinishZone == totalPlayers)
            {
                // Выполняем переход в главное меню
                SceneManager.LoadScene("StartScene");
            }
        }
    }
}