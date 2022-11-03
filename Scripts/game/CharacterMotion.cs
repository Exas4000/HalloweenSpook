using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMotion : MonoBehaviour
{
    
    //this Script is meant to simply handle animations and game object's motion

    [Header("Movement Type")]  
    [SerializeField] bool isFloating = false; //should the character bob up and down?
    [SerializeField] bool isHopping = false; //should the character hop when moving or during idle?
    [SerializeField] bool isStationnary = false; // should the character remain in place rather than walk up to their target?
    [SerializeField] bool isWobbling = false; // should the character stretch and squatch


    [Header("Movement Variables")]
    [SerializeField] float speedWalk = 30f;//speed during movement on the scene
    [SerializeField] float floorYCord = 0f;// y position of the floor
    private bool startingOrientationisLeft = false; //base orientation initialized when intantialized
    private bool currentOrientationIsLeft = false; //bool for movementflips

    [Header("Hover Variables")]
    [SerializeField] float tweenIntensity = 5f; //how far to tween
    [SerializeField] float tweenInterval = 2f; //how many second should you tween
    [SerializeField] float tweenspeed = 2f; //how fast to move up and down
    private float currentHeight; //used as main anchor for where the middle point of the hover should be, also used by hop to know where the floor is.
    private int hoverRebound = 1; //used by the switch statement to alternate hover direction
    private float hoverTimer = 0;

    [Header("Hop Variables")]
    [SerializeField] float hopInterval = 3f; //cooldown between hops
    [SerializeField] float hopHeight = 6f; //max Y value of the jump
    [SerializeField] float hopSpeed = 6f; // initial hop speed
    [SerializeField] float gravity = 0.4f; //how fast does the object fall
    [SerializeField] bool randomizeHop = true;
    private float hopTimer = 0;
    private bool canHop = true; //if true, allow 1 jump for the cycle.
    private int hopState = 0; //were in the jump we are at
    private float currentHopSpeed = 0;


    [Header("wob Variables")]
    [SerializeField] float wobInterval = 3f; //cooldown between stretch and squash
    [SerializeField] float wobIntensity = 0.3f; //how much squash is applied at a most
    [SerializeField] float wobSpeed = 30; //how fast the squash is applied
    private int rebound = 1; //used by switch state to alternate stretch and squash
    private float wobTimer = 0;

    private movementType moveMethod;

    private enum movementType
    {
        Float, //Float/hover is highest priority
        Hop,
        Walk //lowest priority
    }

    void Start()
    {
        if (transform.rotation.y <= 179)
        {
            startingOrientationisLeft = true;
            currentOrientationIsLeft = true;
        }

        //set movement behavior
        if (isFloating)
        {
            moveMethod = movementType.Float;
            currentHeight = transform.position.y;
            hoverTimer = tweenInterval / 2; // make sure the main character can hover at regular pace.
        }
        else if (isHopping)
        {
            moveMethod = movementType.Hop;
            currentHeight = transform.position.y;
            currentHopSpeed = hopSpeed;
        }
        else
        {
            moveMethod = movementType.Walk;
        }
    }

    
    void Update()
    {
        StretchAndSquash(isWobbling);

        switch(moveMethod)
        {
            case movementType.Float:
                {
                    Hover(true);
                    break;
                }
            case movementType.Hop:
                {
                    //hop function
                    Hop(true);
                    break;
                }
            case movementType.Walk:
                {
                    //any functions i could want
                    break;
                }
        }
    }

    private void StretchAndSquash(bool squash)
    {

        if (squash)
        {
            switch(rebound)
            {
                case -1:
                    {
                        if (transform.localScale.x > 1 - wobIntensity)
                        {
                            transform.localScale += new Vector3(-wobSpeed, wobSpeed, 0) * Time.deltaTime;
                        }

                        break;
                    }

                case 1:
                    {
                        if (transform.localScale.x < 1 + wobIntensity)
                        {
                            transform.localScale += new Vector3(wobSpeed, -wobSpeed, 0) * Time.deltaTime;
                        }

                        break;
                    }
            }
        }

        if (wobTimer < wobInterval)
        {
            wobTimer += Time.deltaTime;
        }
        else
        {
            wobTimer = 0;
            rebound = rebound * -1;

        }
    }

    private void Hover(bool shouldHover)
    {
        //hover up and down up to selected limit before hand.
        if (shouldHover)
        {
            switch(hoverRebound)
            {
                case 1:
                    {
                        if (transform.position.y > currentHeight - tweenIntensity)
                        {
                            transform.position += new Vector3(0, -tweenspeed , 0) * Time.deltaTime;
                        }
                        break;
                    }

                case -1:
                    {
                        if (transform.position.y < currentHeight + tweenIntensity)
                        {
                            transform.position += new Vector3(0, tweenspeed, 0) * Time.deltaTime;
                        }
                        break;
                    }
            }
        }

        //timer
        if (hoverTimer < tweenInterval)
        {
            hoverTimer += Time.deltaTime;
        }
        else
        {
            hoverTimer = 0;
            hoverRebound = hoverRebound * -1;
        }
        
    }

    private void Hop(bool hop)
    {
        if (canHop && hop)
        {
            //hoping behaviour.
            switch(hopState)
            {
                case 0:
                    {

                        //move based on current speed and gravity
                        transform.position += new Vector3(0, currentHopSpeed, 0) * Time.deltaTime;

                        currentHopSpeed -= gravity * Time.deltaTime;

                        // change state if speed should be 0;
                        if (transform.position.y <= currentHeight)
                        {
                            hopState = 1;
                            transform.position = new Vector3(transform.position.x,currentHeight,transform.position.z);
                            canHop = false;
                        }

                        break;
                    }

                case 1:
                    {
                        //make sure it stays in place
                        currentHopSpeed = 0;
                        break;
                    }
            }



        }

        //hop timers to prepare a new jump
        if (hopTimer > 0)
        {
            hopTimer -= Time.deltaTime;
        }
        else if (!canHop)
        {
            canHop = true;
            hopState = 0;
            hopTimer = hopInterval;
            currentHopSpeed = hopSpeed;

            //randomize hops
            if (randomizeHop)
            {
                hopTimer += (Random.Range(0, hopInterval)) / 10;
            }
        }
    }
}
