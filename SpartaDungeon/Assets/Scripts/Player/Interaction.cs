using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Interaction : MonoBehaviour
{
    public float checkRate = 0.05f;
    private float lastCheckTime;
    public float maxCheckDistance;
    public LayerMask layerMask;
    
    public GameObject curInteractGameObject;
    private IInteractable curInteractable;
    public Image promptImage;
    public GameObject promptPanel;
    public TextMeshProUGUI promptText;
    private Camera camera;
    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - lastCheckTime > checkRate)
        {
            lastCheckTime = Time.time;
            Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2.0f, Screen.height / 2.0f));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, maxCheckDistance, layerMask))
            {
                if (hit.collider.gameObject != curInteractGameObject)
                {
                    curInteractGameObject = hit.collider.gameObject;
                    curInteractable = hit.collider.GetComponent<IInteractable>();
                    SetPrompt();
                }
            }
            else
            {
                curInteractGameObject = null;
                curInteractable = null;
                promptPanel.SetActive(false);
            }
        }
    }

    void SetPrompt()
    {
        promptPanel.SetActive(true);
        SetPromptText();
        SetPromptImage();
    }
    void SetPromptText()
    {
        promptText.text = curInteractable.GetInteractPrompt();
    }

    void SetPromptImage()
    {
        promptImage.sprite = curInteractable.GetInteractSprite();
        
    }

    public void OnInteractInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && curInteractable != null)
        {
            curInteractable.OnInteract();
            curInteractGameObject = null;
            curInteractable = null;
        }
    }
}
