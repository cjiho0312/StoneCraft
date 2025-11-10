using System.Collections;
using UnityEngine;

public class Customer : MonoBehaviour
{
    [SerializeField] Transform BirthArea;
    [SerializeField] Transform Path1;
    [SerializeField] Transform Path2;
    [SerializeField] float speed = 2f;
    Rigidbody rb;
    Animator animator;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        gameObject.transform.position = BirthArea.transform.position;
        StartCoroutine(GoPath1());
    }

    IEnumerator GoPath1()
    {
        animator.SetBool("Walking", true);

        // Path1에 도달할 때까지 반복
        while (Vector3.Distance(transform.position, Path1.position) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, Path1.position, speed * Time.deltaTime);

            yield return null; // 다음 프레임까지 대기
        }

        if (WorkshopManager.Instance.IsExistSculpture())
        {
            // 구매
            StartCoroutine(GoPath2());
        }
        else
        {
            // 돌아가기
            StartCoroutine(GoBack());
        }
    }

    IEnumerator GoPath2()
    {
        // Path2에 도달할 때까지 반복
        while (Vector3.Distance(transform.position, Path2.position) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, Path2.position, speed * Time.deltaTime);

            yield return null; // 다음 프레임까지 대기
        }

        animator.SetBool("Walking", false);
        StartCoroutine(Choice());

    }

    IEnumerator Choice()
    {
        yield return new WaitForSeconds(3f);

        WorkshopManager.Instance.SellSculputure();

        yield return null;

        StartCoroutine(GoBack());
    }

    IEnumerator GoBack()
    {
        gameObject.transform.Rotate(0, 180, 0);
        animator.SetBool("Walking", true);

        while (Vector3.Distance(transform.position, Path1.position) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, Path1.position, speed * Time.deltaTime);

            yield return null; // 다음 프레임까지 대기
        }

        while (Vector3.Distance(transform.position, BirthArea.position) > 1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, BirthArea.position, speed * Time.deltaTime);

            yield return null; // 다음 프레임까지 대기
        }

        Destroy(gameObject);
    }
}
