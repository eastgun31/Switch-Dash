using UnityEngine;

public class BackGround : MonoBehaviour
{
    // Quad의 Material을 저장할 변수
    [SerializeField] private Material material;
    [SerializeField] private float bgspeed = 0.15f;
    GameManager gm;

    void Start()
    {
        gm = GameManager.instance;
        // Renderer에서 Material 가져오기
        material = GetComponent<Renderer>().material;
    }

    void Update()
    {
        // X축 방향으로 스크롤 오프셋 계산
        Vector2 offset = new Vector2(bgspeed * gm.worldSpeed * Time.deltaTime, 0);

        // Material의 메인 텍스처 오프셋 값을 변경하여 스크롤 효과 적용
        material.mainTextureOffset += offset;
    }
}
