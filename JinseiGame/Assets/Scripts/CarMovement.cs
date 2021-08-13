using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine.AI;
using UnityEngine;
using UnityEngine.UI;

public class CarMovement : MonoBehaviour
{
    [SerializeField] GameObject[] tilePoints;    //マス
    [SerializeField] GameObject roulette;   //RoulettePrefab
    [SerializeField] Text resultText; // resultText
    PlayerData playerData;
    NavMeshAgent navMeshAgent;
    Tween tween;
    Vector3 latestPosition;
    int currentNum = 0; //現在いるタイル
    Rigidbody rigidbody;
    const float INTERVAL = 0.5f;    //次のマスに進むまでに待つ時間

    

    const float ROTATE_FORWARD_ADJUST_ANGLE = 90;

    void Start() {
        latestPosition = transform.position;    
        navMeshAgent = GetComponent<NavMeshAgent>();
        rigidbody = GetComponent<Rigidbody>();
        playerData = GetComponent<PlayerData>();
    }

    public void MoveForward(int tileNum){
        if(tileNum < tilePoints.Length){
            //this.tween = transform.DOMove(tilePoints[tileNum].transform.position,1);
            navMeshAgent.SetDestination(tilePoints[tileNum].transform.position);
            this.currentNum++;
        }
    }
    
    public IEnumerator Dice(){
        int dice = int.Parse(RouletteController.result);   //ルーレット回すよ
        Debug.Log(dice);
        for(int i = 0; i < dice; i++){
            MoveForward(currentNum + 1);
            yield return new WaitForSeconds(INTERVAL);
            resultText.text = (dice-i-1).ToString();
        }
        OnTileStopped();    //移動し終わった
        roulette.SetActive(true);
    }

    void OnTileStopped(){
        //移動し終わった
        tilePoints[currentNum].GetComponent<Tile>().Stopped(ref playerData);
        resultText.gameObject.SetActive(false);
    }


    void Update() {
        
    }
}
