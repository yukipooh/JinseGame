using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class RouletteController : MonoBehaviourPunCallbacks {
    [HideInInspector] public GameObject roulette;
    [HideInInspector] public float rotatePerRoulette;
    [HideInInspector] public RouletteMaker rMaker;

    // [SerializeField] CarMovement carMovement;
    public static string result;
    private float rouletteSpeed;
    private float slowDownSpeed;
    private int frameCount;
    private bool isPlaying;
    private bool isStop;
    [SerializeField] public Text resultText;
    [SerializeField] private Button startButton;
    [SerializeField] private Button stopButton;
    [SerializeField] GameManager gameManager;
    
    // [SerializeField] private Button retryButton;

    public void SetRoulette () {
        isPlaying = false;
        isStop = false;
        startButton.gameObject.SetActive (true);
        stopButton.gameObject.SetActive (false);
        // retryButton.gameObject.SetActive(false);
        startButton.onClick.AddListener (StartOnClick);
        stopButton.onClick.AddListener (StopOnClick);
        // retryButton.onClick.AddListener (RetryOnClick);
    }

    private void Update () {
        if (!isPlaying) return;
        roulette.transform.Rotate (0, 0, rouletteSpeed);
        frameCount++;
        if (isStop && frameCount > 3) {
            rouletteSpeed *= slowDownSpeed;
            slowDownSpeed -= 0.25f * Time.deltaTime;
            frameCount = 0;
        }
        if (rouletteSpeed < 0.05f) {
            isPlaying = false;
            ShowResult (roulette.transform.eulerAngles.z);
        }
    }

    private void StartOnClick () {
        rouletteSpeed = 14f;
        this.transform.Rotate(0,0,Random.Range(0,359));
        startButton.gameObject.SetActive (false);
        Invoke ("ShowStopButton", 0.1f);
        isPlaying = true;
    }

    private void StopOnClick () {
        slowDownSpeed = Random.Range (0.92f, 0.98f);
        isStop = true;
        stopButton.gameObject.SetActive (false);
    }

    private void RetryOnClick(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void ShowStopButton () {
        stopButton.gameObject.SetActive (true);
    }

    private void ShowResult (float x) {
        for (int i = 1; i <= rMaker.choices.Count; i++) {
            if (((rotatePerRoulette * (i - 1) <= x) && x <= (rotatePerRoulette * i)) ||
                (-(360 - ((i - 1) * rotatePerRoulette)) >= x && x >= -(360 - (i * rotatePerRoulette)))) {
                result = rMaker.choices[i - 1];
            }
        }
        resultText.text = result;
        // retryButton.gameObject.SetActive(true);
        resultText.gameObject.SetActive(true);
        Invoke (nameof(Move), 0.5f);
        
        
    }

    void Move(){
        isStop = false; //ルーレットを再利用できるように
        startButton.gameObject.SetActive(true); //ルーレットを再利用できるように
        this.transform.parent.gameObject.SetActive(false);  //ルーレットを非表示

        foreach(PhotonView photonView in PhotonNetwork.PhotonViewCollection){
            Debug.Log($"{photonView.gameObject.name}({photonView.ViewID})");
        }
        
        foreach(CarMovement carMovement in GameManager.carMovements){
            if(carMovement.photonView.IsMine){
                carMovement.StartCoroutine(carMovement.Dice(true,0)); //resultに応じて移動
            }
        }
    }
}
