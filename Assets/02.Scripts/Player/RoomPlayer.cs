using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

// Room Player Prefab : 게임이 시작되기 전 게임 대기실(Game Room Scene)에서 플레이어가 서버와 상호작용하기 위한 것
public class RoomPlayer : NetworkRoomPlayer
{
    [SyncVar]
    public EPlayerColor playerColor;

    public void Start()
    {
        base.Start();

        if (isServer)
        {
            SpawnLobbyPlayerCharacter();
        }
    }

    private void SpawnLobbyPlayerCharacter()
    {
        var roomSlots = (NetworkManager.singleton as RoomManager).roomSlots;
        EPlayerColor color = EPlayerColor.Red;

        for(int i = 0; i < (int)EPlayerColor.Lime + 1; i++)
        {
            bool isFindSameColor = false;

            foreach(var roomPlayer in roomSlots)
            {
                var amongUsRoomPlayer = roomPlayer as RoomPlayer;

                if(amongUsRoomPlayer.playerColor == (EPlayerColor)i && roomPlayer.netId != netId)
                {
                    isFindSameColor = true;
                    break;
                }
            }

            if (!isFindSameColor)
            {
                color = (EPlayerColor)i;
                break;
            }
        }
        playerColor = color;

        Vector3 spawnPos = FindObjectOfType<SpawnPositions>().GetSpawnPosition();

        var player = Instantiate(RoomManager.singleton.spawnPrefabs[0], spawnPos, Quaternion.identity).GetComponent<LobbyCharacterMove>();
        NetworkServer.Spawn(player.gameObject, connectionToClient);
        player.playerColor = color;
    }
}
