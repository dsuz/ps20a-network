using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 制限時間を管理する
/// </summary>
public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance { get; private set;}
    /// <summary>制限時間 </summary>
    [SerializeField] float m_limitSecond = 90;
    /// <summary>ゲーム内の残り時間 </summary>
    float m_time = 0;
    /// <summary>残り時間を表示する </summary>
    [SerializeField]Text m_timeText = null;
    /// <summary>タイムアップを表示する </summary>
    [SerializeField] Text m_timeUpText = null;
    // Start is called before the first frame update
    void Start()
    {
        m_timeUpText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        m_time += Time.deltaTime;
        m_limitSecond -= Time.deltaTime;
        if (m_limitSecond <= 0)
        {
            ShowTimeUp();
            m_limitSecond = 0;
        }
        m_timeText.text = "残り時間：" + Mathf.FloorToInt(m_limitSecond).ToString();
        
    }

    public void ShowTimeUp()
    {
        m_timeUpText.text = "TimeUp!!";
        m_timeUpText.gameObject.SetActive(true);
    }
}
