using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
            Material materialInstance = Instantiate(crewImages[i].material);
            crewImages[i].material = materialInstance;
        }

        roomData = new CreateGameRoomData() { imposterCount = 1, maxPlayerCount = 10 };
        UpdateCrewImages();
    }

    // 최대 임포스터 수 선택
    public void UpdateImposterCount(int count)
    {
        roomData.imposterCount = count;

        for (int i = 0; i < imposterCountButtons.Count; i++)
        {
            if (i == count - 4)
            {
                imposterCountButtons[i].image.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            }
            else
            {
                imposterCountButtons[i].image.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
            }
        }

        // 임포스터의 수가 1이 아닐 경우 플레이어 수 제한
        int limitMaxPlayer = count == 1 ? 4 : (count == 2) ? 7 : 9;

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
            var text = maxPlayerCountButtons[i].GetComponent<Text>();

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
}

// 새로 만드는 방의 데이터 저장 및 방 생성 완료 후 새로 생성된 방에 전달
public class CreateGameRoomData
{
    public int imposterCount;
    public int maxPlayerCount;
}
