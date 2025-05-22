using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Resource, //재화아이템 - 별, 코인 등
    Consumable //소비아이템 - 버섯, 꽃
}

public enum ResourceType
{
    Coin,
    Star,
    Life
}

[CreateAssetMenu(fileName = "Item", menuName = "New Item")]
public class ItemData : ScriptableObject
{
    [Header("Info")]
    public string displayName, description;
    public ItemType itemType;
    public ResourceType resourceType;
    public Sprite icon;
    //public GameObject dropPrefab;
    

    [Header("Stacking")]
    public bool canStack;
    public int maxStackAmount;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
