using UnityEngine;

public class LimestoneMine : MineBase
{
    [SerializeField] Stone limestone;
    public override Stone StoneType => limestone;
    public override float durability => 10;

    public override void OnInteract()
    {
        mineManager.StartMining(this);
    }
}
