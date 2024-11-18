using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatForm : MonoBehaviour
{
    public GameObject[] obstacles;  // 장해물 오브젝트
    private bool stepped = false;   // 플래이어 캐릭터가 밟았는 가? 
    private string playerTag = "Player";
    void OnEnable()  // Awake() -> OnEnable() -> start() 순으로 호출
    {   // 컴퍼넌트가 활성화 될 때마다 매번 실행되는 메서드
        // 발판을 리셋 처리
        stepped = false; // 다시 켜질때를 대비해서 false 처리
        // 장해물의 수만큼 루프
        for (int i = 0; i < obstacles.Length; i++)
        {
            // 현재 순번의 장해물을 1/3의 확율로 활성화
            if (Random.Range(0, 3) == 0)
            {
                obstacles[i].SetActive(true);
            }
            else
            {
                obstacles[i].SetActive(false);
            }
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {   // 플레이어 캐릭터가 자신을 밟을 때 점수를 추가하는 처리
        if (collision.collider.CompareTag(playerTag) && !stepped)
        {
            stepped = true;
            GameManager.Instance.AddScore(1);
        }
    }
    private void OnDisable()
    {
        // 오브젝트가 비활성화 될때 마다 호출
    }
}
