using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class 音符生成器 : MonoBehaviour
{
    public TextAsset 譜面檔案;             // 拖進 .xml 檔（TextAsset 格式）
    public InputControl 攻擊控制器;        // 拖入 InputControl 腳本的物件
    public AudioSource 音樂來源;           // 播放 MP3 用的 AudioSource
    public float 判定容錯 = 0.2f;          // 容錯時間（秒）

    private List<節奏資訊> 節奏列表 = new List<節奏資訊>();

    void Start()
    {
        讀取譜面資料();
    }

    public void 嘗試攻擊(int 玩家方向)
    {
        float 現在時間 = 音樂來源.time;

        foreach (var 節奏點 in 節奏列表)
        {
            if (節奏點.已判定) continue;

            float 誤差 = Mathf.Abs(節奏點.時間 - 現在時間);
            if (誤差 <= 判定容錯 && 節奏點.方向 == 玩家方向)
            {
                攻擊控制器.CreateBeat(玩家方向); // 命中才攻擊
                節奏點.已判定 = true;
                return;
            }
        }

        // 若無符合的節奏點，可加上 Miss 效果
        Debug.Log("Miss！");
    }

    void 讀取譜面資料()
    {
        XmlDocument 文件 = new XmlDocument();
        文件.LoadXml(譜面檔案.text);

        XmlNodeList 音符節點 = 文件.GetElementsByTagName("音符");
        foreach (XmlNode 音符 in 音符節點)
        {
            float 時間 = float.Parse(音符.Attributes["時間"].Value);
            string 方向文字 = 音符.Attributes["方向"].Value;

            int 方向 = 方向文字 switch
            {
                "上" => 0,
                "下" => 1,
                "左" => 2,
                "右" => 3,
                _ => -1
            };

            節奏資訊 新音符 = new 節奏資訊 { 時間 = 時間, 方向 = 方向, 已判定 = false };
            節奏列表.Add(新音符);
        }
    }
}

public class 節奏資訊
{
    public float 時間;
    public int 方向;        // 0: 上, 1: 下, 2: 左, 3: 右
    public bool 已判定;    // 是否已被判定過（避免重複判定）
}