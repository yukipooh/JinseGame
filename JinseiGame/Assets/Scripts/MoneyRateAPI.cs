using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System;

public class MoneyRateAPI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(nameof(Fetch));
    }

    public IEnumerator Fetch(){
        // ユーロポンド,ドル円,豪ドルドル を取得
        string url = "http://api.aoikujira.com/kawase/json/usd";

        UnityWebRequest request = UnityWebRequest.Get (url);

        yield return request.Send();

        if (request.isHttpError || request.isNetworkError)
        {
            //エラー確認
            Debug.Log(request.error);
        }
        else
        {
            string jsonText = request.downloadHandler.text;
            MoneyRateFromUSDObject moneyObject = JsonUtility.FromJson<MoneyRateFromUSDObject>(jsonText);
            DateTime update = DateTime.ParseExact(moneyObject.update, "yyyy-MM-dd hh:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
            Debug.Log($"{update.Year}年{update.Month}月{update.Day}日{update.Hour}時{update.Minute}分{update.Second}秒におけるUSD-JPY為替レート");
            // Debug.Log(moneyObject.update);
            Debug.Log("1ドル = " + moneyObject.JPY.ToString() + "円");

            GameManager.moneyObject = moneyObject;
            GameManager.updateTime = update;
        }
    }
}

public class MoneyRateFromUSDObject {
    public string result;
    public string bashCode;
    public string update;
    public string source;
    public string API_URL;
    public float JPY;

}
