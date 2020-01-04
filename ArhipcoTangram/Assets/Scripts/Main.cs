using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{ 
    public static bool MouseIsBusy;        
    public static int level_number = 1;
    string level_state = "ready"; // ready, game , complete, null
    public static Vector3 mousePos;
    public static float startPosX, startPosY; 
    public static float timerStart;
    public static int installedCount;
 
    public Color[] colorBank;
 
    void ResetLevel(int ln)
    {   
        if (ln > 2)
            ln = 1;
        if (level_state == "complete")
            SceneManager.LoadScene("Level" + ln);

        level_state = "game";
        installedCount = 0; 
    }
    public void Awake()
    {   
        if (level_state == "ready") 
            ResetLevel(level_number);  
    }
     
    public void Update() 
    {   
        if (level_state == "complete")  
            ResetLevel(++level_number);

        if (installedCount == 7) 
            level_state = "complete";   
    }  
}
