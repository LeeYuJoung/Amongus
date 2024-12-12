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

    // 霸烙 规 积己
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

    // 捞固 积己 等 规俊 曼啊
    public void OnClickEnterGameRoomButton()
    {
        if (nicknameInputField.text != "")
        {
            var manager = RoomManager.singleton;
            manager.StartClient();
        }
        else
        {
            nicknameInputField.GetComponent<Animator>().SetTrigger("On");
        }
    }
}
