using UnityEngine;

// 31일차 첫 번째 실험: 실행 중 숫자와 PlayerPrefs 저장값을 비교합니다.
public class Day31PlayerPrefsLab : MonoBehaviour
{
    // 현재 실행 중 메모리에 들어 있는 코인 수입니다.
    [SerializeField] private int coins;

    // PlayerPrefs에서 이 코인 수를 구별하는 이름표입니다.
    private const string CoinKey = "Day31_Coins_Practice";

    private void Start()
    {
        // Play를 새로 시작할 때 실험용 현재 값을 0으로 맞춥니다.
        coins = 0;
        Debug.Log("실행 시작: 메모리 코인 = " + coins);
    }

    [ContextMenu("01 Add 10 Coins")]
    private void AddTenCoins()
    {
        // 저장소가 아니라 지금 실행 중인 변수만 변경합니다.
        coins += 10;
        Debug.Log("현재 코인 = " + coins);
    }

    [ContextMenu("02 Save Coins")]
    private void SaveCoins()
    {
        // 이름표에 현재 코인 수를 연결합니다.
        PlayerPrefs.SetInt(CoinKey, coins);
        // 변경된 PlayerPrefs 값을 영구 저장소에 기록합니다.
        PlayerPrefs.Save();
        Debug.Log("PlayerPrefs 저장 완료: " + coins);
    }

    [ContextMenu("03 Reset Runtime Coins")]
    private void ResetRuntimeCoins()
    {
        // 저장소는 지우지 않고 실행 중인 값만 0으로 바꿉니다.
        coins = 0;
        Debug.Log("메모리만 초기화: " + coins);
    }

    [ContextMenu("04 Load Coins")]
    private void LoadCoins()
    {
        // 저장된 키가 없다면 기본값 0을 받습니다.
        coins = PlayerPrefs.GetInt(CoinKey, 0);
        //if(PlayerPrefs.HasKey(CoinKey) == true)
        //{
        //    coins = PlayerPrefs.GetInt(CoinKey);
        //}
        //else
        //{
        //    coins = 0;
        //}

        Debug.Log("PlayerPrefs 불러오기 완료: " + coins);
    }

    [ContextMenu("05 Delete Saved Key")]
    private void DeleteSavedKey()
    {
        // 실습에서 사용한 하나의 키만 지웁니다. 전체 삭제는 하지 않습니다.
        PlayerPrefs.DeleteKey(CoinKey);
        PlayerPrefs.Save();
        Debug.Log("실습용 PlayerPrefs 키 삭제 완료");
    }
}
