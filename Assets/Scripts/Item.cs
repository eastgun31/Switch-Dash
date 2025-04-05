using UnityEngine;

public class Item : MonoBehaviour
{
    private Vector3 targetPos;
    private float speed = 3f;

    private void Start()
    {
        targetPos = new Vector3(-12f, transform.position.y, 0);
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, 
            targetPos, GameManager.instance.worldSpeed * speed * Time.deltaTime);

        if (transform.position.x <= targetPos.x)
            Destroy(gameObject);
    }
}
