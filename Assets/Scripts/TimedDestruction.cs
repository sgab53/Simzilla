using UnityEngine;

public class TimedDestruction : MonoBehaviour
{
    [SerializeField] private float timeOut = 8.0f;
    [SerializeField] private GameObject particle;
    private Rigidbody rb;
    private float count = 0.0f;

    private void Start()
    {
        var t = Instantiate(particle).transform;
        t.position = transform.position;
        t.rotation = Quaternion.Euler(new Vector3(0, Random.Range(0, 360.0f), 0));
        t.localScale *= Random.Range(.8f, 1.1f);
    }

    public void AddRigidbody(Rigidbody rigidbody)
    {
        rb = rigidbody;
    }

    private void FixedUpdate()
    {
        count += Time.fixedDeltaTime;
        if (count >= timeOut)
        {
            count = 0.0f;
            gameObject.SetActive(false);
        }
    }
}
