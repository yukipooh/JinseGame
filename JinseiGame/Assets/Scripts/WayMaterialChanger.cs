using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayMaterialChanger : MonoBehaviour
{
    GameObject[] tiles;
    [SerializeField] Material[] materials;

    // Start is called before the first frame update
    void Start()
    {
        tiles = new GameObject[transform.childCount];
        for(int i = 0; i < transform.childCount; i++){
            tiles[i] = transform.GetChild(i).gameObject;
        }
        ChangeMaterial();
    }

    void ChangeMaterial(){
        foreach(GameObject tile in tiles){
            int random = Random.Range(0,materials.Length);
            tile.GetComponent<MeshRenderer>().material = materials[random];
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
