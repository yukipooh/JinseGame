using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New TileInfo", menuName = "TileInfo")]
public class TileInfo : ScriptableObject
{
    public EnumDefinitions.TileType tileType;
    public int money_delta;
}
