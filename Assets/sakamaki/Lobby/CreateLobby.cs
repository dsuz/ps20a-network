using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class CreateLobby : MonoBehaviourPunCallbacks
{
    /// <summary>スタートボタン</summary>
    [SerializeField] GameObject m_startButton;
    /// <summary>部屋設定の選択メニュー</summary>
    [SerializeField] GameObject m_choseButtons;
    /// <summary>部屋作成メニュー</summary>
    [SerializeField] GameObject m_createMenu;
    /// <summary>部屋参加メニュー</summary>
    [SerializeField] GameObject m_joinMenu;

    private State m_state = State.Title;

    public enum State
    {
        Title,
        Chose,
        CreateMenu,
        JoinMenu
    }

    private void Start()
    {
        Connect("1,0");
    }

    public void TransitionState(State state)
    {
        m_state = state;
        switch (m_state)
        {
            case State.Title:
                break;
            case State.Chose:
                m_startButton.SetActive(false);
                m_choseButtons.SetActive(true);
                break;
            case State.CreateMenu:
                m_choseButtons.SetActive(false);
                m_createMenu.SetActive(true);
                break;
            case State.JoinMenu:
                m_choseButtons.SetActive(false);
                m_joinMenu.SetActive(true);
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Photonに接続する
    /// </summary>
    private void Connect(string gameVersion)
    {
        if (PhotonNetwork.IsConnected == false)
        {
            PhotonNetwork.GameVersion = gameVersion;    // 同じバージョンを指定したもの同士が接続できる
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    /// <summary>
    /// 部屋を作成
    /// </summary>
    /// <param name="roomName">部屋名</param>
    /// <param name="password">パスワード</param>
    public void CreateRoom(string roomName, string password)
    {
        if (PhotonNetwork.IsConnected)
        {
            string str;
            RoomOptions roomOptions = new RoomOptions();
            roomOptions.MaxPlayers = 4;
            if (password != null)
            {
                str = $"{roomName}_{password}";
                roomOptions.IsVisible = false;
                Debug.Log("パスワードを設定した部屋を作成");
            }
            else
            {
                str = roomName;
                roomOptions.IsVisible = true;
                Debug.Log("公開された部屋を作成");
            }
            PhotonNetwork.CreateRoom(roomName, roomOptions); // ルーム名に null を指定するとランダムなルーム名を付ける
        }
    }

    /// <summary>部屋を作成した時に呼ばれる</summary>
    public override void OnCreatedRoom()
    {
        Debug.Log("部屋を作成");
    }
}
