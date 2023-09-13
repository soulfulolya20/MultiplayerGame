using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinishZone : MonoBehaviour
{
    private int playersInFinishZone = 0;
    private int totalPlayers;
    private AudioSource finishSound;

    public Text[] playerPlaceTexts; // Массив текстовых полей для вывода мест игроков
    private int[] playerPlaces; // Массив для хранения мест игроков

    private void Start()
    {
        // Автоматически определяем количество игроков с тегом "Player"
        GameObject[] players = GameObject.FindGameObjectsWithTag("player");
        totalPlayers = players.Length;
        finishSound = GetComponent<AudioSource>();

        // Инициализируем массив мест игроков
        playerPlaces = new int[totalPlayers];

        // Инициализируем текстовые поля для вывода мест игроков
        for (int i = 0; i < playerPlaceTexts.Length; i++)
        {
            playerPlaceTexts[i].text = "";
        }
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

            // Определяем место текущего игрока по порядку финиша
            int currentPlayerPlace = playersInFinishZone;

            // Выводим информацию о текущем игроке и его месте
            ShowCurrentPlayerPlace(player, currentPlayerPlace);

            // Проверяем, если все игроки вошли в зону финиша
            if (playersInFinishZone == totalPlayers)
            {
                // Выполняем переход в главное меню
                SceneManager.LoadScene("StartScene");
            }
        }
    }

    private void ShowCurrentPlayerPlace(GameObject player, int currentPlayerPlace)
    {
        // Выводим информацию о текущем игроке и его месте
        for (int i = 0; i < playerPlaces.Length; i++)
        {
            if (playerPlaces[i] == 0)
            {
                playerPlaces[i] = currentPlayerPlace;
                playerPlaceTexts[i].text = player.name + ": Place " + currentPlayerPlace;
                break;
            }
        }
    }
}