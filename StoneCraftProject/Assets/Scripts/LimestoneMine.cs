using UnityEngine;

public class LimestoneMine : MineBase
{
    [SerializeField] StoneData limestone;
    public override StoneData StoneType => limestone;
    public override float durability => 10;

    public override void OnInteract()
    {
        mineManager.StartMining(this);
    }
}
