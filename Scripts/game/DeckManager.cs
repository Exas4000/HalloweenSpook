using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeckManager : MonoBehaviour
{
    /// <summary>
    /// this script handle the deck tab and keep all the deck info that will be ACTIVELY used.
    /// do not use the GameManager for cards usage and upgrades.
    /// </summary>



    [SerializeField] GameObject viewerBG;
    

    //might not be necessary
    [SerializeField] float transitionTime = 2f;
    private float opacityValue = 0;

    //card settings
    [SerializeField] float speed = 20f;
    [SerializeField] float minimizedScale = 0.1f;

    private bool isClosing = true;
    private process myState = process.close;
    [SerializeField] float opacity = 0.3f;//opacity of the Background while open

    [SerializeField] GameObject[] visualCard;
    private int teamMemberId = 0;
    private bool refreshCards = true;
    private int numcard = 0;

    //deck info. make sure to update the ally decks when allies are recruited.
    private List<Card> leaderDeck;
    private List<Card> allyDeck_1;
    private List<Card> allyDeck_2;



    private enum process
    {
        moving,
        close,
        open
    }




    void Start()
    {
        for (int i = 0; i < visualCard.Length;i++)
        {
            //set all cards to minimized size, then deactivate
            visualCard[i].GetComponent<VisualCard>().ForceSize(minimizedScale);
            //visualCard[i].GetComponent<RectTransform>().localScale = new Vector3(minimizedScale, minimizedScale, 1); //call card's function instead
            visualCard[i].SetActive(false);
        }

        if (viewerBG != null)
        {
            //set opacity of BG to 0
            viewerBG.GetComponent<Image>().color = new Vector4(1,1,1,0);
        }
       
    }

    
    void Update()
    {
        switch(myState)
        {
            case process.moving:
                {
                    //slow transition to the "open" state where 
                    viewerBG.GetComponent<Image>().color = new Vector4(1, 1, 1, opacity) * Time.deltaTime;
                    

                    for (int i = 0; i < leaderDeck.Count; i++)
                    {
                        visualCard[i].SetActive(true);
                        visualCard[i].GetComponent<VisualCard>().ResizeInit(true);
                    }

                    //switch to "open" state when no more cards need to be opened
                    myState = process.open;

                    break;
                }

            case process.open:
                {
                    //end state, causes a quick closure when asked back to "close" state
                    if (refreshCards)
                    {
                        switch(teamMemberId)
                        {
                            case 0:
                                {
                                    //reset counter to 0
                                    numcard = 0;

                                    for (int i = 0; i < leaderDeck.Count; i++)
                                    {
                                        //make sure the cards are active and go to the right size
                                        visualCard[i].SetActive(true); //always a chance that decks does not have the same amount of active cards.
                                        visualCard[i].GetComponent<VisualCard>().ResizeInit(true);

                                        //change the visual to reflect the card previewed
                                        visualCard[i].GetComponent<VisualCard>().ChangeCardInfo(leaderDeck[i].GetCardType(), leaderDeck[i].GetCost(), leaderDeck[i].GetPrimaryEffect(), 
                                            leaderDeck[i].GetPrimaryPower(), leaderDeck[i].GetSecondaryEffect_1(), leaderDeck[i].GetSecondaryEffect_2());

                                        //increment counter
                                        numcard += 1;
                                    }

                                    for (int i = numcard; i < visualCard.Length; i++)
                                    {
                                        //deactivate unused cards previews
                                        visualCard[i].SetActive(false);
                                    }

                                    break;
                                }
                        }

                       
                    }

                    //resize and close all card preview
                    if (isClosing)
                    {
                        for (int i = 0; i < visualCard.Length; i++)
                        {
                            visualCard[i].GetComponent<VisualCard>().ForceSize(minimizedScale);
                            visualCard[i].SetActive(false);
                        }

                        //turn off opacity
                        if (viewerBG != null)
                        {
                            //set opacity of BG to 0
                            viewerBG.GetComponent<Image>().color = new Vector4(1, 1, 1, 0);
                        }

                        //back to closed state
                        myState = process.close;
                    }

                    break;
                }

            case process.close:
                {
                    //default stay, more of an idle
                    if (!isClosing)
                    {
                        //switch state
                        myState = process.moving;
                    }
                    break;
                }
        }
    }

    public void AddNewAlly(List<Card> newList)
    {
        //Debug.Log("addNewAlly is empty");

        
        if (leaderDeck == null)
        {
            Debug.Log("leaderDeck's deck Added");
            leaderDeck = newList;
        }
        else if (allyDeck_1 == null)
        {
            Debug.Log("ally_1's deck Added");
            allyDeck_1 = newList;
        }
        else if (allyDeck_2 == null)
        {
            Debug.Log("ally_2's deck Added");
            allyDeck_2 = newList;
        }

    }

    public void OpenDeckPreview(bool open)
    {
        if (StateObserver.HuDState == StateObserver.ObserverState.deckViewer || StateObserver.HuDState == StateObserver.ObserverState.idle)
        {
            //call to change the state. 
            isClosing = open;
        }

    }

    public void OpenDeckSwitch()
    {

        if (StateObserver.HuDState == StateObserver.ObserverState.idle || StateObserver.HuDState == StateObserver.ObserverState.deckViewer)
        {
            switch(myState)
            {
                case process.close:
                    {
                        StateObserver.HuDStateRequest(StateObserver.ObserverState.deckViewer);

                        isClosing = false;
                        break;
                    }
                case process.open:
                    {
                        StateObserver.HuDStateRequest(StateObserver.ObserverState.idle);

                        isClosing = true;
                        break;
                    }
            }
        }
    }
}
