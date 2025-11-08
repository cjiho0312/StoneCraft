using UnityEngine;

public class CartArea : MonoBehaviour
{
    bool isCartInArea = false;
    public bool GetIsCartArea() {  return isCartInArea; }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Cart"))
        {
            isCartInArea = true;
            Debug.Log("Cart in Area");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Cart"))
        {
            isCartInArea = false;
            Debug.Log("Cart out of Area");
        }
    }
}
