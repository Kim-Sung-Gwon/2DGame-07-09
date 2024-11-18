using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatForm : MonoBehaviour
{
    public GameObject[] obstacles;  // ���ع� ������Ʈ
    private bool stepped = false;   // �÷��̾� ĳ���Ͱ� ��Ҵ� ��? 
    private string playerTag = "Player";
    void OnEnable()  // Awake() -> OnEnable() -> start() ������ ȣ��
    {   // ���۳�Ʈ�� Ȱ��ȭ �� ������ �Ź� ����Ǵ� �޼���
        // ������ ���� ó��
        stepped = false; // �ٽ� �������� ����ؼ� false ó��
        // ���ع��� ����ŭ ����
        for (int i = 0; i < obstacles.Length; i++)
        {
            // ���� ������ ���ع��� 1/3�� Ȯ���� Ȱ��ȭ
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
    {   // �÷��̾� ĳ���Ͱ� �ڽ��� ���� �� ������ �߰��ϴ� ó��
        if (collision.collider.CompareTag(playerTag) && !stepped)
        {
            stepped = true;
            GameManager.Instance.AddScore(1);
        }
    }
    private void OnDisable()
    {
        // ������Ʈ�� ��Ȱ��ȭ �ɶ� ���� ȣ��
    }
}
