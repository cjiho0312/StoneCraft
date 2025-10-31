using UnityEngine;

public class StoneObject : MonoBehaviour
{
    [SerializeField] int StoneId;

    public int GetStoneID() {  return StoneId; }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Terrain"))
        {
            Debug.Log("와장창 깨져버린 돌");
            Destroy(gameObject);
            // 부서지는 효과 넣을 거면 여기다가..
        }
    }
}
