using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New TileInfo", menuName = "TileInfo")]
public class TileInfo : ScriptableObject
{
    public EnumDefinitions.TileType tileType;
    [Multiline(4)] public string description;
    public int money_delta;
    public EnumDefinitions.Job job;
    public EnumDefinitions.Treasure treasure;
    public EnumDefinitions.Insurance insurance;
    public bool isRed;  //通り過ぎても効果を発揮するかどうか
    public bool isMustStop; //強制ストップするかどうか

}
