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
            cart.TakeStones(); // 카트에서 돌 리스트 가져오기

            if (!cart.isHaveStones()) return;

            StoneStorageManager.Instance.GetStonesInStorage(cart.stoneList); // 창고에 추가하기
            cart.ClearList();
            StartCoroutine(cart.PlayToStorageEffect());
            GuideTextManager.Instance.MakeGuide(GuideSub.GETITEM, "Stone added to storage");
        }
    }
}
