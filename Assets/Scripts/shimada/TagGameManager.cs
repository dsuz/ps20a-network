using UnityEngine;
using UnityEngine.UI;
// Photon 用の名前空間を参照する
using Photon.Pun;

/// <summary>
/// 鬼ごっこゲームのゲーム状態を制御するコンポーネント
/// 現状では部屋がいっぱいにならないとゲームが始まらない
/// </summary>
public class TagGameManager : MonoBehaviour
{
    /// <summary>画面に文字を表示するための Text</summary>
    [SerializeField] Text m_console = null;
    /// <summary>ゲームが始まっているかを管理するフラグ</summary>
    bool m_isGameStarted = false;
    [SerializeField] int m_maxPlayerCount = 4;
    [SerializeField] int m_startPlayerCount = 1;
    [SerializeField] Button m_startButton = null;

    void Start()
    {
        m_console.text = "Wait for other players...";
        m_startButton.gameObject.SetActive(false);
    }

    void Update()
    {
        if (!PhotonNetwork.InRoom) return;

        if (!m_isGameStarted)
        {
            m_console.text = $"{PhotonNetwork.CurrentRoom.PlayerCount} / {m_maxPlayerCount}"; //部屋の最大人数と現在の人数を表示
                                                                                              // 入室していて、まだゲームが始まっていない状態で、人数が揃った時
            if (PhotonNetwork.CurrentRoom.PlayerCount >= m_startPlayerCount)
            {
                m_startButton.gameObject.SetActive(true);//STARTボタンを有効にする
            }
        }
    }

    /// <summary>
    /// ボタンから呼ばれる
    /// </summary>
    public void PlayStart()
    {
        m_startButton.gameObject.SetActive(false);//STARTボタンを無効にする

        // ゲームを開始する
        m_console.text = "Game Start!";
        Invoke("ClearConsole", 1.5f);   // 1.5秒後に表示を消す
        m_isGameStarted = true; // ゲーム開始フラグを立てる

        // マスタークライアントにより、ランダムに鬼を決める
        if (PhotonNetwork.IsMasterClient)
        {
            PlayerController2D[] players = GameObject.FindObjectsOfType<PlayerController2D>();
            PhotonView view = players[Random.Range(0, players.Length)].GetComponent<PhotonView>();
            view.RPC("Tag", RpcTarget.All);
        }
    }

    void ClearConsole()
    {
        m_console.text = "";
    }
}
