using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    bool IsEnd = true;
    [SerializeField]
    public float m_correctSpeed =1.5f;
    [SerializeField]
    public float m_powerTime = 5f;
    [SerializeField]
    GameObject m_item = null;
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
        return m_correctSpeed;
    }

    IEnumerator SetTimer()
    {
        IsEnd = false;
        yield return new WaitForSeconds(m_powerTime);
        IsEnd = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!m_item) return;

        if (collision.CompareTag("Player"))
        {
            Debug.Log("Hit");
            m_item.SetActive(false);
        }
    }
}
