using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RouletteMaker : MonoBehaviour {
    [SerializeField] private Transform imageParentTransform;
    public List<string> choices;
    [SerializeField] private List<Color> rouletteColors;
    [SerializeField] private Image rouletteImage;
    [SerializeField] private RouletteController rController;
    public void Initialize () {
        float ratePerRoulette = 1 / (float) choices.Count;
        float rotatePerRoulette = 360 / (float) (choices.Count);
        for (int i = 0; i < choices.Count; i++) {
            var obj = Instantiate (rouletteImage, imageParentTransform);
            obj.color = rouletteColors[(choices.Count - 1 - i)];
            obj.fillAmount = ratePerRoulette * (choices.Count - i);
            obj.GetComponentInChildren<Text> ().text = choices[(choices.Count - 1 - i)];
            obj.transform.GetChild (0).transform.rotation = Quaternion.Euler (0, 0, ((rotatePerRoulette / 2) + rotatePerRoulette * i));
        }
        rController.SetRoulette();
        rController.rMaker = this;
        rController.rotatePerRoulette = rotatePerRoulette;
        rController.roulette = imageParentTransform.gameObject;
    }
}