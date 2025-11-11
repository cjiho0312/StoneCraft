using UnityEngine;

public class MarbleMine : MineBase
{
    [SerializeField] Stone Marble;
    [SerializeField] CartArea cartArea;
    public override Stone StoneType => Marble;
    public override float durability => 5;

    public override void OnInteract()
    {
        if (PlayerManager.Instance.currentItem != null &&
            PlayerManager.Instance.currentItem.itemtype == ItemType.Pickaxe)
        {
            if (cartArea.GetIsCartArea())
            {
                mineManager.StartMining(this);
            }
            else
            {
                GuideTextManager.Instance.MakeGuide(GuideSub.GUIDE, "No cart nearby!");
                Debug.Log("수레가 필요합니다!");
            }
        }
        else
        {
            GuideTextManager.Instance.MakeGuide(GuideSub.GUIDE, "Pickaxe required!");
            Debug.Log("곡괭이가 필요합니다!");
        }
    }
}
