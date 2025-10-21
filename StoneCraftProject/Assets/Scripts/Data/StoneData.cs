using UnityEngine;

[CreateAssetMenu(menuName = "Data/StoneData")]
public class StoneData : ScriptableObject
{
    public string stoneID;
    public int baseValue;
    public AudioClip hitSound;
}
