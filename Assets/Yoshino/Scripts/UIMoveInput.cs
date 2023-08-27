using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMoveInput : MonoBehaviour
{
    [SerializeField] private LifeUIManager m_lifeUIManager = null;
    // Start is called before the first frame update
    void Start()
    {
        if (m_lifeUIManager == null)
        {
            m_lifeUIManager = FindObjectOfType<LifeUIManager>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            m_lifeUIManager.LifeUISetUp();
        }
    }
}
