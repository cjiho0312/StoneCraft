
public enum ItemType
{
    Tool,       // ���, ���� ����, Ȱ ��
    Sculpture,  // ����ǰ
    Loot        // ��� ����ǰ
}

[System.Serializable]
public class Item
{
    public int id;             // ���� ������ ID
    public string itemName;
    public ItemType type;
    public int grade;          // ���� ���, ����ǰ ǰ�� ��
    public int quantity = 1;   // �⺻�� 1 (������ 1�� ����)
}
