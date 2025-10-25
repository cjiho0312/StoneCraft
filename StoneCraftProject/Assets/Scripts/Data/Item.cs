using UnityEngine;

public enum ItemType
{
    Pickaxe, // ���
    Tool, // ����
    Bow, // Ȱ
    Sculpture, // ����ǰ
    Loot // ��� ����ǰ
}

[CreateAssetMenu(menuName = "Data/Item")]
public class Item : ScriptableObject
{
    public int itemId; // ���� ������ ID
    public string itemName; // �̸�
    public ItemType itemtype; // Ÿ��
    public Sprite itemImage; // ��������Ʈ
    public int grade;          // ���� ���, ����ǰ ǰ�� ��
    public int quantity = 1;   // �⺻�� 1 (������ 1�� ����)
}
