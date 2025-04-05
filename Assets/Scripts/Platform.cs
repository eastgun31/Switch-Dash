using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] Vector3 firstPos;
    [SerializeField] float objSpeed = 3f;
    GameManager gm;

    void Start()
    {
        firstPos = transform.position;
        gm = GameManager.instance;
    }

    private void FixedUpdate()
    {
        if (transform.position.x > firstPos.x)
        {
            transform.position = Vector2.MoveTowards
                (transform.position, firstPos, gm.worldSpeed * objSpeed * Time.fixedDeltaTime);

            if (transform.position.x == firstPos.x)
                gm.holeTrapActive = false;
        }
    }
}
