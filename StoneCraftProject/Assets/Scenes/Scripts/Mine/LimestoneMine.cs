using UnityEngine;

public class LimestoneMine : MineBase
{
    [SerializeField] Stone limestone;
    public override Stone StoneType => limestone;
    public override float durability => 10;

    public override void OnInteract()
    {
        if (PlayerManager.Instance.currentItem != null && PlayerManager.Instance.currentItem.itemtype == ItemType.Pickaxe)
        {
            mineManager.StartMining(this);
        }
        else
        {
            Debug.Log("°î±ªÀÌ°¡ ÇÊ¿äÇÕ´Ï´Ù!");
        }
    }
}
