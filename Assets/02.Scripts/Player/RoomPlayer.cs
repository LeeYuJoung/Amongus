using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

// Room Player Prefab : ������ ���۵Ǳ� �� ���� ����(Game Room Scene)���� �÷��̾ ������ ��ȣ�ۿ��ϱ� ���� ��
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
    // Client���� �Լ��� ȣ���ϸ� �Լ� ������ ������ �������� ����ǵ��� ��
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
