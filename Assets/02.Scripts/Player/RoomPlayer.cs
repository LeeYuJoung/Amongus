using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

// Room Player Prefab : 게임이 시작되기 전 게임 대기실(Game Room Scene)에서 플레이어가 서버와 상호작용하기 위한 것
public class RoomPlayer : NetworkRoomPlayer
{
    private static RoomPlayer myRoomPlayer;
    public static RoomPlayer MyRoomPlayer
    {
        get
        {
            if(myRoomPlayer == null)
            {
                var players = FindObjectsOfType<RoomPlayer>();

                foreach(var player in players)
                {
                    if (player.isOwned)
                    {
                        myRoomPlayer = player;
                    }
                }
            }

            return myRoomPlayer;
        }
    }

    [SyncVar(hook = nameof(SetPLayerColor_Hook))]
    public EPlayerColor playerColor;

    public void SetPLayerColor_Hook(EPlayerColor oldColor, EPlayerColor newColor)
    {
        LobbyUIManager.Instance.CustomizeUI.UpdateColorButton();
    }

    public CharacterMove lobbyPlayerCharacter;

    public void Start()
    {
        base.Start();

        if (isServer)
        {
            SpawnLobbyPlayerCharacter();
        }
    }

    [Command]
    // Client에서 함수를 호출하면 함수 내부의 동작이 서버에서 실행되도록 함
    public void CmdSetPlayerColor(EPlayerColor color)
    {
        playerColor = color;
        lobbyPlayerCharacter.playerColor = color;
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

        var playerCharacter = Instantiate(RoomManager.singleton.spawnPrefabs[0], spawnPos, Quaternion.identity).GetComponent<LobbyCharacterMove>();
        NetworkServer.Spawn(playerCharacter.gameObject, connectionToClient);
        playerCharacter.ownerNetId = netId;
        playerCharacter.playerColor = color;
    }
}
