using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateObserver : MonoBehaviour
{

    public static ObserverState HuDState = ObserverState.idle;

    [SerializeField] Map mapObject;
    [SerializeField] DeckManager deckManagerObject;

    public enum ObserverState
    {
        idle, //no HuD are open
        map, //regular map is open
        travel, // travel mode of the map is open
        deckViewer //the deck preview is open
    }

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public static void HuDStateRequest(ObserverState newState)
    {

        switch (HuDState)
        {
            case ObserverState.idle:
                {
                    //neutral state where nothing would be active
                    HuDState = newState;
                    break;
                }

            case ObserverState.map:
                {
                    //the map is open. thus, no other menus should be openned.
                    //can enable travel
                    if (newState == ObserverState.idle || newState == ObserverState.travel)
                    {
                        HuDState = newState;
                    }
                    break;
                }

            case ObserverState.travel:
                {
                    //the map is open and currently able to allow travel. thus, no other menus should be openned.
                    // can disable travel
                    if (newState == ObserverState.idle || newState == ObserverState.map)
                    {
                        HuDState = newState;
                    }
                    break;
                }

            case ObserverState.deckViewer:
                {
                    //the deck viewer is open. thus, no unrelated menus should be openned.
                    if (newState == ObserverState.idle)
                    {
                        HuDState = newState;
                    }
                    break;
                }
        }
    }

}
