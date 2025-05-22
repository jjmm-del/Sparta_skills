using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public ItemData item;
    public ItemPanel inventory;
    //public UIInventory inventory;
    public Image icon;
    private Outline outline;

    public int index; //123
    public bool canUse; //소비아이템만 넣을꺼니까

    private void Awake()
    {
        outline = GetComponent<Outline>();
    }
    private void OnEnable()
    {
        outline.enabled = canUse;
    }

    public void Set()
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
