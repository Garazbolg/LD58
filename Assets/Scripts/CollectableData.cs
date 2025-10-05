using UnityEngine;

[CreateAssetMenu(fileName = "CollectableData", menuName = "Data/CollectableData", order = 1)]
public class CollectableData : ScriptableObject
{
    public int type;
    public string name;
    public Sprite icon;
    public int price;
}