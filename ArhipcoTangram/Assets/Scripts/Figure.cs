using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Figure : MonoBehaviour
{  
    public int f_rotation; // values of f_rotation from 0 to 7
    public bool mouse_on_me = false;  
    // статус фигуры: dropped (фигура уже пару кадров лежит проверенная), moving, falling (момент бросания), installed, shadow (фигура является тенью)
    public string figureState = "dropped";  
    //public string _id; // bt - big triangle; st - small triangle; mt - mid triangle; par- parall, sq - square
 
    public bool IsRightPlace = false;
    private Vector3 shadowPlace;

    void Start()
    {  
        Main mParent = FindObjectOfType<Main>();
         int num = Random.Range(0, mParent.colorBank.Length); 
        SpriteRenderer s = GetComponent<SpriteRenderer>();
        s.color = mParent.colorBank[num]; 
  
        f_rotation = (int)Mathf.Round(this.transform.eulerAngles.z / 45.0f);  
    }
    public void SetRot(int _rot)
    {
        this.f_rotation = _rot;// f_rotation variable to real f_rotation figure    
        this.gameObject.transform.rotation =  Quaternion.Euler(0,0, 45*_rot);
    }
      
    public void TurnFigure()
    { 
        if (figureState != "installed")
        {
            f_rotation += 1;
            if (f_rotation > 7)  
                f_rotation = 0; 

            SetRot(f_rotation); 
        }
    } 

    void OnTriggerExit2D(Collider2D other)
    {
        IsRightPlace = false; 
    }
    void OnTriggerStay2D(Collider2D other)
    {  
        if (figureState != "installed")
        {
            var otherLayer = LayerMask.LayerToName(other.gameObject.layer); 
            var otherTag = other.gameObject.tag;
            var thisTag = this.gameObject.tag; 
            var trot = this.f_rotation;
            var orot = Mathf.Round(other.gameObject.transform.eulerAngles.z / 45);   

            IsRightPlace = false; 
            shadowPlace = other.gameObject.transform.position; 
            // // если имена идентификаторов совпадают  
            if (thisTag == otherTag && otherLayer == "Shadows")
            {   
                if (trot == orot)// если углы поворота совпадают  
                    IsRightPlace = true; 
                else if (thisTag == "sq" 
                        && ((trot % 2 == 0 && orot % 2 == 0) || ((trot % 2 != 0 && orot % 2 != 0) ))) // проверка совпадения по чётности числа 
                        IsRightPlace = true;   
                else if (thisTag == "pr" && (trot - 4) == orot) // углы не совпадают и не квадрат,4 это 180 градусов, повторяется силуэт параллелограмма 
                        IsRightPlace = true;   
            }    
            if (IsRightPlace)
            {
                float dist =   Vector3.Distance(other.gameObject.transform.position, transform.position); 
                if (dist > 3.001f)
                    IsRightPlace = false;
            }   
        } 
    }
 
    public void Update()
    {  
            if (figureState == "moving" && figureState != "installed")
            {
                Main.mousePos = Input.mousePosition;
                Main.mousePos = Camera.main.ScreenToWorldPoint(Main.mousePos);
                this.gameObject.transform.localPosition = new Vector3(Main.mousePos.x - Main.startPosX,
                    Main.mousePos.y - Main.startPosY, -3.0f); 
            }
                    
            if (figureState == "falling" && IsRightPlace == true)
            { 
                    figureState = "installed"; 
                    Main.installedCount++; 
                    this.transform.position =  shadowPlace;// магнитим на место   
                    this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -2);
                    IsRightPlace = false;
            }
            else if (figureState == "falling" && IsRightPlace == false)
            { 
                figureState = "dropped";
            }    
        
    }
 
    public void OnMouseDown()
    {   
        if (figureState != "installed")
        {
            mouse_on_me = true; 
            Main.mousePos = Input.mousePosition;
            Main.mousePos = Camera.main.ScreenToWorldPoint(Main.mousePos);
            Main.startPosX = Main.mousePos.x - this.transform.localPosition.x;
            Main.startPosY = Main.mousePos.y - this.transform.localPosition.y; 

            Main.timerStart = Time.time; 
            figureState = "moving"; 
        }     
    }
 
    public void OnMouseUp()
    {  
            mouse_on_me = false; 
            
            if (Time.time - Main.timerStart < 0.3f && IsRightPlace == false) 
                TurnFigure();  
            if (figureState == "moving")   
                figureState = "falling";    

    }
}

