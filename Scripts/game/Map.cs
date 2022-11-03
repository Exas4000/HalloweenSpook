using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    /// <summary>
    /// this script's use is simply to have a smooth opening and closing process of the map
    /// </summary>


    [SerializeField] GameObject map;

    [SerializeField] float transitionTime = 2f;
    private float timer = 0;

    [SerializeField] float speed = 20f;

    private bool isClosing = true;
    private bool canTravel = true;
    private process myState = process.close;
    private Vector3 startPosMap;

    private enum process
    {
        moving,
        close,
        open
    }

    void Start()
    {
        if (map != null)
        {
            var rect = map.GetComponent<RectTransform>();

            //save initial map position to avoid offset from repeated open/close process
            startPosMap = rect.position;

            //move map off-screen
            rect.position += new Vector3(0, speed * transitionTime, 0);

            
        }
    }

    
    void Update()
    {
        switch (myState)
        {
            case process.moving:
                {
                    //moving the menu
                    if (isClosing)
                    {
                        map.GetComponent<RectTransform>().position += new Vector3 (0,speed,0) * Time.deltaTime;
                    }
                    else
                    {
                        map.GetComponent<RectTransform>().position -= new Vector3(0, speed, 0) * Time.deltaTime;
                    }

                    //checking which state to send the menu
                    if (timer >= transitionTime)
                    {
                        if (isClosing)
                        {
                            myState = process.close;
                        }
                        else
                        {
                            myState = process.open;
                            //adjust map position
                            map.GetComponent<RectTransform>().position = startPosMap;
                        }

                        
                    }

                    timer += Time.deltaTime;
                    break;
                }

            case process.close:
                {
                    //send into transition state
                    if (!isClosing)
                    {
                        myState = process.moving;
                    }
                    break;
                }

            case process.open:
                {
                    //send into transition state
                    if (isClosing)
                    {
                        myState = process.moving;
                    }
                    break;
                }
        }
    }

    public void OpenMap(bool open)
    {
        if (StateObserver.HuDState == StateObserver.ObserverState.map || StateObserver.HuDState == StateObserver.ObserverState.travel || StateObserver.HuDState == StateObserver.ObserverState.idle)
        {
            //call to change the state. make sure to use it as a true statement when changing location on the map.
            isClosing = open;
            timer = 0;
        }
            
    }

    public void OpenMapSwitch()
    {
        //if allowed to open the map
        if (StateObserver.HuDState == StateObserver.ObserverState.map || StateObserver.HuDState == StateObserver.ObserverState.travel || StateObserver.HuDState == StateObserver.ObserverState.idle)
        {
            //alternate closing state
            switch (myState)
            {
                case process.close:
                    {
                        //reset timer
                        timer = 0;

                        //change observer state to prevent multiple opened menus at the same time
                        if (canTravel)
                        {
                            //travel mode
                            StateObserver.HuDStateRequest(StateObserver.ObserverState.travel);
                        }
                        else
                        {
                            //normal map with no travel
                            StateObserver.HuDStateRequest(StateObserver.ObserverState.map);
                        }

                        isClosing = false;
                        break;
                    }
                case process.open:
                    {
                        //go back to idle to allow other menus to open
                        StateObserver.HuDStateRequest(StateObserver.ObserverState.idle);

                        

                        //reset timer
                        timer = 0;

                        isClosing = true;
                        break;
                    }
            }
        }
            
    }

    public void CloseFromOutside()
    {

        //if in open state, close without calling state changes.
        if (myState == process.open)
        {
            isClosing = true;
        }

    }
}
