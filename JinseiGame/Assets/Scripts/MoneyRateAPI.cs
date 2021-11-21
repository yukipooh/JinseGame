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

    // Update is called once per frame
    void Update()
    {
        
    }

    public static IEnumerator Fetch(){
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
        }
    }
}

public class MoneyRateFromUSDObject {
    public string result;
    public string bashCode;
    public string update;
    public string source;
    public string API_URL;
    public float ARS;
    public float UYU;
    public float ANG;
    public float CAD;
    public float CUP;
    public float GTQ;
    public float KYD;
    public float CRC;
    public float COP;
    public float JMD;
    public float CLP;
    public float DOP;
    public float TTD;
    public float NIO;
    public float HTG;
    public float PAB;
    public float BSD;
    public float BMD;
    public float PYG;
    public float BBD;
    public float BRL;
    public float VES;
    public float BZD;
    public float PEN;
    public float BOB;
    public float HNL;
    public float MXN;
    public float XCD;
    public float XPF;
    public float INR;
    public float IDR;
    public float AUD;
    public float KHR;
    public float SGD;
    public float LKR;
    public float SCR;
    public float THB;
    public float NZD;
    public float NPR;
    public float PKR;
    public float BDT;
    public float FJD;
    public float PHP;
    public float BND;
    public float VND;
    public float MOP;
    public float MYR;
    public float MMK;
    public float LAK;
    public float KRW;
    public float HKD;
    public float TWD;
    public float CNY;
    public float JPY;
    public float ISK;
    public float ALL;
    public float GBP;
    public float UAH;
    public float HRK;
    public float CHF;
    public float SEK;
    public float RSD;
    public float CZK;
    public float DKK;
    public float NOK;
    public float HUF;
    public float BGn;
    public float BYN;
    public float PLN;
    public float MDL;
    public float EUR;
    public float RON;
    public float RUB;
    public float AZN;
    public float AED;
    public float AMD;
    public float YER;
    public float ILS;
    public float IQD;
    public float IRR;
    public float UZS;
    public float OMR;
    public float KZT;
    public float QAR;
    public float KGS;
    public float KWD;
    public float GEL;
    public float SAR;
    public float TMT;
    public float TRY;
    public float BHD;
    public float JOD;
    public float LBP;

}
