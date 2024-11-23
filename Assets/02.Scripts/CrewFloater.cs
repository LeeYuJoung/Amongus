using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrewFloater : MonoBehaviour
{
    [SerializeField]
    private GameObject prefab;
    [SerializeField]
    private List<Sprite> sprites;

    private bool[] crewStates = new bool[12];
    private float timer = 0.5f;
    private float distance = 8.0f;

    private void Start()
    {
        for(int i = 0; i < crewStates.Length; i++)
        {
            SpawnFloatingCrew((EPlayerColor)i, Random.Range(0.0f, distance));
        }
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0.0f)
        {
            SpawnFloatingCrew((EPlayerColor)Random.Range(0, 12), distance);
            timer = 1.0f;
        }
    }

    public void SpawnFloatingCrew(EPlayerColor playerColor, float dist)
    {
        if (!crewStates[(int)playerColor])
        {
            crewStates[(int)playerColor] = true;

            float angle = Random.Range(0.0f, 360.0f);
            Vector3 spawnPos = new Vector3(Mathf.Sin(angle), Mathf.Cos(angle), 0.0f) * distance;
            Vector3 direction = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), 0.0f);
            float floatingSpeed = Random.Range(1.0f, 4.0f);
            float rotateSpeed = Random.Range(-3.0f, 3.0f);

            var crew = Instantiate(prefab, spawnPos, Quaternion.identity).GetComponent<FloatingCrew>();
            crew.SetFloatingCrew(sprites[Random.Range(0, sprites.Count)], playerColor, direction, floatingSpeed, rotateSpeed, Random.Range(0.5f, 1.0f));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var crew = collision.GetComponent<FloatingCrew>();

        if(crew != null)
        {
            crewStates[(int)crew.playerColor] = false;
            Destroy(crew.gameObject);
        }
    }
}