using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObjectShake : MonoBehaviour
{
    
    [Header("�h��X�C�b�`")]public bool istime;//istime��true�ɂ���ƁA�h�ꂪ�n�܂�B������False�ɂȂ�
    [Header("�h�ꎞ��")]public float time=1;
    private float timer;
    [Header("�h��X�s�[�h")]public float Shakespeed;
    private Vector3 pos;
    private float timer2;
    // Start is called before the first frame update
    void Start()
    {
        
        pos=transform.localPosition;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (istime&&timer<=0)
        {
            timer = time;
            pos = transform.localPosition;
        }
        timer -= Time.deltaTime;
        if (timer < 0&&istime)
        {
            istime = false;
            transform.localPosition = pos;
        }
        else
        {
            if(timer2<=0)
            {
                if(istime)
                {
                    transform.localPosition = new Vector3(pos.x + ((Mathf.PerlinNoise(Random.Range(1, 0f), Random.Range(1, 0)) - 0.5f) * 2 * Shakespeed), pos.y + ((Mathf.PerlinNoise(Random.Range(-1, 0), Random.Range(-1, 0)) - 0.5f) * 2 * Shakespeed), pos.z);
                    timer2 = 0.01f;
                }
               
            }
            else
            {
                timer2 -= Time.deltaTime;
            }
            
            
        }
       
    }
   
}
