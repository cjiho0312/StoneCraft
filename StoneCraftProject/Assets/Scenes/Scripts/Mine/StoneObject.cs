using UnityEngine;

public class StoneObject : MonoBehaviour
{
    [SerializeField] int StoneId;

    public int GetStoneID() {  return StoneId; }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Terrain"))
        {
            Debug.Log("����â �������� ��");
            Destroy(gameObject);
            // �μ����� ȿ�� ���� �Ÿ� ����ٰ�..
        }
    }
}
