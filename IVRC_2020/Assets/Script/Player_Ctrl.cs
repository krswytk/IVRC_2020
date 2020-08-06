using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Ctrl : MonoBehaviour
{
    public enum Setting//ボタン操作の設定
    {
        AUTO,
        W_S_BUTTON
    }
    private Rigidbody rb;//  Rigidbodyを使うための変数
    private bool Grounded;//  地面に着地しているか判定する変数
    bool jump = false;//ジャンプするかどうか
    bool forward = false;//前進するかどうか
    [SerializeField] private float Jumppower;//  ジャンプ力
    [SerializeField] private float Forwardpower;//前進する力
    [SerializeField] private float Descendingpower;//下降していく力
    [SerializeField] private Vector3 velocity;              // 移動方向
    public Setting setting;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();//  rbにRigidbodyの値を代入する
    }

    // Update is called once per frame
    void Update()
    {
        if (setting == Setting.AUTO)//自動で前に前進する設定
        {
            if (Input.GetKeyDown(KeyCode.Space))//スペースキーを押したらFixedUpdateでジャンプする
            {
                jump = true;
                Debug.Log("Jump");
                Grounded = false;//  Groundedをfalseにする
            }


            if (Grounded == false)//  もし、Groundedがfalseなら、FixedUpdateで前進する
            {
                forward = true;
                Debug.Log("前進");
            }
        }
        else if (setting == Setting.W_S_BUTTON)//WとSボタンで動く
        {

            if (Input.GetKeyDown(KeyCode.Space))//スペースキーを押したらFixedUpdateでジャンプする
            {
                jump = true;
                Debug.Log("Jump");
                Grounded = false;//  Groundedをfalseにする
            }
            W_S_Move();//移動の処理
        }


    }

    private void FixedUpdate()//AddForceの処理をここで行う
    {
        if (jump == true)//ジャンプする
        {
            rb.AddForce(Vector3.up * Jumppower, ForceMode.Impulse);//  上にJumpPower分力をかける
            jump = false;
        }

        if (forward == true)//前進する
        {
            this.rb.velocity = new Vector3(0, -Descendingpower, Forwardpower);
            forward = false;
        }

    }

    private void W_S_Move()//wとsボタンで前後に動く処理
    {
        velocity = Vector3.zero;
        if (Input.GetKey(KeyCode.W))//前進
        {
            velocity.z += 1;
        }
        if (Input.GetKey(KeyCode.S))//後退
        {
            velocity.z -= 1;
        }
        // 速度ベクトルの長さを1秒でmoveSpeedだけ進むように調整します
        velocity = velocity.normalized * Forwardpower * Time.deltaTime;

        // いずれかの方向に移動している場合
        if (velocity.magnitude > 0)
        {
            // プレイヤーの位置(transform.position)の更新
            // 移動方向ベクトル(velocity)を足し込みます
            transform.position += velocity;
        }

    }

    void OnCollisionEnter(Collision other)//  地面に触れた時の処理
    {
        if (other.gameObject.tag == "ground")//  もしGroundというタグがついたオブジェクトに触れたら、
        {
            Grounded = true;//  Groundedをtrueにする
        }
    }
}
