using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    bool IsEnd = false;
    [SerializeField]
    float m_correctSpeed =1.5f;
    [SerializeField]
    float m_powerTime = 5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<PlayerController2D>();
            Debug.Log("speedup");
            Destroy(this.gameObject);
        }
    }

    float SetCorrectionNum()
    {
        StartCoroutine(SetTimer());
        if (IsEnd)
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
