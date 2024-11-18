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
    { // 스타트 함수보다 먼저 호출매니저 클래스는 반드시 Awake
        if (Instance == null)  // 인스턴스가 널이면
            Instance = this;   // 동적 할당
        else if (Instance != this)  // 현재 인스턴스가 자신과 같지않으면 파괴
            Destroy(gameObject);
        //DontDestroyOnLoad(gameObject);
        // 다음씬으로 넘어가더라도 게임 매니저 오브젝트는 없어지지않는다.
        ScoreText = GameObject.Find("Canvas-UI").transform.GetChild(1).GetComponent<Text>();
    }
    void Update()
    {
        // 현재 게임 오버인 생태이고 마우스 외니쪽 버튼을 누른다면
        if (isGameOver && Input.GetMouseButtonDown(0))
        {
            //gameObject.SetActive(true);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            // 현재 씬을 재시작
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
