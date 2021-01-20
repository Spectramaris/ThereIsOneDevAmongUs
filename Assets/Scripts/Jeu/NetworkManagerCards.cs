using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;

[AddComponentMenu("")]
public class NetworkManagerCards : NetworkManager
{
    public Transform firstPlayerSpawn;
    public Transform secondPlayerSpawn;

    public TextMeshProUGUI textGUI;

    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        Transform start = numPlayers == 0 ? firstPlayerSpawn : secondPlayerSpawn;
        GameObject playerGO = Instantiate(playerPrefab, start.position, start.rotation);
        NetworkServer.AddPlayerForConnection(conn, playerGO);
    }
}
