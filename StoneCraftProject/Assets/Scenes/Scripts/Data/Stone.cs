using UnityEngine;

[CreateAssetMenu(menuName = "Data/Stone")]
public class Stone : ScriptableObject
{
    public int stoneID;
    public string stoneName;
    public int baseValue;
    public AudioClip hitSound;
}
