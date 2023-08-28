using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animator_Play : MonoBehaviour
{
    public Animator anim;
    public string st;
    [Header("1“x‚Ì‚Ý")] public bool oneplay;
    private bool isplay;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (oneplay)
        {
            if (!isplay)
            {
                anim.Play(st);
                isplay = true;
            }
        }
        else
        {
            anim.Play(st);
        }
    }
}
