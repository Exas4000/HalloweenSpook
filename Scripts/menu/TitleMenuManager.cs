using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleMenuManager : MonoBehaviour
{
    private bool sceneStarting = true; //did we enter the scene
    private bool isLoadingGame = false; //are we going to the game proper
    private int stateSelectionInt = -1; //-1 is default

    [SerializeField] float loadTime = 2f; //time to stay in the "loading" enum state
    private float timer = 0;

    [SerializeField] float speed = 2f; //speed for the the entry/exit of canvas
    private Vector3 centralAnchor; //position used by rect transforms when centered on screen (hopefully)

    [SerializeField] GameObject canvaMain;
    [SerializeField] GameObject canvaPlay;
    [SerializeField] GameObject canvaSetting;
    [SerializeField] GameObject canvaCredits;
    [SerializeField] GameObject canvaCollection;

    private enum titleState
    {
        //enum used to assist with title screen effects/transitions.

        loading,//lose control as you enter and leave the title scene
        main, //main menu, branch to other places
        gotoSpecific, //transitionnal state from the main state to other states
        gotoMain, //transitionnal state from spécific states back to the main state 
        collection, //where the player can look at info about the game as they play
        charSelect, //the screen before starting the game, displays the playable characters
        setting, // the screen to adjust sounds and other options as needed
        credit // the screen displaying the source of music and assets used
    }

    private titleState myState =  titleState.loading;

    void Start()
    {
        centralAnchor = this.gameObject.GetComponent<RectTransform>().position;
        canvaMain.GetComponent<RectTransform>().position += new Vector3(-speed * loadTime, 0, 0);
        canvaPlay.GetComponent<RectTransform>().position += new Vector3(-speed * loadTime,0, 0);
        canvaSetting.GetComponent<RectTransform>().position += new Vector3(-speed * loadTime, 0, 0);
        canvaCredits.GetComponent<RectTransform>().position += new Vector3(-speed * loadTime, 0, 0);
        canvaCollection.GetComponent<RectTransform>().position += new Vector3(-speed * loadTime, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        switch(myState)
        {
            case titleState.loading:
                {                   
                    //check if finished loading
                    if (TimerManager(false))
                    {                        

                        if (sceneStarting)
                        {
                            //go to main
                            myState = titleState.main;
                            sceneStarting = false;

                            canvaMain.GetComponent<RectTransform>().position = centralAnchor; //set buttons into useable position/cancel unwanted offset from repeated use
                            //enable button interactions

                        }
                        else if (isLoadingGame)
                        {
                            //load main game scene
                            SceneManager.LoadScene(1);
                        }
                    }
                    else
                    {
                        timer += Time.deltaTime;

                        //code to move the buttons into view
                        if (!isLoadingGame)
                        {
                            slideCanvas(canvaMain.GetComponent<RectTransform>(), true, true, speed);
                        }
                        
                    }

                    break;
                }

            case titleState.main:
                {
                    break;
                }

            case titleState.gotoSpecific:
                {
                    timer += Time.deltaTime;

                    slideCanvas(canvaMain.GetComponent<RectTransform>(), true, false, speed);
                    subMenuSwitch(true);
                    break;
                }

            case titleState.gotoMain:
                {

                    timer += Time.deltaTime;

                    slideCanvas(canvaMain.GetComponent<RectTransform>(), true, true, speed); //return main menu buttons into view
                    subMenuSwitch(false);                    

                    break;
                }
        }
    }

    private void slideCanvas(RectTransform target, bool isxAxis,bool isEnteringScreen, float speed)
    {
        //deciding how to move the rect transform
        int aiguilleur = 0;

        if (isxAxis)
        {
            aiguilleur += 1;
        }

        if (isEnteringScreen)
        {
            aiguilleur += 2;
        }

        //actual switch statement based on the bools
        switch(aiguilleur)
        {
            case 0:
                {
                    //goes down on the y axis
                    target.position += new Vector3(0, -speed, 0) * Time.deltaTime;
                    break;
                }
            case 1:
                {
                    target.position += new Vector3(-speed, 0, 0) * Time.deltaTime;
                    break;
                }
            case 2:
                {
                    //goes UP on the y axis
                    target.position += new Vector3(0, speed, 0) * Time.deltaTime;
                    break;
                }
            case 3:
                {
                    target.position += new Vector3(speed, 0, 0) * Time.deltaTime;
                    break;
                }
            
        }
        
    }

   
    //switch for making other menus come in and out when called by "gotoSpecific"
    private void subMenuSwitch(bool isEntering)
    {
        switch (stateSelectionInt)
        {
            case 0:
                {
                    slideCanvas(canvaPlay.GetComponent<RectTransform>(), true, isEntering, speed);

                    if (TimerManager(false))
                    {
                        if (isEntering)
                        {
                            myState = titleState.charSelect;
                            canvaPlay.GetComponent<RectTransform>().position = centralAnchor;
                        }
                        else
                        {
                            myState = titleState.main;
                            stateSelectionInt = -1;
                        }
                        
                    }

                    break;
                }

            case 1:
                {
                    slideCanvas(canvaSetting.GetComponent<RectTransform>(), true, isEntering, speed);

                    if (TimerManager(false))
                    {   

                        if (isEntering)
                        {
                            myState = titleState.setting;
                            canvaSetting.GetComponent<RectTransform>().position = centralAnchor;
                        }
                        else
                        {
                            myState = titleState.main;
                            stateSelectionInt = -1;
                        }
                    }

                    break;
                }
            case 2:
                {
                    slideCanvas(canvaCollection.GetComponent<RectTransform>(), true, isEntering, speed);

                    if (TimerManager(false))
                    {

                        if (isEntering)
                        {
                            myState = titleState.collection;
                            canvaCollection.GetComponent<RectTransform>().position = centralAnchor;
                        }
                        else
                        {
                            myState = titleState.main;
                            stateSelectionInt = -1;
                        }
                    }

                    break;
                }
            case 3:
                {
                    slideCanvas(canvaCredits.GetComponent<RectTransform>(), true, isEntering, speed);

                    if (TimerManager(false))
                    {

                        if (isEntering)
                        {
                            myState = titleState.credit;
                            canvaCredits.GetComponent<RectTransform>().position = centralAnchor;
                        }
                        else
                        {
                            myState = titleState.main;
                            stateSelectionInt = -1;
                        }
                    }

                    break;
                }
            case 4:
                {
                    isLoadingGame = true;
                    myState = titleState.loading;
                    break;
                }
        }
    }

    //allow external buttons from initiating the switch from the main menu to subMenus
    public void ButtonStateChange(int state)
    {
        if (myState == titleState.main)
        {
            TimerManager(true); //reset timer to 0
            stateSelectionInt = state; //prepare the int for the switch statement of "subMenuSwitch"

            myState = titleState.gotoSpecific;
        }
        
        
    }

    public void ButtonStart()
    {
        if (myState == titleState.charSelect)
        {
            TimerManager(true); //reset timer to 0
            stateSelectionInt = 4; //prepare the int for the switch statement of "subMenuSwitch"

            myState = titleState.gotoSpecific;
        }


    }

    //allow external buttons to initiate the closure of submenues
    public void ButtonToMenu()
    {
        if (myState != titleState.gotoMain && myState != titleState.gotoSpecific)
        {
            TimerManager(true); //reset timer to 0

            myState = titleState.gotoMain;
        }

        
    }



    //handles the timer reset and indicating if the timer for button transitions has reached the target time
    private bool TimerManager(bool reset)
    {
        if (reset)
        {
            timer = 0;
        }

        if (timer > loadTime)
        {
            return true;
        }
        else
        {
            return false;
        }
            
    }

    public void CloseApplication()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Application.Quit();

    }
}
