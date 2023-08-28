using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObjectShake : MonoBehaviour
{
    
    [Header("揺れスイッチ")]public bool istime;//istimeをtrueにすると、揺れが始まる。自動でFalseになる
    [Header("揺れ時間")]public float time=1;
    private float timer;
    [Header("揺れスピード")]public float Shakespeed;
    private Vector2 pos;
    private float timer2;
    // Start is called before the first frame update
    void Start()
    {
        
        pos=transform.localPosition;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (istime)
        {
            timer = time;
            istime = false;
        }
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            transform.localPosition = pos;
        }
        else
        {
            if(timer2<=0)
            {
                transform.localPosition = new Vector2(pos.x + ((Mathf.PerlinNoise(Random.Range(1, 0f), Random.Range(1, 0)) - 0.5f) * 2 * Shakespeed), pos.y + ((Mathf.PerlinNoise(Random.Range(-1, 0), Random.Range(-1, 0)) - 0.5f) * 2 * Shakespeed));
                timer2 = 0.01f;
            }
            else
            {
                timer2 -= Time.deltaTime;
            }
            
            
        }
       
    }
   
}
