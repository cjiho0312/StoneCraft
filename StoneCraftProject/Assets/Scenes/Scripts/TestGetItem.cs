using UnityEngine;
using UnityEngine.UI;

public class TestGetItem : MonoBehaviour
{
    Button button;
    [SerializeField] Item item;

    public void GetPick()
    {
        Inventory.Instance.AddItem(item);
    }
}
