using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] Text playerNameText;
    [SerializeField] Text currentMoneyText;
    [SerializeField] Text descriptionText;
    [SerializeField] GameObject rouletteForMove;
    [SerializeField] GameObject rouletteForDecision;

    public void Initialize(){

    }

    public void DismissAllDefaultUI(bool name = false, bool money = false, bool description = false, bool roulette = false){
        //通常の画面で表示されているUIを全て非表示に
        playerNameText.gameObject.SetActive(name);
        currentMoneyText.gameObject.SetActive(money);
        descriptionText.gameObject.SetActive(description);
        rouletteForMove.SetActive(roulette);
    }

    public void ShowAllDefaultUI(bool name = true, bool money = true, bool description = true, bool roulette = true){
        //通常の画面で表示されているUIを全て表示
        playerNameText.gameObject.SetActive(name);
        currentMoneyText.gameObject.SetActive(money);
        descriptionText.gameObject.SetActive(description);
        rouletteForMove.SetActive(roulette);
    }
}
