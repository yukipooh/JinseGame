using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PressEnterText : MonoBehaviour
{
    [SerializeField] GameObject alertText;
    public static bool isPressedEnter = false;
    
    /// <summary>
    /// アラートを点滅させるアニメーション
    /// </summary>
    /// <returns></returns>
    public IEnumerator AlertAnimation(){
        while(!isPressedEnter){
            alertText.SetActive(true);
            yield return new WaitForSeconds(1);
            alertText.SetActive(false);
            yield return new WaitForSeconds(0.25f);
        }
        alertText.SetActive(false);
        isPressedEnter = false;
    }
}
