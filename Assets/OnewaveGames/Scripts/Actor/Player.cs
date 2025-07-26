using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Actor
{
    public Arm arm;

    [Header("Movement Settings")]
    public float rotationSpeed = 720f;

    // 입력 값 저장
    private Vector2 moveInput;
    private bool leftClickPressed;

    // 컴포넌트
    private Camera playerCamera;
    private bool isOnCooldown = false;


    public override void InitData()
    {
        // 컴포넌트 가져오기
        base.InitData();
        playerCamera = Camera.main;
    }

    void Update()
    {
        HandleMovement();
        HandleClicks();
    }

    void HandleMovement()
    {
        if (moveInput != Vector2.zero && arm.IsActive == false)
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
            characterController.Move(moveDirection * actorStatData.MoveSpeed * Time.deltaTime);

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
        moveInput = value.Get<Vector2>();
    }

    // public void OnLook(InputAction.CallbackContext context)
    // {
    //     // 카메라 회전 처리
    //     Debug.Log("OnLook action triggered");
    // }

    public void OnFire(InputValue value)
    {
        if (value.isPressed && isOnCooldown == false)
        {
            leftClickPressed = true;

        }
    }

    void PerformAttack()
    {
        // 마우스 위치로 레이캐스트
        Ray ray = playerCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (Physics.Raycast(ray, out RaycastHit hit, 100f))
        {
            Debug.Log($"클릭 좌표: {hit.point}");

            if (actorStatData.CurrentMana > arm.skillData.requireMana)
            {
                // 팔 스킬 적용
                ApplySkill(hit.point);
                StartCoroutine(ArmCooldownCoroutine());
            }
            else
            {
                Debug.Log("마나가 부족합니다.");
            }
        }
    }



    private IEnumerator ArmCooldownCoroutine()
    {
        isOnCooldown = true;
        var armCooldown = arm.skillData.cooldownTime;
        Debug.Log($"팔 쿨다운 시작 - {armCooldown}초");
        yield return new WaitForSeconds(armCooldown);

        isOnCooldown = false;
        Debug.Log($"팔 쿨다운 종료");
    }


    public void OnArmReached()
    {
        Debug.Log("팔이 플레이어에게 돌아왔습니다.");
    }



    public override void ApplySkill(Vector3 targetPosition)
    {
        // 잡기 스킬 사용
        SkillManager.Instance.UseThrowArmSkill(this, null, targetPosition);
    }
}
