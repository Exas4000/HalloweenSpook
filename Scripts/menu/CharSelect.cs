using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharSelect : MonoBehaviour
{

    [SerializeField] Button startGameButton;
    [SerializeField] Image splashArt;
    [SerializeField] Text[] playText;

    [SerializeField] Sprite[] splashSprites;
    [SerializeField] string[] names;
    [SerializeField] string[] desc;
    [SerializeField] string[] info;

    [SerializeField] string[] specialInfo;




    void Start()
    {
        PreparePref();
    }

    
    void Update()
    {
        
    }

    private void PreparePref()
    {
        //character flags. 0 means not unlocked, 1 unlocked, 2 disabled
        if (!PlayerPrefs.HasKey("Char_0"))
        {
            //Estelle, unlocked from the start
            PlayerPrefs.SetInt("Char_0", 1);
        }

        if (!PlayerPrefs.HasKey("Char_1"))
        {
            //Jack jr.
            PlayerPrefs.SetInt("Char_1", 0);
        }

        if (!PlayerPrefs.HasKey("Char_2"))
        {
            //Charly
            PlayerPrefs.SetInt("Char_2", 0);
        }

        //character selection, reset upon going to main menu
        PlayerPrefs.SetInt("Selection", 0);

    }

    public void SelectChar(int value)
    {
        //set selection to specific character id
        PlayerPrefs.SetInt("Selection", value);

        //check f character can be used as leader for new game
        string id = "Char_" + value;
        int choiceState = PlayerPrefs.GetInt(id);

        if (choiceState == 1)
        {
            //if true, enable play button
            startGameButton.interactable = true;
        }
        else
        {
            startGameButton.interactable = false;
        }

        //set text to show character's name, description and other info
        playText[0].text = names[value];
        playText[1].text = desc[value];

        switch(choiceState)
        {
            case 0:
                {
                    playText[2].text = "Win a spooking duel against this character to unlock them!";
                    break;
                }
            case 1:
                {
                    playText[2].text = info[value];
                    break;
                }
            case 2:
                {
                    playText[2].text = "This character got caught by Thanatophobia.";
                    break;
                }
        }


        //setSplashArt to specific image
        if (splashArt != null)
        {
            splashArt.sprite = splashSprites[value];
        }
        

    }

}
