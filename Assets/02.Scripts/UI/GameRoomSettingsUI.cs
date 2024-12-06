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
            // �߰� ���� �ʿ� ��� => Host�� ������ ��� �ٸ� �÷��̾ �ޱ��� �ʱ� ���� Host ������ �Ѱ��ִ� ���̱׷��̼� ��� �߰�
            manager.StopHost();
        }
        else if(manager.mode == Mirror.NetworkManagerMode.ClientOnly)
        {
            manager.StopClient();
        }
    }
}