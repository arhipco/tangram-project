using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{ 
    
   // public static event Action eMouseIsBusy;
    public static bool MouseIsBusy;
    public static int level_number = 1;

    public enum GameState { Ready, Game, Complete }; 
    public GameState state = GameState.Ready; // ready, game , complete, null
    public static Vector3 mousePos;
    public static float startPosX, startPosY; 
    public static float timerStart;
    public static int installedCount;
 
    public Color[] colorBank;
 
    void ResetLevel(int ln)
    {   
        if (ln > 2)
            ln = 1;
        if (state == GameState.Complete)
            SceneManager.LoadScene("Level" + ln);

        state = GameState.Game;
        installedCount = 0; 
    }
    public void Awake()
    {   
        if (state == GameState.Ready) 
            ResetLevel(level_number);
    }
     
    public void Update() 
    {   
        if (state == GameState.Complete)
            ResetLevel(++level_number);

        if (installedCount == 7)
            state = GameState.Complete;
    }  
}
