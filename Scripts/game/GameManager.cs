using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //other Managers
    [SerializeField] DeckManager manager;

    //library of characters
    [SerializeField] Actor[] playableActorLibrary;
    [SerializeField] Actor[] playableEnemiesLibrary;

    //active actors
    private List<Actor> heroTeam = new List<Actor>(); // player's team
    private List<Actor> rivalTeam = new List<Actor>();// any npc, hostile or not
    private GameObject puppetsFriendly; //the game object for the visual aspect 
    private GameObject puppetsRival; //the game object for the visual aspect 



    private gameState state = gameState.loading;
    private bool initGame = false; //when false, "loading" will ssetup the game ssession. when true, it will instead send back to menu or reset the scene
    private bool restart = false; //decide if an ended game session should lead to main menu or be reset
    [SerializeField] float transitionDelay = 1f; //how long between
    private float timer = 0;

    public enum gameState
    {
        loading, // transition to the game scene and back to the main menu
        ending, // if the player wins the game
        loss, // if the player lose the game
        map, // the map to decide where to go
        battle, // battle between the player and the enemy
        shop, // non-hostile scenario
        transition //when we switch between places + necessary prep as needed
    }

    void Start()
    {
        //spawn chosen character
        GameObject hero = Instantiate(playableActorLibrary[0].GetPuppetPrefab(), new Vector3(-1.85f, playableActorLibrary[PlayerPrefs.GetInt("Selection")].GetSpawnY(), -2.55f), Quaternion.Euler(0, 180, 0));

        //add it to the team
        heroTeam.Add(new Actor(playableActorLibrary[PlayerPrefs.GetInt("Selection")].GetName(), false, playableActorLibrary[PlayerPrefs.GetInt("Selection")].GetHp(), playableActorLibrary[PlayerPrefs.GetInt("Selection")].GetWillpower(), hero, playableActorLibrary[PlayerPrefs.GetInt("Selection")].GetDeck()));


        //pass permanent deck to the deckManager
        manager.AddNewAlly(heroTeam[0].GetDeck());
    }


    void Update()
    {
        switch (state)
        {
            case gameState.loading:
                {
                    //giving time to do game prep and visual transition
                    if (timer >= transitionDelay)
                    {
                        //has the game already gone through the process of starting the game?
                        if (initGame)
                        {
                            //should you restart the scene for a new game?
                            if (restart)
                            {
                                SceneManager.LoadScene(1);//load the current scene
                            }
                            else
                            {
                                SceneManager.LoadScene(0); // back to main menu!
                            }
                        }
                        else
                        {

                            
                            //give control to the player to start playing!
                            initGame = true;
                            state = gameState.shop;
                        }
                    }
                    else
                    {
                        timer += Time.deltaTime;
                    }
                    break;
                }
        }
    }

    //function that initiate the gameManager to reset or return to the main menu
    public void ReturnToMain(bool resetGame)
    {
        timer = 0;
        restart = resetGame;
        state = gameState.loading;
    }

    public List<Card> fetchCardInfoAlly(int id)
    {
        //read pass them to the proper systems
        return heroTeam[id].GetDeck();
    }

    public List<Card> fetchCardInfoEnemy(int id)
    {
        //read pass them to the proper systems
        return rivalTeam[id].GetDeck();
    }

    public void BattleRequest(Battle_Event battle)
    {

    }
}
