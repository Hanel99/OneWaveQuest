using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Actor
{

    [Header("스킬 데이터(플레이어 스탯)")]
    public ArmSkillData skillData;


    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float rotationSpeed = 720f;
    public int AttackPower = 10;

    // 입력 값 저장
    private Vector2 moveInput;
    private bool leftClickPressed;

    // 컴포넌트
    private Camera playerCamera;

    void Start()
    {
        // 컴포넌트 가져오기
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
        moveInput = value.Get<Vector2>();

        // if (moveInput.y > 0)
        //     Debug.Log("위쪽 입력");
        // else if (moveInput.y < 0)
        //     Debug.Log("아래쪽 입력");

        // if (moveInput.x > 0)
        //     Debug.Log("오른쪽 입력");
        // else if (moveInput.x < 0)
        //     Debug.Log("왼쪽 입력");
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
            Debug.Log($"클릭 좌표: {hit.point}");
            Debug.Log($"공격 대상: {hit.collider.name}");

            ApplySkill(hit.point);

            // 공격 대상 처리
            // if (hit.collider.CompareTag("Enemy"))
            // {
            //     Debug.Log("Enemy hit detected");
            //     ApplySkill(hit.collider.transform.parent.GetComponent<Actor>());
            // }

        }
    }



    public override void ApplySkill(Vector3 targetPosition)
    {
        // 잡기 스킬 사용
        var throwArmSkill = new ThrowArmSkill();
        throwArmSkill.SetEffectList(this, null, targetPosition);
        if (throwArmSkill.ApplySkill(this, null, targetPosition))
        {
            Debug.Log("ThrowArmSkill applied successfully.");
        }
        else
        {
            Debug.LogWarning("Failed to apply ThrowArmSkill.");
        }
    }
}
