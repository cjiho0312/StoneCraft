using UnityEngine;

public class CartStation : MonoBehaviour
{
    Collider AreaCollider;
    Cart cart;

    void Awake()
    {
        AreaCollider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Cart"))
        {
            cart = other.gameObject.GetComponent<Cart>();

            cart.TakeStones(); // īƮ���� �� ����Ʈ ����
            StoneStorageManager.Instance.GetStonesInStorage(cart.stoneList); // â�� �ٽ� �߰�
        }
    }
}
