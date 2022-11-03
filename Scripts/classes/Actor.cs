using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Actor 
{
    //character prefab to be instantialized. the prefab will have all the script to work
    [SerializeField] GameObject prefab;

    //general info
    [SerializeField] private string name;
    [SerializeField] private bool isEnemy; // determine team and orientation!
    [SerializeField] private int hp; //secret stat if i wanna go with phobia's gimmick later
    [SerializeField] private int willPower; // the actual health used in 99% of the fights, if it reach 0, switch to being scared.
    [SerializeField] private float spawnY = 0.80f; //how high should you spawn the puppet gameObject on the y axis
    private bool isScared = false; //state whenever the character has no will left. if the whole team is scard, game over

    //passive and cards
    //add "card[]"
    [SerializeField] private List<Card> deck = new List<Card>();
    //add passive[]

    //add list<passive> as these can change
    //could wrap up the revival debuff as a permanent passive


    //constructor
    public Actor()
    {
        //unsure if the base constructor override the serialized info

        /*
        name = "default name";
        isEnemy = false;
        hp = 5;
        willPower = 3;

        //does not generate a deck for now.
        Debug.Log("testing, if this shows up, i am in trouble");
        */
    }

    public Actor(string argName,bool enemy, int hpValue, int will, GameObject puppet)
    {
        name = argName;
        isEnemy = enemy;
        hp = hpValue;
        willPower = will;
        prefab = puppet;

        Debug.Log("a new actor was created.");
    }

    public Actor(string argName, bool enemy, int hpValue, int will, GameObject puppet, List<Card> newDeck)
    {
        name = argName;
        isEnemy = enemy;
        hp = hpValue;
        willPower = will;
        prefab = puppet;

        deck = newDeck;
        //Debug.Log("a new actor was created. deck version");
    }

    public Actor(string argName, bool enemy, int hpValue, int will)
    {
        name = argName;
        isEnemy = enemy;
        hp = hpValue;
        willPower = will;        
    }

    //get/set
    public string GetName()
    {
        return name;
    }

    public void SetName(string newName)
    {
        name = newName;
    }

    public bool GetSide()
    {
        return isEnemy;
    }

    public float GetSpawnY()
    {
        return spawnY;
    }

    public void SetSpawnY(float value)
    {
        spawnY = value;
    }

    public int GetHp()
    {
        return hp;
    }

    public void SetHp(int value)
    {
        hp = value;
    }

    public int GetWillpower()
    {
        return willPower;
    }

    public void SetWillpower(int value)
    {
        willPower = value;
    }

    public bool GetScared()
    {
        return isScared;
    }

    public void SetScared(bool newBool)
    {
        isScared = newBool;
    }

    public GameObject GetPuppetPrefab()
    {
        return prefab;
    }

    public void SetPuppetPrefab(GameObject newPrefab)
    {
        prefab = newPrefab;
    }

    public List<Card> GetDeck()
    {
        //Debug.Log("deck passed to other script");
        return deck;
    }

    public Card GetSoloCard(int cardNum)
    {
        return deck[cardNum];
    }
}
