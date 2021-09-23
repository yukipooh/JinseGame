using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class TitleUI : MonoBehaviour
{
    [SerializeField] Text inputText;
    [SerializeField] Button enterButton;    //決定ボタン
    public static string playerName = "Tanaka";

    // Start is called before the first frame update
    void Start()
    {
        enterButton.onClick.AddListener(() => Enter());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Enter(){
        if(inputText.text != ""){
            playerName = inputText.text;
            SceneManager.LoadScene("SampleScene");
        }
    }
}
