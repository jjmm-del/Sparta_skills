using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    private Vector2 curMovementInput;
    public float jumpPower;
    public LayerMask groundLayerMask;
    
    [Header("Look")]
    public Transform cameraContainer;
    public float minXLook;
    public float maxXLook;
    private float camCurXRot;
    public float lookSensitivity;

    private Vector2 mouseDelta;

    [HideInInspector] public bool canLook = true;

    private Rigidbody rb;
    public ItemPanel itempanel;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void LateUpdate()
    {
        if (canLook)
        {
            CameraLook();
        }
    }

    public void OnLookInput(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            curMovementInput = context.ReadValue<Vector2>();
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            curMovementInput = Vector2.zero;
        }
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && IsGrounded())
        {
            rb.AddForce(Vector2.up * jumpPower, ForceMode.Impulse);
        }
    }
    
    public void OnAttackInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            //공격
        }
    }
    
    public void OnSitInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed && IsGrounded())
        {
            //땅에서 앉기 -> 앉은 상태
        }

        if (context.phase == InputActionPhase.Performed && !IsGrounded())
        {
            //공중에서 앉기 -> 내려 찍기
        }
        
        else if (context.phase == InputActionPhase.Canceled)
        {
            //다시 일어선 상태
        }
    }
    public void OnRunInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed && !IsGrounded())
        {
            //달리기 
        }
    }

    public void OnUse1Input(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started) itempanel.UseItem(0);
    }
    public void OnUse2Input(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started) itempanel.UseItem(1);
    }
    public void OnUse3Input(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started) itempanel.UseItem(2);
    }


    private void Move()
    {
        Vector3 direction = transform.forward * curMovementInput.y + transform.right * curMovementInput.x;
        direction *= moveSpeed;
        direction.y = rb.velocity.y;
        
        rb.velocity = direction;
    }
    private void CameraLook()
    {
        camCurXRot += mouseDelta.y * lookSensitivity;
        camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook);
        cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0.0f, 0.0f);
        
        transform.eulerAngles += new Vector3(0.0f, mouseDelta.x * lookSensitivity, 0.0f);
    }

    bool IsGrounded()
    {
        Ray[] rays = new Ray[4]
        {
            new Ray(transform.position + (transform.forward * 0.2f) + (transform.up * 0.05f), Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.2f) + (transform.up * 0.05f), Vector3.down),
            new Ray(transform.position + (transform.right * 0.2f) + (transform.up * 0.05f), Vector3.down),
            new Ray(transform.position + (-transform.right * 0.2f) + (transform.up * 0.05f), Vector3.down)
        };

        for (int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i], 0.2f, groundLayerMask))
            {
                return true;
            }
        }
        return false;
    }

    public void ToggleCursor(bool toggle)
    {
        Cursor.lockState = toggle ? CursorLockMode.None : CursorLockMode.Locked;
        canLook = !toggle;
    }
}
