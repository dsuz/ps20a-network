using UnityEngine;
// Photon 用の名前空間を参照する
using Photon.Pun;

/// <summary>
/// Tag ゲームのプレイヤーを制御するコンポーネント
/// ダッシュした直後はしばらくダッシュできない
/// 鬼が移った後はしばらく鬼を移すことができない
/// </summary>
[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer))]
public class PlayerController2D : MonoBehaviour
{
    /// <summary>動く力</summary>
    [SerializeField] float m_movePower = 10f;
    /// <summary>ダッシュ力</summary>
    [SerializeField] float m_dashPower = 50f;
    /// <summary>一度ダッシュした後、次にダッシュできるまでの秒数</summary>
    [SerializeField] float m_dashPeriod = 3f;
    /// <summary>スピード補正</summary>
    float m_correctSpeed = 1;
    public bool CanInput = false;
    ///// <summary>鬼の時の色</summary>
    //[SerializeField] Color m_taggedColor = Color.red;
    ///// <summary>鬼ではない時の色</summary>
    //[SerializeField] Color m_normalColor = Color.white;
    /// <summary>鬼を移された直後、鬼を移せない猶予期間（秒）</summary>
    [SerializeField] float m_gracePeriod = 1.5f;

    Rigidbody2D m_rb = null;
    PhotonView m_view = null;
    //SpriteRenderer m_sprite = null;

    [SerializeField] public GameObject _tagMark = null;

    /// <summary>ダッシュの間隔を計るためのタイマー</summary>
    float m_dashTimer = 0f;
    /// <summary>猶予期間を計るためのタイマー</summary>
    float m_graceTimer = 0f;
    /// <summary>補正期間を計るためのタイマー</summary>
    float m_powerTimer;

    ItemManager itemManager;

    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_view = GetComponent<PhotonView>();
        m_dashTimer = m_dashPeriod;
        //m_sprite = GetComponent<SpriteRenderer>();
        //_tagMark = this.transform.Find("TagMark").gameObject;
        //_tagMark.SetActive(false);
    }

    void Update()
    {
        if (!m_view || !m_view.IsMine) return;      // 自分が生成したものだけ処理する

        Move();
        Rotate();
        // ダッシュの処理
        if (m_dashTimer < m_dashPeriod)
        {
            m_dashTimer += Time.deltaTime;
        }
        else if (Input.GetButtonDown("Jump"))
        {
            // ダッシュタイマーが溜まった時に Jump ボタンを押すとダッシュする
            Dash();
            m_dashTimer = 0f;
        }

        // 猶予期間のタイマー処理
        if (m_graceTimer < m_gracePeriod)
        {
            m_graceTimer += Time.deltaTime;
        }

        if (m_powerTimer > 0)
        {
            m_correctSpeed = itemManager.SetCorrectionNum();
            m_powerTimer -= Time.deltaTime;
        }
        else
        {
            m_correctSpeed = 1;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // 自分が生成したオブジェクトが鬼であり、かつ猶予期間でなく衝突相手がプレイヤーである時
        if (m_view && m_view.IsMine && _tagMark.activeSelf
            && m_graceTimer >= m_gracePeriod && collision.gameObject.CompareTag("Player"))
        {
            // 衝突相手を鬼にする
            PhotonView view = collision.gameObject.GetComponent<PhotonView>();

            if (view)
            {
                view.RPC("Tag", RpcTarget.All);
            }

            // 自分を鬼ではなくする
            m_view.RPC("Release", RpcTarget.All);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject);
        if (collision.tag == "Item")
        {
            itemManager = collision.GetComponentInParent<ItemManager>();
            m_powerTimer = itemManager.m_powerTime;
            Debug.Log(itemManager);
            Destroy(collision.gameObject);
        }
    }

    /// <summary>
    /// ダッシュする
    /// </summary>
    void Dash()
    {
        m_rb.AddForce(this.transform.up * m_dashPower, ForceMode2D.Impulse);
    }

    /// <summary>
    /// プレイヤーを進行方向に向ける
    /// </summary>
    void Rotate()
    {
        this.transform.up = m_rb.velocity;
    }

    /// <summary>
    /// 上下左右にキャラクターを動かす
    /// </summary>
    void Move()
    {
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");

        Vector2 dir = (Vector2.up * v + Vector2.right * h).normalized;

        if (dir != Vector2.zero)
        {
            m_rb.AddForce(dir * m_movePower * m_correctSpeed, ForceMode2D.Force);
        }
    }

    /// <summary>
    /// つかまった時に呼び出す
    /// </summary>
    [PunRPC]
    public void Tag()
    {
        _tagMark.SetActive(true);
        m_graceTimer = 0f;
    }

    /// <summary>
    /// 鬼でなくなる時に呼び出す
    /// </summary>
    [PunRPC]
    public void Release()
    {
        _tagMark.SetActive(false);
    }

    /// <summary>
    /// 速度補正値を受け取る
    /// </summary>
    /// <param name="num">補正値</param>
    public void RecieveCorrectNum(float num)
    {
        m_correctSpeed = num;
    }
}
