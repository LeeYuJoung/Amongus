using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
using Mirror.BouncyCastle.Asn1.Mozilla;

public class OnlineUI : MonoBehaviour
{
    [SerializeField]
    private InputField nicknameInputField;
    [SerializeField]
    private GameObject createRoomUI;

    // 게임 방 생성
    public void OnClickCreateRoomButton()
    {
        if(nicknameInputField.text != "")
        {
            PlayerSettings.nickname = nicknameInputField.text;

            createRoomUI.SetActive(true);
            gameObject.SetActive(false);
        }
        else
        {
            nicknameInputField.GetComponent<Animator>().SetTrigger("On");
        }
    }

    // 이미 생성 된 방에 참가
    public void OnClickEnterGameRoomButton()
    {
        if (nicknameInputField.text != "")
        {
            // 닉네임 저장 확인하기

            var manager = RoomManager.singleton;
            manager.StartClient();
        }
        else
        {
            nicknameInputField.GetComponent<Animator>().SetTrigger("On");
        }
    }
}
