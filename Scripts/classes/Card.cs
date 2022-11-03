using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Card
{
    [SerializeField] private int cost;
    [SerializeField] private int primaryEffectPower;

    [SerializeField] private cardEffects primaryEffectType;
    [SerializeField] private cardEffects SecondaryEffectType_1;
    [SerializeField] private cardEffects SecondaryEffectType_2;

    [SerializeField] private cardType type;


    public enum cardEffects
    {
        empty, //only use for secondary! basically makes it so nothing happen
        spook, // the regular attack command!
        counter,// counter attack!
        shock, // reduce action points by 1 per stack
        energize, // increase action points by 1 per stack
        tension, // better spook enabler for next turn!
        betterSpook, // a damage up for spook
        shield, // temporary hp!
        betterShield, // empowers temporary hp
        hide, //prevent 1 instance of damage per stack
        AoE, // makes it so the action affect everyone
        doublePrimary, // plays the primary effect twice
        fear, // lingering damage for a few things.
        LesserSpook, //debuff to spook power
        LesserShield, //debuff to temporary hp
        taunt, //becomes a priority target

    }

    public enum cardType
    {
        //will help the game know who to target by default and deck viewer to know what color to use for the cards
        //also assist with upgrade rules
        Red, // spook!
        yellow,// counter
        purple, // wail (aka afflicting status to the enemy)
        green // cheer! (aka afflicting status to allies)
    }



    public int GetCost()
    {
        return cost;
    }

    public void SetCost(int value)
    {
        cost = value;
    }

    public int GetPrimaryPower()
    {
        return primaryEffectPower;
    }

    public void SetPrimaryPower(int value)
    {
        primaryEffectPower = value;
    }

    public cardEffects GetPrimaryEffect()
    {
        return primaryEffectType;
    }

    public void SetPrimaryEffect(cardEffects newEffect)
    {
        primaryEffectType = newEffect;
    }

    public cardEffects GetSecondaryEffect_1()
    {
        return SecondaryEffectType_1;
    }

    public void SetSecondaryEffect_1(cardEffects newEffect)
    {
        SecondaryEffectType_1 = newEffect;
    }

    public cardEffects GetSecondaryEffect_2()
    {
        return SecondaryEffectType_2;
    }

    public void SetSecondaryEffect_2(cardEffects newEffect)
    {
        SecondaryEffectType_2 = newEffect;
    }

    public cardType GetCardType()
    {
        return type;
    }

    public void SetCardType(cardType newType)
    {
        type = newType;
    }
}
