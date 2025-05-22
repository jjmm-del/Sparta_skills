using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResourcePanel : MonoBehaviour
{
    public GameObject coin;
    public GameObject star;
    public GameObject life;
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI starText;
    public TextMeshProUGUI lifeText;
    

    public int coinValue;
    public int starValue;
    public int lifeValue;
    private void Start()
    {
        
    }

    void Update()
    {
        coinText.text = CoinValueString(coinValue);
        starText.text = StarValueString(starValue);
        lifeText.text = LifeValueString(lifeValue);
    }

    string CoinValueString(int coinValue)
    {
        string CoinValueString = "X " + coinValue.ToString();
        
        return CoinValueString;
    }
    string StarValueString(int starValue)
    {
        string starValueString = "X " + starValue.ToString();
        
        return starValueString;
    }
    string LifeValueString(int lifeValue)
    {
        string lifeValueString = "X " + lifeValue.ToString();
        
        return lifeValueString;
    }
}
