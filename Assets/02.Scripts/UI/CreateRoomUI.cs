using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class CreateRoomUI : MonoBehaviour
{
    [SerializeField]
    private List<Image> crewImages;

    [SerializeField]
    private List<Button> imposterCountButtons;

    [SerializeField]
    private List<Button> maxPlayerCountButtons;

    private CreateGameRoomData roomData;

    void Start()
    {
        for(int i = 0; i < crewImages.Count; i++)
        {
            // 크루원 Material을 개별로 생성해주어 각 크루원 색상 변경 시 모든 크루원의 색상이 전부 바뀌는 현상 방지
            Material materialInstance = Instantiate(crewImages[i].material);
            crewImages[i].material = materialInstance;
        }

        // roomData 생성 후 초기화
        roomData = new CreateGameRoomData() { imposterCount = 1, maxPlayerCount = 10 };
        UpdateCrewImages();
    }

    // 최대 임포스터 수 선택
    public void UpdateImposterCount(int count)
    {
        roomData.imposterCount = count;

        for (int i = 0; i < imposterCountButtons.Count; i++)
        {
            if (i == count - 1)
            {
                imposterCountButtons[i].image.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            }
            else
            {
                imposterCountButtons[i].image.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
            }
        }

        // 임포스터의 수가 1이 아닐 경우 플레이어 수 지정 제한
        int limitMaxPlayer = (count == 1) ? 4 : (count == 2) ? 7 : 9;

        if(roomData.maxPlayerCount < limitMaxPlayer)
        {
            UpdateMaxPlayerCount(limitMaxPlayer);
        }
        else
        {
            UpdateMaxPlayerCount(roomData.maxPlayerCount);
        }

        // limitMaxPlayer 보다 작은 플레이어 버튼은 선택할 수 없도록 비활성화
        for(int i = 0; i < maxPlayerCountButtons.Count; i++)
        {
            var text = maxPlayerCountButtons[i].GetComponentInChildren<Text>();

            if(i < limitMaxPlayer - 4)
            {
                maxPlayerCountButtons[i].interactable = false;
                text.color = Color.gray;
            }
            else
            {
                maxPlayerCountButtons[i].interactable = true;
                text.color = Color.white;
            }
        }

        UpdateCrewImages();
    }

    // 최대 인원 수 선택
    public void UpdateMaxPlayerCount(int count)
    {
        roomData.maxPlayerCount = count;

        for(int i = 0; i < maxPlayerCountButtons.Count; i++)
        {
            if(i == count - 4)
            {
                maxPlayerCountButtons[i].image.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            }
            else
            {
                maxPlayerCountButtons[i].image.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
            }
        }

        UpdateCrewImages();
    }

    // 크루원 이미지 변경
    private void UpdateCrewImages()
    {
        for(int i = 0; i < crewImages.Count; i++)
        {
            crewImages[i].material.SetColor("_PlayerColor", Color.white);
        }

        int imposterCount = roomData.imposterCount;
        int idx = 0;

        while(imposterCount != 0)
        {
            if(idx >= roomData.maxPlayerCount)
            {
                idx = 0;
            }

            if (crewImages[idx].material.GetColor("_PlayerColor") != Color.red && Random.Range(0, 5) == 0)
            {
                crewImages[idx].material.SetColor("_PlayerColor", Color.red);
                imposterCount--;
            }
            idx++;
        }

        for(int i = 0; i < crewImages.Count; i++)
        {
            if(i < roomData.maxPlayerCount)
            {
                crewImages[i].gameObject.SetActive(true);
            }
            else
            {
                crewImages[i].gameObject.SetActive(false);
            }
        }
    }

    // 확인 Button 클릭 시 게임 실행
    public void CreateRoom()
    {
        // 방 설정 작업 처리
        // 서버를 여는 동시에 Client로써 게임에 참가하도록 만들어주는 함수
        var manager = RoomManager.singleton;
        manager.StartHost();
    }
}

// 새로 만드는 방의 데이터 저장 및 방 생성 완료 후 새로 생성된 방에 전달
public class CreateGameRoomData
{
    public int imposterCount;
    public int maxPlayerCount;
}