using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class DisplayName : MonoBehaviourPunCallbacks
{
    /// <summary>プレイヤーゲームオブジェクト</summary>
    [SerializeField] GameObject _player = null;
    /// <summary>インプットフィールドのオブジェクト</summary>
    [SerializeField] GameObject _inputFieldObject;
    /// <summary>キャンバス</summary>
    [SerializeField] Canvas _inputCanvas;
    /// <summary>名前を入力させるインプットフィールド</summary>
    [SerializeField] InputField _inputField;
    /// <summary>表示するテキスト</summary>
    [SerializeField] Text _nameText;
    /// <summary>名前のテキスト</summary>
    [SerializeField] string _strName;
    /// <summary>名前設定の説明のテキスト</summary>
    [SerializeField] GameObject _desctiptionImage;
    /// <summary>名前の入力完了</summary>
    bool _inputName = false;

    PhotonView _view;

    private void Start()
    {
        _view = GetComponent<PhotonView>();
    }

   

    private void Update()
    {
        if (!_view || !_view.IsMine) return;

        if (!_inputName && !_inputFieldObject.activeSelf)
        {
            Debug.Log("Begin Input Name");
            _inputFieldObject.SetActive(true);
        }

        _nameText.rectTransform.position = new Vector3(_player.transform.position.x, _player.transform.position.y + 1.5f, _player.transform.position.z);
    }

    /// <summary>
    ///　インプットフィールドの入力の制御(OnEndEditにアサインする)
    /// </summary>
    public void DisPlayName()
    {
        if (_inputField.text.Length > 0 && _inputField.text.Length <= 5)
        {
            _nameText.text = _inputField.text;
            _inputField.enabled = false;
            _inputCanvas.enabled = false;
            _inputName = true;
            _strName = _nameText.text;
            _view.RPC("SetName", RpcTarget.Others, _strName);
        }
        else
        {
            _inputField.text = "名前が適切ではありません";
        }
    }
    /// <summary>
    /// インプットフィールドの説明(InputFieldにEventTriggerを追加して、(PointerEnterとPointerExitにアサインする)
    /// </summary>
    public void Desctiption()
    {
        if (!_desctiptionImage.activeSelf)
        {
            _desctiptionImage.SetActive(true);
        }
        else
        {
            _desctiptionImage.SetActive(false);
        }
    }

    /// <summary>
    /// 名前を同期する
    /// </summary>
    /// <param name="name">設定する名前</param>
    [PunRPC]
    void SetName(string name)
    {
        _strName = name;
        RefreshNameText();
    }
    /// <summary>
    /// 名前の更新
    /// </summary>
    void RefreshNameText()
    {
        _nameText.text = _strName;
    }

    /// <summary>
    /// 他のユーザーが参加したときに呼ばれる関数
    /// </summary>
    /// <param name="newPlayer"></param>
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        _view.RPC("SetName", RpcTarget.Others, _strName);
    }
}
