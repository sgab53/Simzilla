using UnityEngine;

public class TimedDestruction : MonoBehaviour
{
    [SerializeField] private float timeOut = 8.0f;
    private Rigidbody rb;
    private float count = 0.0f;

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
