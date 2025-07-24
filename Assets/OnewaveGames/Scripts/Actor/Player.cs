using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Actor
{

    [Header("스킬 데이터")]
    public ArmSkillData skillData;


    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float rotationSpeed = 720f;
    public int AttackPower = 10;

    // 입력 값 저장
    private Vector2 moveInput;
    private bool leftClickPressed;

    // 컴포넌트
    private CharacterController characterController;
    private Camera playerCamera;

    void Start()
    {
        // 컴포넌트 가져오기
        characterController = GetComponent<CharacterController>();
        playerCamera = Camera.main;
    }

    void Update()
    {
        HandleMovement();
        HandleClicks();
    }

    void HandleMovement()
    {
        if (moveInput != Vector2.zero)
        {
            // 카메라 기준으로 이동 방향 계산
            Vector3 cameraForward = playerCamera.transform.forward;
            Vector3 cameraRight = playerCamera.transform.right;

            // Y축 제거 (수평 이동만)
            cameraForward.y = 0;
            cameraRight.y = 0;
            cameraForward.Normalize();
            cameraRight.Normalize();

            // 최종 이동 방향
            Vector3 moveDirection = cameraForward * moveInput.y + cameraRight * moveInput.x;

            // 캐릭터 이동
            characterController.Move(moveDirection * moveSpeed * Time.deltaTime);

            // 캐릭터 회전
            if (moveDirection != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
        }
    }

    void HandleClicks()
    {
        if (leftClickPressed)
        {
            leftClickPressed = false; // 한 번만 실행되도록
            PerformAttack();
        }
    }

    // Input System 이벤트 함수들
    public void OnMove(InputValue value)
    {
        Debug.Log("OnMove2 action triggered");
        moveInput = value.Get<Vector2>();
    }

    // public void OnLook(InputAction.CallbackContext context)
    // {
    //     // 카메라 회전 처리
    //     Debug.Log("OnLook action triggered");
    // }

    public void OnFire(InputValue value)
    {
        if (value.isPressed)
        {
            leftClickPressed = true;
        }
    }





    void PerformAttack()
    {
        Debug.Log("left click pressed, performing attack");

        // 마우스 위치로 레이캐스트
        Ray ray = playerCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out RaycastHit hit, 100f))
        {
            Debug.Log($"공격 대상: {hit.collider.name}");

            // 공격 대상 처리
            if (hit.collider.CompareTag("Enemy"))
            {
                Debug.Log("Enemy hit detected");
                ApplySkill(hit.collider.GetComponent<Actor>());
            }

            // 공격 이펙트 생성
            CreateAttackEffect(hit.point);
        }
    }

    void CreateAttackEffect(Vector3 position)
    {
        // 공격 이펙트 파티클이나 이펙트 생성
        // 예시: 파티클 시스템 재생
        Debug.Log($"공격 이펙트 생성 at {position}");
    }


    public override void ApplySkill(Actor target)
    {
        if (target is Enemy enemy)
        {
            if (enemy != null)
            {
                enemy.Grab();
            }
        }
    }
}
