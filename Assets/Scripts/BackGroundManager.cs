using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundManager : MonoBehaviour
{
    [SerializeField] private GameObject backgroundPrefab;
    [SerializeField] private float backgroundWidth = 20f;
    [SerializeField] private float posValue; // ����� �ݺ��� ������ �� (�ػ� ���� ����)

    private List<GameObject> backgrounds = new List<GameObject>();
    private Vector2 startPos; // ����� �ʱ� ��ġ
    private float newPos; // ����� ���ο� ��ġ ���� ����
    GameManager gm;

    private void Start()
    {
        gm = GameManager.instance;
        startPos = transform.position; // ���� ������Ʈ�� �ʱ� ��ġ ����

        // �ʱ� ��� ����
        for (int i = 0; i < 3; i++)
        {
            GameObject bg = Instantiate(backgroundPrefab, new Vector3(i * backgroundWidth, 0, 0), Quaternion.identity);
            backgrounds.Add(bg);
        }
    }

    private void Update()
    {
        // ��� ��ũ��
        foreach (GameObject bg in backgrounds)
        {
            // ����� �������� �̵�
            bg.transform.Translate(Vector3.left * gm.worldSpeed * Time.deltaTime);

            // ����� ȭ�� ������ ������ ����Ʈ�� ������ ��� �ڿ� ��ġ
            if (bg.transform.position.x <= -backgroundWidth)
            {
                GameObject lastBg = backgrounds[backgrounds.Count - 1];
                bg.transform.position = new Vector3(lastBg.transform.position.x + backgroundWidth, bg.transform.position.y, bg.transform.position.z);

                // ����Ʈ���� ��� ���� ������Ʈ
                backgrounds.Remove(bg);
                backgrounds.Add(bg);
            }
        }
    }
}
