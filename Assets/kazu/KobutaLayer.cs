using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KobutaLayer : MonoBehaviour
{
    public SpriteRenderer Red_ren;
    public SpriteRenderer Blue_ren;
    public SpriteRenderer Green_ren;
    public MKPlayerKobuta Red,Blue,Green;
    public Sprite Red_Kobuta_Sp;
    public Sprite Red_Normal_Sp;
    public Sprite Blue_Kobuta_Sp;
    public Sprite Blue_Normal_Sp;
    public Sprite Green_Kobuta_Sp;
    public Sprite Green_Normal_Sp;
    public MKPlayer mk;
     
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(mk.Kobuta==Red)
        {
            Red_ren.sprite = Red_Kobuta_Sp;
            Green_ren.sprite = Green_Normal_Sp;
            Blue_ren.sprite = Blue_Normal_Sp;

            Red_ren.sortingOrder = 1;
            Green_ren.sortingOrder = 0;
            Blue_ren.sortingOrder = 0;
        }
        else if(mk.Kobuta==Blue)
        {
            Red_ren.sprite = Red_Normal_Sp;
            Green_ren.sprite = Green_Normal_Sp;
            Blue_ren.sprite = Blue_Kobuta_Sp;

            Red_ren.sortingOrder = 0;
            Green_ren.sortingOrder = 0;
            Blue_ren.sortingOrder = 1;
        }
        else if(mk.Kobuta==Green)
        {
            Red_ren.sprite = Red_Normal_Sp;
            Green_ren.sprite = Green_Kobuta_Sp;
            Blue_ren.sprite = Blue_Normal_Sp;
            Red_ren.sortingOrder = 0;
            Green_ren.sortingOrder = 1;
            Blue_ren.sortingOrder = 0;
        }
   
    }
}
