using System;

// 게임의 한 시점에서 보관할 값들을 한 객체에 모읍니다.
// Unity JsonUtility가 이 일반 C# 클래스를 JSON으로 바꾸도록 지정합니다.
[Serializable]
public class Day31SaveData
{
    // 저장 파일 구조를 구별합니다. 현재 수업의 형식은 1입니다.
    public int version = 1;

    // 플레이어가 모은 코인 수입니다.
    public int coins;

    // 현재 실행에서 누적된 경과 시간(초)입니다.
    public float elapsedSeconds;

    // 2D 플레이어의 위치를 숫자 두 개로 기록합니다.
    public float playerX;
    public float playerY;
}
