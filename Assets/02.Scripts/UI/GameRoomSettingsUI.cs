using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRoomSettingsUI : SettingsUI
{
    public void ExitGameRoom()
    {
        var manager = RoomManager.singleton;

        if (manager.mode == Mirror.NetworkManagerMode.Host)
        {
            // 추가 구현 필요 요소 => Host가 나갔을 경우 다른 플레이어가 뜅기지 않기 위해 Host 권한을 넘겨주는 마이그레이션 기능 추가
            manager.StopHost();
        }
        else if(manager.mode == Mirror.NetworkManagerMode.ClientOnly)
        {
            manager.StopClient();
        }
    }
}