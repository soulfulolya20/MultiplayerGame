using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class WaypointFollower : NetworkBehaviour
{
    [SerializeField] private GameObject[] waypoints;
    private int currentWaipointIndex = 0;

    [SerializeField] private float speed = 2f;
    private Vector2 transformPosition;

    private void Update()
    {
        if (Vector2.Distance(waypoints[currentWaipointIndex].transform.position, transform.position) < .1f)
        {
            currentWaipointIndex++;
            if (currentWaipointIndex >= waypoints.Length)
            {
                currentWaipointIndex = 0;
            }
        }

        transformPosition = Vector2.MoveTowards(transform.position, waypoints[currentWaipointIndex].transform.position,
            Time.deltaTime * speed);
        transform.position = transformPosition;
        SendPositionServerRpc();
    }

    [ServerRpc]
    void SendPositionServerRpc()
    {
        transform.position = transformPosition;
        HandlePositionClientRpc();
    }

    [ClientRpc]
    void HandlePositionClientRpc()
    {
        if(IsLocalPlayer) return;
        transform.position = transformPosition;
    } 
}