using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICondition : MonoBehaviour
{
    public Condition health;
    //추후 추가

    public void Start()
    {
        CharacterManager.Instance.Player.condition.uiCondition = this;
    }
}
