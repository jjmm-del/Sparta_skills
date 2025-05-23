using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TPSCameraCotroller : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform target;      // 플레이어(또는 어깨 높이) Transform
    [SerializeField] private PlayerInput playerInput; // PlayerInput 컴포넌트 참조

    [Header("Offset")]
    [SerializeField] private float distance = 4f;   // 뒤로 떨어질 거리
    [SerializeField] private float height   = 2f;   // 위로 올라갈 높이

    [Header("Rotation")]
    [SerializeField] private float rotationSpeed = 120f;

    private float yaw   = 0f;
    private float pitch = 10f;

    private InputAction lookAction;

    private void OnEnable()
    {
        lookAction = playerInput.actions["Look"];
        lookAction.performed += OnLook;
        lookAction.canceled  += OnLook;
    }

    private void OnDisable()
    {
        lookAction.performed -= OnLook;
        lookAction.canceled  -= OnLook;
    }

    // Input System의 "Look" 액션이 호출하는 메서드
    private void OnLook(InputAction.CallbackContext ctx)
    {
        Vector2 delta = ctx.ReadValue<Vector2>();
        yaw   += delta.x * rotationSpeed * Time.deltaTime;
        pitch -= delta.y * rotationSpeed * Time.deltaTime;
        pitch  = Mathf.Clamp(pitch, -20f, 60f);
    }

    private void LateUpdate()
    {
        // ① 카메라 회전 계산
        Quaternion rot = Quaternion.Euler(pitch, yaw, 0f);
        // ② 목표 위치에서 offset만큼 뒤 & 위로
        Vector3 offset = rot * new Vector3(0f, height, -distance);
        transform.position = target.position + offset;
        // ③ 대상을 바라보도록
        transform.rotation = rot;
        transform.LookAt(target.position + Vector3.up * (height * 0.5f));
    }
}
