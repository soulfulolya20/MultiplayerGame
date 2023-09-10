using UnityEngine;

public class CameraController : MonoBehaviour
{
    private int numPlayers;

    private void Start()
    {
        // Загружаем количество игроков из PlayerPrefs
        numPlayers = PlayerPrefs.GetInt("NumPlayers", 1);

        // Активируем или деактивируем объекты игроков в зависимости от количества игроков
        AdjustPlayers();
        // Определяем размеры и положения камер в зависимости от количества игроков
        AdjustCameras();
    }

    private void AdjustPlayers()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("player");

        if (players.Length >= numPlayers)
        {
            for (int i = 0; i < players.Length; i++)
            {
                if (i < numPlayers)
                {
                    players[i].SetActive(true);
                }
                else
                {
                    players[i].SetActive(false);
                }
            }
        }
    }

    private void AdjustCameras()
    {
        Camera[] cameras = GetComponentsInChildren<Camera>();

        if (cameras.Length >= numPlayers)
        {
            for (int i = 0; i < cameras.Length; i++)
            {
                if (i < numPlayers)
                {
                    cameras[i].enabled = true;
                    cameras[i].rect = GetCameraRect(numPlayers, i);
                }
                else
                {
                    cameras[i].enabled = false;
                }
            }
        }
    }

    private Rect GetCameraRect(int numPlayers, int playerIndex)
    {
        // Вычисляем размер и положение камеры в зависимости от количества игроков
        switch (numPlayers)
        {
            case 1:
                return new Rect(0f, 0f, 1f, 1f);
            case 2:
                if (playerIndex == 0)
                {
                    return new Rect(0f, 0.5f, 1f, 0.5f);
                }
                else
                {
                    return new Rect(0f, 0f, 1f, 0.5f);
                }
            case 3:
                if (playerIndex == 0)
                {
                    return new Rect(0f, 0.667f, 1f, 0.333f);
                }
                else if (playerIndex == 1)
                {
                    return new Rect(0f, 0.333f, 1f, 0.333f);
                }
                else
                {
                    return new Rect(0f, 0f, 1, 0.333f);
                }
            case 4:
                if (playerIndex == 0)
                {
                    return new Rect(0f, 0f, 0.5f, 0.5f);
                }
                else if (playerIndex == 1)
                {
                    return new Rect(0.5f, 0.5f, 0.5f, 0.5f);
                    
                }
                else if (playerIndex == 2)
                {
                    return new Rect(0f, 0.5f, 0.5f, 0.5f);
                }
                else
                {
                    return new Rect(0.5f, 0f, 0.5f, 0.5f);
                }
            default:
                return new Rect(0f, 0f, 1f, 1f); // По умолчанию, один большой экран
        }
    }
}