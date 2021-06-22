using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    bool IsEnd = true;
    [SerializeField]
    float m_correctSpeed =1.5f;
    [SerializeField]
    float m_powerTime = 5f;
    PlayerController2DKasai m_player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public float SetCorrectionNum()
    {
        StartCoroutine(SetTimer());
        if (!IsEnd)
        {
            return m_correctSpeed;
        }
        else
        {
            return 1f;
        }
    }

    IEnumerator SetTimer()
    {
        IsEnd = false;
        yield return new WaitForSeconds(m_powerTime);
        IsEnd = true;
    }


}
