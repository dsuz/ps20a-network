using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class CreateLobby : MonoBehaviourPunCallbacks
{

    /// <summary>
    /// Photonに接続する
    /// </summary>
    public void Connect()
    {
        if (PhotonNetwork.IsConnected == false)
        {
            PhotonNetwork.GameVersion = "1.0";    // 同じバージョンを指定したもの同士が接続できる
            PhotonNetwork.ConnectUsingSettings();

        }
    }

    /// <summary>
    /// MasterSaeverに接続
    /// </summary>
    public override void OnConnectedToMaster()
    {
        if (PhotonNetwork.IsConnected)
        {
            Debug.Log("MasterSaeverに接続した");
            PhotonNetwork.JoinRandomRoom();
        }
    }

    /// <summary>
    /// 既存の部屋がなかった場合に、部屋を作成する
    /// </summary>
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("既存の部屋がなかった場合に、部屋を作成する");
        CreateRoom();
    }

    public override void OnJoinedRoom()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {
            PhotonNetwork.LoadLevel("MasterGameScene");
        }
    }

    /// <summary>
    /// 部屋を作成
    /// </summary>
    public void CreateRoom()
    {
        if (PhotonNetwork.IsConnected)
        {
            RoomOptions roomOptions = new RoomOptions();
            roomOptions.MaxPlayers = 4;

            PhotonNetwork.CreateRoom(null, roomOptions); // ルーム名に null を指定するとランダムなルーム名を付ける
        }
    }

    /// <summary>部屋を作成した時に呼ばれる</summary>
    public override void OnCreatedRoom()
    {
        Debug.Log("部屋を作成");
    }
}
