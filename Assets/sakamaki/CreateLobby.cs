using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class CreateLobby : MonoBehaviourPunCallbacks
{
    [SerializeField] InputField m_inputRoomName;
   
    private string m_roomName = null;

    private void Start()
    {
        Connect("1,0");
    }

    public void  CreateMyRoom()
    {
        m_roomName = m_inputRoomName.text.ToString();
        CreateRandomRoom();
        SceneManager.LoadScene("Game_Sakamaki");
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
    /// 指定した名前のルームを作って参加する
    /// </summary>
    private void CreateRandomRoom()
    {
        if (PhotonNetwork.IsConnected)
        {
            RoomOptions roomOptions = new RoomOptions();
            roomOptions.IsVisible = false;   // 誰でも参加できないようにする
            /* **************************************************
             * spawPositions の配列長を最大プレイ人数とする。
             * 無料版では最大20まで指定できる。
             * MaxPlayers の型は byte なのでキャストしている。
             * MaxPlayers の型が byte である理由はおそらく1ルームのプレイ人数を255人に制限したいためでしょう。
             * **************************************************/
            roomOptions.MaxPlayers = 4;
            PhotonNetwork.CreateRoom(m_roomName, roomOptions); // ルーム名に null を指定するとランダムなルーム名を付ける
           // Debug.Log()
        }
    }

    /// <summary>部屋を作成した時</summary>
    public override void OnCreatedRoom()
    {
        Debug.Log("部屋を作成");
    }
}
