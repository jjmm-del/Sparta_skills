using UnityEngine;

public interface IInteractable
{
    public string GetInteractPrompt();
    public Sprite GetInteractSprite();
    public void OnInteract();
}
public class ItemObject : MonoBehaviour, IInteractable
{
    public ItemData data;

    public string GetInteractPrompt()
    {
        string str = $"{data.displayName}\n{data.description}";
        return str;
    }

    public Sprite GetInteractSprite()
    {
        Sprite icon = data.icon;
        return icon;
    }
    

    public void OnInteract()
    {
        CharacterManager.Instance.Player.itemData = data;
        CharacterManager.Instance.Player.addItem?.Invoke();
        Destroy(gameObject);
    }
}
