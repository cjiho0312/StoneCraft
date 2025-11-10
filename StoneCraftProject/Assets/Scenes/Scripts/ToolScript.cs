using UnityEngine;

public class ToolScript : MonoBehaviour
{
    public void PlayEffect()
    {
        // if (PlayerManager.Instance.currentItem.itemtype == ItemType.Tool)
        // {
        //     AudioManager.Instance.PlaySculptingToolSound();
        // }
        if (PlayerManager.Instance.currentItem.itemtype == ItemType.Pickaxe)
        {
            MineManager.Instance.PlayMineEffect();
            AudioManager.Instance.PlayPickaxeSound();
        }
    }

}
