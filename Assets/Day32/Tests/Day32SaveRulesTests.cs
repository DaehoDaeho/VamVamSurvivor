using NUnit.Framework;

// 테스트 코드는 Runtime 코드와 분리된 EditMode 테스트 어셈블리에 둡니다.
// [Test]는 Unity Test Runner가 실행할 메서드를 표시합니다.
public class Day32SaveRulesTests
{
    [Test]
    public void ValidValues_AreAccepted()
    {
        // 정상 값이 들어오면 true가 나와야 합니다.
        bool actual = Day32SaveRules.CanRestore(10, 30f);
        Assert.IsTrue(actual);
    }

    [Test]
    public void NegativeCoins_AreRejected()
    {
        // 코인은 음수가 될 수 없으므로 false가 나와야 합니다.
        bool actual = Day32SaveRules.CanRestore(-1, 30f);
        Assert.IsFalse(actual);
    }

    [Test]
    public void ZeroCoinsAndZeroSeconds_AreAccepted()
    {
        // 게임을 시작한 직후의 0개, 0초는 유효한 경계값입니다.
        bool actual = Day32SaveRules.CanRestore(0, 0f);
        Assert.IsTrue(actual);
    }

    [TestCase(-0.1f)]
    [TestCase(float.NaN)]
    [TestCase(float.PositiveInfinity)]
    public void InvalidElapsedTime_IsRejected(float seconds)
    {
        // 동일한 규칙을 여러 잘못된 입력값으로 반복 검사합니다.
        bool actual = Day32SaveRules.CanRestore(10, seconds);
        Assert.IsFalse(actual);
    }
}
