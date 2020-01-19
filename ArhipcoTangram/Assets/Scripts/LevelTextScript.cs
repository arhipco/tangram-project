using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelTextScript : MonoBehaviour
{
    public Text LevelText;
    void Start()
    {  
        LevelText = GetComponent<Text> ();
        LevelText.text = "Level " +  Main.level_number;
    }
}
