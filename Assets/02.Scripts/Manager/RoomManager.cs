using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

// Offline Scene(게임을 위한 네트워크에 접속하지 않은 씬)
// Online Scene (게임 네트워크에 접속한 후 실제 게임을 시작하기 전 게임 대기실 씬)
// Room Scene (플레이어들이 접속해서 게임이 준비되기를 기다리는 대기실 씬)
public class RoomManager : NetworkRoomManager
{
    // 서버에서 새로 접속한 클라이언트를 감지했을 때 동작
    public override void OnRoomServerConnect(NetworkConnectionToClient conn)
    {
        base.OnRoomServerConnect(conn);

        var player = Instantiate(spawnPrefabs[0]);
        NetworkServer.Spawn(player, conn);
    }
}
