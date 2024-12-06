using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

// Offline Scene(������ ���� ��Ʈ��ũ�� �������� ���� ��)
// Online Scene (���� ��Ʈ��ũ�� ������ �� ���� ������ �����ϱ� �� ���� ���� ��)
// Room Scene (�÷��̾���� �����ؼ� ������ �غ�Ǳ⸦ ��ٸ��� ���� ��)
public class RoomManager : NetworkRoomManager
{
    // �������� ���� ������ Ŭ���̾�Ʈ�� �������� �� ����
    public override void OnRoomServerConnect(NetworkConnectionToClient conn)
    {
        base.OnRoomServerConnect(conn);

        var player = Instantiate(spawnPrefabs[0]);
        NetworkServer.Spawn(player, conn);
    }
}
