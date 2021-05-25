using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 制限時間を管理する
/// </summary>
public class TimeManager : MonoBehaviour
{
    /// <summary>制限時間 </summary>
    [SerializeField] int m_limitSecond = 90;
    /// <summary>ゲーム内の経過時間 </summary>
    float m_time = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        m_time += Time.deltaTime;
        m_limitSecond -= (int)m_time;
    }
}
