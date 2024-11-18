using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatFromSpawner : MonoBehaviour
{
    public GameObject platformPrefab;
    public int count = 3;

    public float timeBestSpawnMin = 1.25f; // ���� ��ġ���� �ּ� �ð�
    public float timeBestSpawnMax = 2.25f; // �ִ� �ð�
    private float timeBestSpawn;

    public float yMin = -3.5f;
    public float yMax = 1.5f;
    private float xPos = 20f;
    
    private GameObject[] platforms;
    private int courrentIndex = 0;

    private Vector2 poolPosition = new Vector2(0, -25f);
    private float lastSpawnTime;
    // Start is called before the first frame update
    void Start()
    {
        platforms = new GameObject[count];

        for (int i = 0; i < count; i++)
        {// ������ ������ ��ġ���� ���� ���� ������ ������ �迭�� �Ҵ�
            platforms[i] = Instantiate(platformPrefab, poolPosition, Quaternion.identity);
        }

        lastSpawnTime = 0f; // ������ ��ġ ���� �ʱ�ȭ
        timeBestSpawn = 0f; // ���� ��ġ ���� �ʱ�ȭ
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.isGameOver)
            return;

        if (Time.time >= lastSpawnTime + timeBestSpawn)
        {
            lastSpawnTime = Time.time;
            timeBestSpawn = Random.Range(timeBestSpawnMin, timeBestSpawnMax);
            float yPos = Random.Range(yMin, yMin);
            platforms[courrentIndex].SetActive(false);
            platforms[courrentIndex].SetActive(true);
            platforms[courrentIndex].transform.position = new Vector2(xPos, yPos);
            courrentIndex++;
        }

        if (courrentIndex >= count)
        {
            courrentIndex = 0;
        }
    }
}
