using UnityEngine;

public class StoneObject : MonoBehaviour
{
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
