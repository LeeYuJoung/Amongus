using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomizeUI : MonoBehaviour
{
    [SerializeField]
    private Image characterPreview;

    [SerializeField]
    private List<ColorSelectButton> colorSelectButtons;

    void Start()
    {
        var inst = Instantiate(characterPreview.material);
        characterPreview.material = inst;
    }

    private void OnEnable()
    {
        UpdateColorButton();

        var roomSlots = (NetworkManager.singleton as RoomManager).roomSlots;

        foreach (var player in roomSlots)
        {
            var aPlayer = player as RoomPlayer;

            if (aPlayer.isLocalPlayer)
            {
                UpdatePreviewColor(aPlayer.playerColor);
                break;
            }
        }
    }

    public void UpdateColorButton()
    {
        var roomSlots = (NetworkManager.singleton as RoomManager).roomSlots;

        for(int i = 0; i < colorSelectButtons.Count; i++)
        {
            colorSelectButtons[i].SetInteractable(true);
        }

        foreach(var player in roomSlots)
        {
            var aPlayer = player as RoomPlayer;

            colorSelectButtons[(int)aPlayer.playerColor].SetInteractable(false);
        }
    }

    public void UpdatePreviewColor(EPlayerColor color)
    {
        characterPreview.material.SetColor("_PlayerColor", PlayerColor.GetColor(color));
    }

    // 색상 버튼을 클릭 했을 시 호출
    public void OnClickColorButton(int index)
    {
        if (colorSelectButtons[index].isInteractable)
        {
            RoomPlayer.MyRoomPlayer.CmdSetPlayerColor((EPlayerColor)index);
            UpdatePreviewColor((EPlayerColor)index);
        }
    }

    public void Open()
    {
        RoomPlayer.MyRoomPlayer.lobbyPlayerCharacter.isMoveable = false;
        gameObject.SetActive(true);
    }

    public void Close()
    {
        RoomPlayer.MyRoomPlayer.lobbyPlayerCharacter.isMoveable = true;
        gameObject.SetActive(false);
    }
}
