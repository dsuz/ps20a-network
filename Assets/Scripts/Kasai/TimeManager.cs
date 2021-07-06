
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

/// <summary>
/// 制限時間を管理する
/// </summary>
public class TimeManager : MonoBehaviour, IPunObservable
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
    bool IsGameStart = false;

    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        m_timeUpText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (IsGameStart)
        {
            Debug.Log("GameStart!!");
            m_limitSecond -= Time.deltaTime;
            if (m_limitSecond <= 0)
            {
                ShowTimeUp();
                m_limitSecond = 0;
            }
        }
    }

    [PunRPC]
    public void ShowTimeUp()
    {
        m_timeUpText.text = "TimeUp!!";
        m_timeUpText.gameObject.SetActive(true);
    }

    [PunRPC]
    public void DisplayTime()
    {
        m_timeText.text = "残り時間：" + Mathf.FloorToInt(m_limitSecond).ToString();
    }

    [PunRPC]
    public void StartTimer()
    {
        IsGameStart = true;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        // オーナーの場合
        if (stream.IsWriting)
        {
            stream.SendNext(m_limitSecond);
        }
        // オーナー以外の場合
        else
        {
            m_limitSecond = (float)stream.ReceiveNext();
        }
    }
}
