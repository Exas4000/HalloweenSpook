using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Event
{
    //this class is meant to content pertinent information used by
    //the game manager to load individual gameplay scenes

    [SerializeField] public int[] actorID; //the actors used for the event
    [SerializeField] public Vector3[] actorSpawnPositions; //spawn positions of the actors
    [SerializeField] public Sprite[] locationSprite;

    public Event()
    {

    }

    public Event(int[] actorArray, Vector3[] vectorArray, Sprite[] spriteSet)
    {
        actorID = actorArray;
        actorSpawnPositions = vectorArray;
        locationSprite = spriteSet;
    }

}

[System.Serializable]
public class Battle_Event : Event
{
    public void startBattle()
    {

    }
}

[System.Serializable]
public class Shop_Event : Event
{
    
}