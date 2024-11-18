using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundLoop : MonoBehaviour
{
    private float width;
    //void Awake() //����Ƽ �̺�Ʈ �Լ��� ���� ���� ȣ���  Awake ->Start �Լ� ȣ��
    IEnumerator Start()
    {
        BoxCollider2D boxCollider2D = GetComponent<BoxCollider2D>();
        width = boxCollider2D.size.x;
        yield return null;
        StartCoroutine(BackGroundMove());
    }
    IEnumerator BackGroundMove()
    {
        while (!GameManager.Instance.isGameOver)
        {
            yield return new WaitForSeconds(0.02f);
            if (transform.position.x <= -width)
            {
                RePosition();
            }
        }
    }
    void RePosition()
    {
        Vector2 offset = new Vector2(width * 2, 0);
        transform.position = (Vector2)transform.position + offset;
    }
}
