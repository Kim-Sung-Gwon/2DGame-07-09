using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
//1. ���� ���۳�Ʈ 
//2.���� ��� �Һ��� 
//3. ���� ���� �� ���� 
public class PlayerController : MonoBehaviour
{
    [SerializeField] private AudioClip deathClip;
    [SerializeField] private Rigidbody2D rbody2D;
    [SerializeField] private Animator animator;
    [SerializeField] private AudioSource audioSource;
    private float jumpForce = 700f;//���� �� 
    private int jumpCount = 0; //���� ���� Ƚ�� 
    bool isDead = false; //��� ���� 
    bool isGrounded = false; //���� ��Ҵ� ��
    private readonly string Deadzone = "DeadZone";
    void Start()
    {
        rbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        deathClip = Resources.Load("die") as AudioClip;
    }
    void Update()
    {
        if (isDead) return;
        // ���콺 ���ʹ�ư�� �������� && �ִ� ���� Ƚ�� 2�� ���� ���� �ʾҰ�
        if (Input.GetMouseButtonDown(0) && jumpCount < 2)
        {
            jumpCount++; //���� Ƚ�� ���� 
            //���� ������ �ӵ��� ���������� ����(0,0)�� ����
            rbody2D.velocity = Vector3.zero;
            //������ �ٵ� �������� ���� �ְ� 
            rbody2D.AddForce(new Vector2(0f, jumpForce));
            audioSource.Play(); //����� ���
        }
        //Input.GetMouseButton()
        else if (Input.GetMouseButtonUp(0) && rbody2D.velocity.y > 0)
        {
            //���콺 ���ʹ�ư���� ���� ���� ���� && �ӵ��� y���� ������
            //������ �ӵ��� �������� ����
            rbody2D.velocity = rbody2D.velocity * 0.5f;
        }
        animator.SetBool("IsGrounded", isGrounded);
        //�ִϸ������� IsGrounded �Ķ���͸� ������Ʈ

    }
    void Die()
    {
        animator.SetTrigger("DieTrigger");
        audioSource.clip = deathClip;
        audioSource.Play();
        rbody2D.velocity = Vector2.zero;
        isDead = true;
        GameManager.Instance.OnPlayerDaed();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Ʈ���� �ݶ��̴��� ���� ��ֹ��� �浹�� ����
        if (other.CompareTag(Deadzone) && !isDead)
        {
            GameManager.Instance.isGameOver = true;
            Destroy(other.gameObject);
            Die();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //�ٴڿ� ����� �� ���� �ϴ� ó�� 
        // � �ݶ��̴��� �������  �浹ǥ���� ������ ���� ������
        if (collision.contacts[0].normal.y > 0.7f)
        {   //�ǥ���� �븻������ y���� 1.0�� ��� �ش�ǥ���� ������ ����
            isGrounded = true;
            jumpCount = 0;
        }

    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        //�ٴ��� ������� ���� �ϴ� ó�� 
        isGrounded = false;
    }
}