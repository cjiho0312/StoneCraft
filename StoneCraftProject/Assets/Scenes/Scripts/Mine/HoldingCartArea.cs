using UnityEngine;

public class HoldingCartArea : MonoBehaviour
{
    Collider col;
    bool isInCol;

    private void Awake()
    {
        col = GetComponent<Collider>();
        isInCol = false;
    }

    public bool IsInCol()
    { return isInCol;}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInCol = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInCol = false;
        }
    }
}
