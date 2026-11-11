using System;
using System.IO;
using UnityEngine;

// JSON 문자열 변환과 실제 파일 입출력을 수행하는 실습용 컴포넌트입니다.
public class Day31JsonSaveLab : MonoBehaviour
{
    // 위치를 저장하고 복원할 오브젝트를 Inspector에서 연결합니다.
    [SerializeField] private Transform playerTarget;

    // 이번 실행 중의 코인 수와 경과 시간입니다.
    [SerializeField] private int coins;
    [SerializeField] private float elapsedSeconds;

    private string GetSavePath()
    {
        // Unity가 제공하는 저장용 폴더와 파일 이름을 안전하게 합칩니다.
        return Path.Combine(Application.persistentDataPath, "day31_save.json");
    }

    private void Start()
    {
        // 실제 저장 위치는 컴퓨터마다 다르므로 직접 출력합니다.
        Debug.Log("JSON 저장 파일 위치: " + GetSavePath());
    }

    private void Update()
    {
        // 게임이 실행되는 동안 매 프레임 지난 시간을 누적합니다.
        elapsedSeconds += Time.deltaTime;
    }

    [ContextMenu("01 Add 10 Coins")]
    private void AddTenCoins()
    {
        // 저장하기 전에 게임의 현재 데이터를 일부러 변경합니다.
        coins += 10;
        Debug.Log("현재 코인: " + coins);
    }

    [ContextMenu("02 Move Player Right")]
    private void MovePlayerRight()
    {
        if (playerTarget == null)
        {
            Debug.LogWarning("Player Target에 오브젝트를 연결해 주세요.");
            return;
        }

        // 오른쪽으로 2유닛 움직여 저장 전후 위치를 비교합니다.
        playerTarget.position += new Vector3(2f, 0f, 0f);
        Debug.Log("현재 플레이어 X: " + playerTarget.position.x);
    }

    [ContextMenu("03 Save JSON")]
    private void SaveGame()
    {
        if (playerTarget == null)
        {
            Debug.LogWarning("연결된 플레이어가 없어 저장하지 않았습니다.");
            return;
        }

        // 게임의 현재 상태를 저장 전용 데이터로 복사합니다.
        Day31SaveData data = new Day31SaveData();
        data.version = 1;
        data.coins = coins;
        data.elapsedSeconds = elapsedSeconds;
        data.playerX = playerTarget.position.x;
        data.playerY = playerTarget.position.y;

        // 데이터를 사람이 읽을 수 있는 JSON 문자열로 직렬화합니다.
        string json = JsonUtility.ToJson(data, true);

        try
        {
            // 파일을 만들거나 기존 파일 내용을 새 문자열로 덮어씁니다.
            File.WriteAllText(GetSavePath(), json);
            Debug.Log("JSON 저장 완료: " + GetSavePath());
        }
        catch (Exception ex)
        {
            // 권한/경로/장치 문제로 쓰지 못한 경우 오류를 남깁니다.
            Debug.LogError("JSON 저장 실패: " + ex.Message);
        }
    }

    [ContextMenu("04 Reset Runtime State")]
    private void ResetRuntimeState()
    {
        if (playerTarget == null)
        {
            Debug.LogWarning("Player Target에 오브젝트를 연결해 주세요.");
            return;
        }

        // 파일에는 손대지 않고, 현재 상태만 초기화합니다.
        coins = 0;
        elapsedSeconds = 0f;
        playerTarget.position = Vector3.zero;
        Debug.Log("현재 상태만 초기화했습니다.");
    }

    [ContextMenu("05 Load JSON")]
    private void LoadGame()
    {
        if (playerTarget == null)
        {
            Debug.LogWarning("연결된 플레이어가 없어 불러오지 않았습니다.");
            return;
        }

        string path = GetSavePath();
        if (!File.Exists(path))
        {
            // 첫 실행처럼 파일이 없는 경우는 흔한 정상 상황입니다.
            Debug.LogWarning("저장 파일이 없습니다: " + path);
            return;
        }

        try
        {
            // 파일의 문자열을 읽고, 저장 전용 객체로 역직렬화합니다.
            string json = File.ReadAllText(path);
            Day31SaveData data = JsonUtility.FromJson<Day31SaveData>(json);

            // 파일이 있으나 내용이 비정상적이면 현재 값을 유지합니다.
            if (data == null || data.version != 1 || data.coins < 0 ||
                data.elapsedSeconds < 0f || float.IsNaN(data.elapsedSeconds) ||
                float.IsNaN(data.playerX) || float.IsNaN(data.playerY) ||
                float.IsInfinity(data.elapsedSeconds) ||
                float.IsInfinity(data.playerX) || float.IsInfinity(data.playerY))
            {
                Debug.LogWarning("저장 데이터가 유효하지 않아 복원하지 않았습니다.");
                return;
            }

            // 확인을 통과한 뒤에만 실행 중 상태를 실제로 변경합니다.
            coins = data.coins;
            elapsedSeconds = data.elapsedSeconds;
            playerTarget.position = new Vector3(
                data.playerX, data.playerY, playerTarget.position.z);

            Debug.Log("JSON 복원 완료: 코인 " + coins +
                      ", 플레이어 X " + playerTarget.position.x);
        }
        catch (Exception ex)
        {
            // JSON 문법 오류나 파일 읽기 실패를 Console에 남깁니다.
            Debug.LogError("JSON 불러오기 실패: " + ex.Message);
        }
    }
}
