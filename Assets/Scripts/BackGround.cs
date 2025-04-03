using UnityEngine;

public class BackGround : MonoBehaviour
{
    // Quad�� Material�� ������ ����
    [SerializeField] private Material material;
    [SerializeField] private float bgspeed = 0.15f;
    GameManager gm;

    void Start()
    {
        gm = GameManager.instance;
        // Renderer���� Material ��������
        material = GetComponent<Renderer>().material;
    }

    void Update()
    {
        // X�� �������� ��ũ�� ������ ���
        Vector2 offset = new Vector2(bgspeed * gm.worldSpeed * Time.deltaTime, 0);

        // Material�� ���� �ؽ�ó ������ ���� �����Ͽ� ��ũ�� ȿ�� ����
        material.mainTextureOffset += offset;
    }
}
