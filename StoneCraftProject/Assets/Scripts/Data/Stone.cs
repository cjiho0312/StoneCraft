using UnityEngine;

[CreateAssetMenu(menuName = "Data/Stone")]
public class Stone : ScriptableObject
{
    public string stoneID;
    public int baseValue;
    public AudioClip hitSound;
}
