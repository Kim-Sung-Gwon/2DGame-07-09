using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingOject : MonoBehaviour
{
    public float Speed = 10f;
    void Update()
    {
        if (GameManager.Instance.isGameOver == false)
        transform.Translate(Vector3.left * Speed * Time.deltaTime);
    }
}
