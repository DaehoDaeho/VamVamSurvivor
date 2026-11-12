// 31일차의 저장 데이터를 불러오기 전에 숫자 값의 유효성을 검사합니다.
// UnityEngine/MonoBehaviour 없이 실행되는 순수 C# 계산 규칙입니다.
public static class Day32SaveRules
{
    // 코인이 0개인 저장은 정상입니다. 음수만 허용하지 않습니다.
    public static bool IsCoinCountValid(int coins)
    {
        return coins >= 0;
    }

    // 경과 시간은 0 이상인 유한한 숫자여야 합니다.
    public static bool IsElapsedTimeValid(float elapsedSeconds)
    {
        // NaN은 정상적인 숫자가 아니며, 무한대도 플레이 시간으로 쓸 수 없습니다.
        if (float.IsNaN(elapsedSeconds) || float.IsInfinity(elapsedSeconds))
        {
            return false;
        }

        // 음수 시간은 불가능한 상태이므로 불러오지 않습니다.
        return elapsedSeconds >= 0f;
    }

    // 개별 검사를 조합해 저장 데이터의 복원 가능 여부를 판단합니다.
    // 호출 순서는 같고 반환 결과도 리팩터링 이전과 같습니다.
    public static bool CanRestore(int coins, float elapsedSeconds)
    {
        return IsCoinCountValid(coins) && IsElapsedTimeValid(elapsedSeconds);
    }
}
