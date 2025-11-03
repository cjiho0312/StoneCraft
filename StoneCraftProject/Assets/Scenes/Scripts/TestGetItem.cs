using UnityEngine;
using UnityEngine.UI;

public class TestGetItem : MonoBehaviour
{
    Button button;
    [SerializeField] Item Pick;
    [SerializeField] Item Tool;

    public void GetPick()
    {
        Inventory.Instance.AddItem(Pick);
    }

    public void GetTool()
    {
        Inventory.Instance.AddItem(Tool);
    }
}
