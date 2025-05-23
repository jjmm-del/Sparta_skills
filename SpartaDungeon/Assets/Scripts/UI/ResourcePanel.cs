using TMPro;
using UnityEngine;

public class ResourcePanel : MonoBehaviour
{
    public static ResourcePanel Instance { get; private set; }
    
    
    public TextMeshProUGUI coinText, starText, lifeText;
    public int coinValue, starValue, lifeValue;

    private const int CoinsForExtraLife = 100;
    private void Awake()
    {
        if(Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Update()
    {
        coinText.text = $"X {coinValue}";
        starText.text = $"X {starValue}";
        lifeText.text = $"X {lifeValue}";
    }

    public void AddCoin(int amount = 1)
    {
        coinValue += amount;
        CharacterManager.Instance.Player.condition.Heal(1);

        if (coinValue >= CoinsForExtraLife)
        {
            coinValue -= CoinsForExtraLife;
            AddLife(1);
        }
    }

    public void AddStar(int amount = 1)
    {
        starValue += amount;   
    }

    public void AddLife(int amount = 1)
    {
        lifeValue += amount;
    }
}
