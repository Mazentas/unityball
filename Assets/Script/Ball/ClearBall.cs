using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearBall : Ball
{
    void Awake()
    {
        gameObject.tag = "Ball";
        effect = new ClearingEffect(GameManager.Instance ,new Effect(Timer.Instance, BonusTime, balltypes));
    }
}
