using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class DisplayName : MonoBehaviour
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
    /// <summary>名前設定の説明のテキスト</summary>
    [SerializeField] GameObject _desctiptionImage;
    /// <summary>名前の入力の可否</summary>
    bool _inputName = false;

    PhotonView _view;

    private void Start()
    {
        _view = GetComponent<PhotonView>();
    }

    private void Update()
    {
        _nameText.rectTransform.position = new Vector3(_player.transform.position.x, _player.transform.position.y + 1.5f, _player.transform.position.z);
        if (_view.IsMine)
        {
            if (!_inputName)
            {
                _inputFieldObject.SetActive(true);
            }
        }
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
}
