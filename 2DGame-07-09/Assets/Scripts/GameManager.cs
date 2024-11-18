using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public bool isGameOver = false;
    [SerializeField] private Text ScoreText;
    [SerializeField] private GameObject gameUI;
    public int score = 0;
    void Awake()
    { // ��ŸƮ �Լ����� ���� ȣ��Ŵ��� Ŭ������ �ݵ�� Awake
        if (Instance == null)  // �ν��Ͻ��� ���̸�
            Instance = this;   // ���� �Ҵ�
        else if (Instance != this)  // ���� �ν��Ͻ��� �ڽŰ� ���������� �ı�
            Destroy(gameObject);
        //DontDestroyOnLoad(gameObject);
        // ���������� �Ѿ���� ���� �Ŵ��� ������Ʈ�� ���������ʴ´�.
        ScoreText = GameObject.Find("Canvas-UI").transform.GetChild(1).GetComponent<Text>();
    }
    void Update()
    {
        // ���� ���� ������ �����̰� ���콺 �ܴ��� ��ư�� �����ٸ�
        if (isGameOver && Input.GetMouseButtonDown(0))
        {
            //gameObject.SetActive(true);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            // ���� ���� �����
        }
    }
    public void AddScore(int newScore)
    {
        score += newScore;
        ScoreText.text = $"Score : {score.ToString()}";
    }
    public void OnPlayerDaed()
    {
        isGameOver = true;
        gameUI.gameObject.SetActive(true);
    }
}
