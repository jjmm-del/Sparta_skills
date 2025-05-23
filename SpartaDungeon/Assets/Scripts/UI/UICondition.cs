using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICondition : MonoBehaviour
{
    public Condition health;
    public Condition coin;
    public Condition star;
    public Condition life;
    
    //추후 추가

    public void Start()
    {
        CharacterManager.Instance.Player.condition.uiCondition = this;
    }
}
