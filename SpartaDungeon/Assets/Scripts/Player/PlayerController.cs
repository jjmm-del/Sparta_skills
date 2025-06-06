using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float walkSpeed;
    public float runSpeed;
    private Vector2 curMovementInput;
    public float jumpPower;
    public LayerMask groundLayerMask;
    private float moveSpeed;
    
    
    [Header("Look")]
    public Transform cameraContainer;
    public float minXLook;
    public float maxXLook;
    private float camCurXRot;
    public float lookSensitivity;

    [Header("Camera Setup")]
    [SerializeField] private Camera mainCamera = null;
    [SerializeField] private Camera handCamera = null;
    [SerializeField] private Camera tpsCamera = null;
    
    [Header("Tps Offset")]
    [SerializeField] private float tpsDistance = 4f;
    [SerializeField] private float tpsHeight = 2f;
    
    [Header("Sit Scale")]
    [SerializeField] private float scaleFactor = 0.5f;
    [SerializeField] private Transform handTransform = null;

    private Vector3 OriginalPlayerScale;
    private Vector3 OriginalHandScale;
    private bool isSitted = false;
        
        
        
        
        
    private bool isThirdPerson = false;
    
    
    
    
    [SerializeField] private Animator handAnimator = null;
    
    private Vector2 mouseDelta;

    [HideInInspector]
    public bool canLook = true;
    public bool canFire = false;
    public GameObject fireProjectilePrefab;
    public float projectileSpeed;
    public float projectileLifeTime;
        

    private Rigidbody rb;
    public ItemPanel itempanel;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        OriginalPlayerScale = transform.localScale;
        OriginalHandScale = handTransform.localScale; 
        
        moveSpeed = walkSpeed;
        
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

            bool moving = curMovementInput.sqrMagnitude > 0.01f;
            handAnimator.SetBool("Moving", moving);
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            curMovementInput = Vector2.zero;
            handAnimator.SetBool("Moving", false);
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
            if (canFire && fireProjectilePrefab != null)
            {
                FireProjetile();
            }
        }
    }
    
    public void OnSitInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed && IsGrounded())
        {
            StartSitting();
            handAnimator.SetBool("Sit", true);
        }

        if (context.phase == InputActionPhase.Performed && !IsGrounded())
        {
            //공중에서 앉기 -> 내려 찍기
        }
        
        else if (context.phase == InputActionPhase.Canceled)
        {
            StopSitting();
            handAnimator.SetBool("Sit", false);
        }
    }
    public void OnRunInput(InputAction.CallbackContext context)
    {
        
        if (context.phase == InputActionPhase.Performed && IsGrounded())
        {
            moveSpeed = runSpeed;
            handAnimator.SetInteger("MoveSpeed", (int)moveSpeed);
            
        }
        if (context.phase == InputActionPhase.Canceled)
        {
            moveSpeed = walkSpeed;
            handAnimator.SetInteger("MoveSpeed", (int)moveSpeed);
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
    public void OnSwitchView(InputAction.CallbackContext context)
    {
        if (context.phase != InputActionPhase.Started) return;
        
            isThirdPerson = !isThirdPerson;
            mainCamera.enabled = !isThirdPerson;
            handCamera.enabled = !isThirdPerson;
            tpsCamera.enabled = isThirdPerson;
        
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

    private void FireProjetile()
    {
        Transform cam = this.cameraContainer;
        Vector3 spawnPos = cam.position + cam.forward * 1.0f;
        GameObject projctile = Instantiate(fireProjectilePrefab,spawnPos,Quaternion.identity);
        Rigidbody rb = projctile.GetComponent<Rigidbody>();
        rb.velocity = cam.forward * projectileSpeed;
        Object.Destroy(projctile, projectileLifeTime);
        
    }

    public void StartSitting()
    {
        if (isSitted == true)
        {
            return;
        }
        isSitted = true;
        
        Vector3 newPlayerScale = new Vector3(
            OriginalPlayerScale.x,
            OriginalPlayerScale.y * scaleFactor,
            OriginalPlayerScale.z
            );
        transform.localScale = newPlayerScale;

        Vector3 newHandScale = new Vector3(
            OriginalHandScale.x,
            OriginalHandScale.y / scaleFactor,
            OriginalHandScale.z);
        handTransform.localScale = newHandScale;
    }

    public void StopSitting()
    {
        if (isSitted == false)
            return;
        
        isSitted = false;
        transform.localScale = OriginalPlayerScale;
        handTransform.localScale = OriginalHandScale;


    }

    public void ToggleCursor(bool toggle)
    {
        Cursor.lockState = toggle ? CursorLockMode.None : CursorLockMode.Locked;
        canLook = !toggle;
    }

    
}
