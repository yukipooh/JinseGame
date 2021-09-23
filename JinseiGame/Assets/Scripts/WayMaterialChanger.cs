using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class WayMaterialChanger : MonoBehaviour
{

    List<GameObject> tiles = new List<GameObject>();
    
    [SerializeField] Material[] materials;
    [SerializeField] GameObject map;
    

    // Start is called before the first frame update
    public void Initialize()
    {
        int courseNum = transform.GetChild(0).childCount;   //コースの数
        for(int i = 0; i < courseNum; i++){
            for(int j = 0; j < transform.GetChild(0).GetChild(i).childCount; j++){
                tiles.Add(transform.GetChild(0).GetChild(i).GetChild(j).gameObject);
            }
        }
        ChangeMaterial();

    }

    void ChangeMaterial(){
        foreach(GameObject tile in tiles){
            switch(tile.GetComponent<Tile>().tileInfo.tileType){
                case EnumDefinitions.TileType.START:
                tile.GetComponent<MeshRenderer>().material = materials[(int)EnumDefinitions.TileType.START];
                    break;
                case EnumDefinitions.TileType.MONEY:
                tile.GetComponent<MeshRenderer>().material = materials[(int)EnumDefinitions.TileType.MONEY];
                    break;
                case EnumDefinitions.TileType.EMPLOY:
                tile.GetComponent<MeshRenderer>().material = materials[(int)EnumDefinitions.TileType.EMPLOY];
                    break;
                case EnumDefinitions.TileType.JOB_RANKUP:
                tile.GetComponent<MeshRenderer>().material = materials[(int)EnumDefinitions.TileType.JOB_RANKUP];
                    break;
                case EnumDefinitions.TileType.MARRY:
                tile.GetComponent<MeshRenderer>().material = materials[(int)EnumDefinitions.TileType.MARRY];
                    break;
                case EnumDefinitions.TileType.BIRTH:
                tile.GetComponent<MeshRenderer>().material = materials[(int)EnumDefinitions.TileType.BIRTH];
                    break;
                case EnumDefinitions.TileType.TREASURE:
                tile.GetComponent<MeshRenderer>().material = materials[(int)EnumDefinitions.TileType.TREASURE];
                    break;
                case EnumDefinitions.TileType.HOUSING:
                tile.GetComponent<MeshRenderer>().material = materials[(int)EnumDefinitions.TileType.HOUSING];
                    break;
                case EnumDefinitions.TileType.INSURANCE:
                tile.GetComponent<MeshRenderer>().material = materials[(int)EnumDefinitions.TileType.INSURANCE];
                    break;
                case EnumDefinitions.TileType.SETTLE:
                tile.GetComponent<MeshRenderer>().material = materials[(int)EnumDefinitions.TileType.SETTLE];
                    break;
                case EnumDefinitions.TileType.GOAL:
                tile.GetComponent<MeshRenderer>().material = materials[(int)EnumDefinitions.TileType.GOAL];
                    break;
            }
            if(tile.GetComponent<Tile>().tileInfo.isRed){
                tile.GetComponent<MeshRenderer>().material = materials[(int)EnumDefinitions.TileType.GOAL + 1];
            }
            if(tile.GetComponent<Tile>().tileInfo.isMustStop){
                tile.GetComponent<MeshRenderer>().material = materials[(int)EnumDefinitions.TileType.GOAL + 2];
            }
            if(tile.GetComponent<Tile>().tileInfo.isSalaryTile){
                tile.GetComponent<MeshRenderer>().material = materials[(int)EnumDefinitions.TileType.GOAL + 3];
            }
            // int random = Random.Range(0,materials.Length);
            // tile.GetComponent<MeshRenderer>().material = materials[random];
        }
    }
}
