using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Unity.VisualScripting;

public class CharacterMove : NetworkBehaviour
{
    public bool isMoveable;

    [SyncVar]
    public float speed = 2.0f;

    void Start()
    {
        // 카메라를 Client가 소유한 캐릭터에 붙이도록 함
        if (isOwned)
        {
            Camera cam = Camera.main;
            cam.transform.SetParent(transform);
            cam.transform.localPosition = new Vector3(0.0f, 0.0f, -10.0f);
            cam.orthographicSize = 2.5f;
        }
    }

    void FixedUpdate()
    {
        Move();
    }

    public void Move()
    {
        // hasAuthority 버전 업그레이드로 인한 변경 => isOwned
        if (isOwned && isMoveable)
        {
            if(PlayerSettings.controlType == EControlType.KeyboaedMouse)
            {
                Vector3 dir = Vector3.ClampMagnitude(new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f), 1.0f);

                if(dir.x < 0.0f)
                {
                    transform.localScale = new Vector3(-0.5f, 0.5f, 1.0f);
                }
                else if(dir.x > 0.0f)
                {
                    transform.localScale = new Vector3(0.5f, 0.5f, 1.0f);
                }

                transform.position += dir * speed * Time.deltaTime;
            }
            else
            {
                if(Input.GetMouseButton(0))
                {
                    Vector3 dir = (Input.mousePosition - new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0.0f)).normalized;

                    if (dir.x < 0.0f)
                    {
                        transform.localScale = new Vector3(-0.5f, 0.5f, 1.0f);
                    }
                    else if (dir.x > 0.0f)
                    {
                        transform.localScale = new Vector3(0.5f, 0.5f, 1.0f);
                    }

                    transform.position += dir * speed * Time.deltaTime;
                }
            }
        }
    }
}
