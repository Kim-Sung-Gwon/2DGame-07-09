using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
//1. 각종 컴퍼넌트 
//2.점프 사망 불변수 
//3. 점프 가능 한 범위 
public class PlayerController : MonoBehaviour
{
    [SerializeField] private AudioClip deathClip;
    [SerializeField] private Rigidbody2D rbody2D;
    [SerializeField] private Animator animator;
    [SerializeField] private AudioSource audioSource;
    private float jumpForce = 700f;//점프 힘 
    private int jumpCount = 0; //누적 점프 횟수 
    bool isDead = false; //사망 여부 
    bool isGrounded = false; //땅에 닿았는 지
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
        // 마우스 왼쪽버튼을 눌렀으며 && 최대 점프 횟수 2에 도달 하지 않았고
        if (Input.GetMouseButtonDown(0) && jumpCount < 2)
        {
            jumpCount++; //점프 횟수 증가 
            //점프 직전에 속도를 순간적으로 제로(0,0)로 변경
            rbody2D.velocity = Vector3.zero;
            //리지디 바디에 위쪽으로 힘을 주고 
            rbody2D.AddForce(new Vector2(0f, jumpForce));
            audioSource.Play(); //오디오 재생
        }
        //Input.GetMouseButton()
        else if (Input.GetMouseButtonUp(0) && rbody2D.velocity.y > 0)
        {
            //마우스 왼쪽버튼에서 손을 떼는 순간 && 속도의 y값이 양수라면
            //현재의 속도를 절반으로 변경
            rbody2D.velocity = rbody2D.velocity * 0.5f;
        }
        animator.SetBool("IsGrounded", isGrounded);
        //애니메이터의 IsGrounded 파라미터를 업데이트

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
        //트리거 콜라이더를 가진 장애물과 충돌을 감지
        if (other.CompareTag(Deadzone) && !isDead)
        {
            GameManager.Instance.isGameOver = true;
            Destroy(other.gameObject);
            Die();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //바닥에 닿았음 을 감지 하는 처리 
        // 어떤 콜라이더와 닿았으며  충돌표면이 위쪽을 보고 있으면
        if (collision.contacts[0].normal.y > 0.7f)
        {   //어떤표면의 노말벡터의 y값이 1.0인 경우 해당표면의 방향은 위쪽
            isGrounded = true;
            jumpCount = 0;
        }

    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        //바닥을 벗어났음을 감지 하는 처리 
        isGrounded = false;
    }
}