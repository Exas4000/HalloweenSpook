using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VisualCard : MonoBehaviour
{

    [SerializeField] float minimizedSized = 0.1f;
    [SerializeField] float speed = 5;
    private bool isOpening = false;

    private RectTransform myTransform;

    //visual items
    [SerializeField] Text textCost;
    [SerializeField] Text textPrimary;
    [SerializeField] Image[] imageCardEffect;
    [SerializeField] Image cardBack;

    //sprite section
    //follow enum order from "Card" script
    [SerializeField] Sprite[] color;
    [SerializeField] Sprite[] effects;

    void Start()
    {
        myTransform = GetComponent<RectTransform>();
    }

    
    void Update()
    {
        //opening code
        if (isOpening)
        {
            if (myTransform.localScale.x < 1)
            {
                myTransform.localScale += new Vector3(speed, speed, 0)* Time.deltaTime;
            }
            else
            {
                //set size to proper one, then stop opening process
                myTransform.localScale = new Vector3(1, 1, 1);
                isOpening = false;
            }
        }

        //potential tooltip addition here
    }

    public void ForceSize(float value)
    {
        GetComponent<RectTransform>().localScale = new Vector3(value, value, 1);
    }

    public void ResizeInit(bool shouldResize)
    {
        isOpening = shouldResize;
    }

    //card visual section
    public void ChangeCostDisplay(int value)
    {
        textCost.text = "" + value;
    }

    public void ChangePrimaryTextDisplay(int value)
    {
        textPrimary.text = "" + value;
    }

    public void ChangePrimaryPicture(Card.cardEffects Arg)
    {
        //enable in-case
        imageCardEffect[0].enabled = true;

        switch(Arg)
        {
            case Card.cardEffects.empty:
            {
                imageCardEffect[0].enabled = false;
                break;
            }

            case Card.cardEffects.spook:
            {
                imageCardEffect[0].sprite = effects[1];
                break;
            }

            case Card.cardEffects.counter:
                {
                    imageCardEffect[0].sprite = effects[2];
                    break;
                }

            case Card.cardEffects.shock:
                {
                    imageCardEffect[0].sprite = effects[3];
                    break;
                }

            case Card.cardEffects.energize:
                {
                    imageCardEffect[0].sprite = effects[4];
                    break;
                }

            case Card.cardEffects.tension:
                {
                    imageCardEffect[0].sprite = effects[5];
                    break;
                }

            case Card.cardEffects.betterSpook:
                {
                    imageCardEffect[0].sprite = effects[6];
                    break;
                }

            case Card.cardEffects.shield:
                {
                    imageCardEffect[0].sprite = effects[7];
                    break;
                }

            case Card.cardEffects.betterShield:
                {
                    imageCardEffect[0].sprite = effects[8];
                    break;
                }

            case Card.cardEffects.hide:
                {
                    imageCardEffect[0].sprite = effects[9];
                    break;
                }

            case Card.cardEffects.AoE:
                {
                    imageCardEffect[0].sprite = effects[10];
                    break;
                }

            case Card.cardEffects.doublePrimary:
                {
                    imageCardEffect[0].sprite = effects[11];
                    break;
                }
            case Card.cardEffects.fear:
                {
                    imageCardEffect[0].sprite = effects[12];
                    break;
                }
            case Card.cardEffects.LesserSpook:
                {
                    imageCardEffect[0].sprite = effects[13];
                    break;
                }
            case Card.cardEffects.LesserShield:
                {
                    imageCardEffect[0].sprite = effects[14];
                    break;
                }
            case Card.cardEffects.taunt:
                {
                    imageCardEffect[0].sprite = effects[15];
                    break;
                }
        }
    }

    public void ChangeSecondaryPicture_1(Card.cardEffects Arg)
    {
        //enable in-case
        imageCardEffect[1].enabled = true;

        switch (Arg)
        {
            case Card.cardEffects.empty:
                {
                    imageCardEffect[1].enabled = false;
                    break;
                }

            case Card.cardEffects.spook:
                {
                    imageCardEffect[1].sprite = effects[1];
                    break;
                }

            case Card.cardEffects.counter:
                {
                    imageCardEffect[1].sprite = effects[2];
                    break;
                }

            case Card.cardEffects.shock:
                {
                    imageCardEffect[1].sprite = effects[3];
                    break;
                }

            case Card.cardEffects.energize:
                {
                    imageCardEffect[1].sprite = effects[4];
                    break;
                }

            case Card.cardEffects.tension:
                {
                    imageCardEffect[1].sprite = effects[5];
                    break;
                }

            case Card.cardEffects.betterSpook:
                {
                    imageCardEffect[1].sprite = effects[6];
                    break;
                }

            case Card.cardEffects.shield:
                {
                    imageCardEffect[1].sprite = effects[7];
                    break;
                }

            case Card.cardEffects.betterShield:
                {
                    imageCardEffect[1].sprite = effects[8];
                    break;
                }

            case Card.cardEffects.hide:
                {
                    imageCardEffect[1].sprite = effects[9];
                    break;
                }

            case Card.cardEffects.AoE:
                {
                    imageCardEffect[1].sprite = effects[10];
                    break;
                }

            case Card.cardEffects.doublePrimary:
                {
                    imageCardEffect[1].sprite = effects[11];
                    break;
                }
            case Card.cardEffects.fear:
                {
                    imageCardEffect[1].sprite = effects[12];
                    break;
                }
            case Card.cardEffects.LesserSpook:
                {
                    imageCardEffect[1].sprite = effects[13];
                    break;
                }
            case Card.cardEffects.LesserShield:
                {
                    imageCardEffect[1].sprite = effects[14];
                    break;
                }
            case Card.cardEffects.taunt:
                {
                    imageCardEffect[1].sprite = effects[15];
                    break;
                }
        }
    }

    public void ChangeSecondaryPicture_2(Card.cardEffects Arg)
    {
        //enable in-case
        imageCardEffect[2].enabled = true;

        switch (Arg)
        {
            case Card.cardEffects.empty:
                {
                    imageCardEffect[2].enabled = false;
                    break;
                }

            case Card.cardEffects.spook:
                {
                    imageCardEffect[2].sprite = effects[1];
                    break;
                }

            case Card.cardEffects.counter:
                {
                    imageCardEffect[2].sprite = effects[2];
                    break;
                }

            case Card.cardEffects.shock:
                {
                    imageCardEffect[2].sprite = effects[3];
                    break;
                }

            case Card.cardEffects.energize:
                {
                    imageCardEffect[2].sprite = effects[4];
                    break;
                }

            case Card.cardEffects.tension:
                {
                    imageCardEffect[2].sprite = effects[5];
                    break;
                }

            case Card.cardEffects.betterSpook:
                {
                    imageCardEffect[2].sprite = effects[6];
                    break;
                }

            case Card.cardEffects.shield:
                {
                    imageCardEffect[2].sprite = effects[7];
                    break;
                }

            case Card.cardEffects.betterShield:
                {
                    imageCardEffect[2].sprite = effects[8];
                    break;
                }

            case Card.cardEffects.hide:
                {
                    imageCardEffect[2].sprite = effects[9];
                    break;
                }

            case Card.cardEffects.AoE:
                {
                    imageCardEffect[2].sprite = effects[10];
                    break;
                }

            case Card.cardEffects.doublePrimary:
                {
                    imageCardEffect[2].sprite = effects[11];
                    break;
                }
            case Card.cardEffects.fear:
                {
                    imageCardEffect[2].sprite = effects[12];
                    break;
                }
            case Card.cardEffects.LesserSpook:
                {
                    imageCardEffect[2].sprite = effects[13];
                    break;
                }
            case Card.cardEffects.LesserShield:
                {
                    imageCardEffect[2].sprite = effects[14];
                    break;
                }
            case Card.cardEffects.taunt:
                {
                    imageCardEffect[2].sprite = effects[15];
                    break;
                }
        }
    }

    public void ChangeCardColor(Card.cardType Arg)
    {
        switch(Arg)
        {
            case Card.cardType.Red:
                {
                    cardBack.sprite = color[0];
                    break;
                }

            case Card.cardType.yellow:
                {
                    cardBack.sprite = color[1];
                    break;
                }

            case Card.cardType.purple:
                {
                    cardBack.sprite = color[2];
                    break;
                }

            case Card.cardType.green:
                {
                    cardBack.sprite = color[3];
                    break;
                }
        }
    }

    public void ChangeCardInfo(Card.cardType color,int cost, Card.cardEffects primaryEffect,int primaryPower, Card.cardEffects secondaryEffect_1, Card.cardEffects secondaryEffect_2)
    {
        //change the full card in one function
        ChangeCostDisplay(cost);

        ChangeCardColor(color);

        ChangePrimaryPicture(primaryEffect);
        ChangePrimaryTextDisplay(primaryPower);

        ChangeSecondaryPicture_1(secondaryEffect_1);
        ChangeSecondaryPicture_2(secondaryEffect_2);

    }
}
