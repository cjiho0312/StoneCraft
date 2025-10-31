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

            cart.TakeStones(); // 카트에서 돌 리스트 빼고
            StoneStorageManager.Instance.GetStonesInStorage(cart.stoneList); // 창고에 다시 추가
        }
    }
}
