using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEditor.Timeline.Actions;
using UnityEngine;

public class ItemPanel : MonoBehaviour
{
    public ItemSlot[] slots;
    public GameObject inventoryWindow;
    public Transform slotPanel;

    private int curUseIndex;
    
    private PlayerController controller;
    private PlayerCondition condition;
    
    
    // Start is called before the first frame update
    void Start()
    {
        controller = CharacterManager.Instance.Player.controller;
        condition = CharacterManager.Instance.Player.condition;
        CharacterManager.Instance.Player.addItem += AddItem;
        
        slots = new ItemSlot[slotPanel.childCount];

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = slotPanel.GetChild(i).GetComponent<ItemSlot>();
            slots[i].index = i;
            slots[i].inventory = this;
            slots[i].Clear();
        }

    }

    public void AddItem()
    {
        ItemData data = CharacterManager.Instance.Player.itemData;
        if (data == null) return;
        
         if (data.itemType == ItemType.Resource)
        {
            switch (data.resourceType)
            {
                case ResourceType.Coin: ResourcePanel.Instance.AddCoin(); break;
                case ResourceType.Star: ResourcePanel.Instance.AddStar(); break;
                case ResourceType.Life: ResourcePanel.Instance.AddLife(); break;
            }

            CharacterManager.Instance.Player.itemData = null;
            return;
        }

        
        if (data.itemType == ItemType.Consumable)
        {
            ItemSlot empty = GetEmptySlot();
            if (empty != null)
            {
                empty.item = data;
                empty.canUse = true;
                UpdateUI();
            }
            

            CharacterManager.Instance.Player.itemData = null;
            return;
        }
        

    }

    public void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item != null)
                slots[i].Set();
            else
                slots[i].Clear();
        }
    }

    ItemSlot GetEmptySlot()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
            {
                return slots[i];
            }
        }
        return null;
    }

    public void UseItem(int index)
    {
        if (index < 0 || index >= slots.Length) return;
        
        ItemSlot slot = slots[index];
        if (slot.item == null) return;
        
        ItemData data = slot.item;
        PowerUpManager powerUpManager = CharacterManager.Instance.Player.GetComponent<PowerUpManager>();
        PlayerController controller = CharacterManager.Instance.Player.controller;

        switch (data.powerUpType)
        {
            case PowerUpType.Grow: 
                ScalePowerUp growUp = new ScalePowerUp(controller.transform, data.scaleFactor);
                powerUpManager.ApplyPowerUp(growUp, data.duration);
                break;
                
            case PowerUpType.Shrink: 
                ScalePowerUp shrinkUp = new ScalePowerUp(controller.transform, data.scaleFactor);
                powerUpManager.ApplyPowerUp(shrinkUp, data.duration);
                break;
                
            case PowerUpType.Flower:
                ProjectilePowerUp flowerUp = new ProjectilePowerUp(
                    controller,
                    data.projectilePrefab,
                    data.projectileSpeed,
                    data.projectileLifeTime
                );
                powerUpManager.ApplyPowerUp(flowerUp, data.duration);
                break;
            
            case PowerUpType.None:
            default:
                    break;
        }
        
        slot.Clear();
        UpdateUI();
    }
}
