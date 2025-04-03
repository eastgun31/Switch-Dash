using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundManager : MonoBehaviour
{
    [SerializeField] private GameObject backgroundPrefab;
    [SerializeField] private float backgroundWidth = 20f;
    [SerializeField] private float posValue; // 배경이 반복될 구간의 값 (해상도 가로 길이)

    private List<GameObject> backgrounds = new List<GameObject>();
    private Vector2 startPos; // 배경의 초기 위치
    private float newPos; // 배경의 새로운 위치 계산용 변수
    GameManager gm;

    private void Start()
    {
        gm = GameManager.instance;
        startPos = transform.position; // 현재 오브젝트의 초기 위치 저장

        // 초기 배경 생성
        for (int i = 0; i < 3; i++)
        {
            GameObject bg = Instantiate(backgroundPrefab, new Vector3(i * backgroundWidth, 0, 0), Quaternion.identity);
            backgrounds.Add(bg);
        }
    }

    private void Update()
    {
        // 배경 스크롤
        foreach (GameObject bg in backgrounds)
        {
            // 배경을 왼쪽으로 이동
            bg.transform.Translate(Vector3.left * gm.worldSpeed * Time.deltaTime);

            // 배경이 화면 밖으로 나가면 리스트의 마지막 배경 뒤에 배치
            if (bg.transform.position.x <= -backgroundWidth)
            {
                GameObject lastBg = backgrounds[backgrounds.Count - 1];
                bg.transform.position = new Vector3(lastBg.transform.position.x + backgroundWidth, bg.transform.position.y, bg.transform.position.z);

                // 리스트에서 배경 순서 업데이트
                backgrounds.Remove(bg);
                backgrounds.Add(bg);
            }
        }
    }
}
