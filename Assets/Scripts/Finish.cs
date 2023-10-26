using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinishZone : NetworkBehaviour
{
    private List<NetworkObject> playersInFinish = new List<NetworkObject>();
    private List<NetworkObject> allPlayers = new List<NetworkObject>();
    private AudioSource finishSound;

    public Text[] playerPlaceTexts;

    private void Start()
    {
        finishSound = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("player"))
        {
            NetworkObject playerNetworkObject = other.GetComponent<NetworkObject>();

            if (playerNetworkObject != null)
            {
                // Записываем игрока, достигшего финиша, в список
                playersInFinish.Add(playerNetworkObject);

                // Отключаем возможность движения
                PlayerBase playerController = other.GetComponent<PlayerBase>();
                
                if (playerController != null)
                {
                    playerController.enabled = false;
                }

                // Воспроизводим звук финиша
                finishSound.Play();

                // Показываем информацию о текущем игроке и его месте
                ShowCurrentPlayerPlace(playerNetworkObject);
            }

            // Если количество игроков в списке достигло общего числа игроков, переходим на стартовую сцену
            if (playersInFinish.Count == GameManager.CountPlayers())
            {
                ServerGameOverServerRpc();
            }
        }
    }

    private void ShowCurrentPlayerPlace(NetworkObject playerNetworkObject)
    {
        int playerPlace = playersInFinish.IndexOf(playerNetworkObject) + 1;
        string playerName = (playerNetworkObject.OwnerClientId + 1).ToString();

        foreach (Text playerPlaceText in playerPlaceTexts)
        {
            if (string.IsNullOrEmpty(playerPlaceText.text))
            {
                playerPlaceText.text = "Player " + playerName + " took place " + playerPlace;
                break;
            }
        }
    }

    [ServerRpc]
    private void ServerGameOverServerRpc()
    {
        RpcLoadStartSceneClientRpc();
    }

    [ClientRpc]
    private void RpcLoadStartSceneClientRpc()
    {
        // Отключите всех клиентов и сервер перед переходом на стартовую сцену
        Cleanup();

        // Задержка перед переходом на стартовую сцену
        StartCoroutine(LoadStartSceneDelayed());
    }


    private IEnumerator LoadStartSceneDelayed()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("StartScene");
    }
    
    void Cleanup()
    {
        if (NetworkManager.Singleton != null)
        {
            Destroy(NetworkManager.Singleton.gameObject);
        }
    }
}
