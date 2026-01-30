using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float despawnTimer = 5f;

    private void Start()
    {
        Destroy(gameObject,despawnTimer);
    }

    private void Update()
    {
        transform.position += speed * Time.deltaTime * Vector3.back; 
    }
}
