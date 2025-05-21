using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public ItemData item;

    //public UIInventory inventory;
    public Image icon;
    private Outline outline;

    public int index; //123
    public bool canUse;

    private void Awake()
    {
        outline = GetComponent<Outline>();
    }
    private void OnEnable()
    {
        outline.enabled = canUse;
    }

    public void set()
    {
        icon.gameObject.SetActive(true);
        icon.sprite = item.icon;
        if (outline != null)
        {
            outline.enabled = canUse;
        }
    }

    public void Clear()
    {
        item = null;
        icon.gameObject.SetActive(false);
    }
}
