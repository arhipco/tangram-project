using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow : MonoBehaviour
{
   public int f_rotation;
   public float r_rotation;

   void Start()
   {
      r_rotation = this.transform.eulerAngles.z;
      f_rotation = (int)Mathf.Round(r_rotation / 45.0f);
   }
}
