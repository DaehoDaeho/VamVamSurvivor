using NUnit.Framework;

// 가장 단순한 계산으로 테스트 문법부터 확인하는 클래스입니다.
public class AdditionTests
{
    // [Test]가 붙은 public 메서드를 Test Runner가 테스트로 인식합니다.
    [Test]
    public void TwoPlusThree_EqualsFive()
    {
        // expected는 우리가 기대하는 정답인 5입니다.
        int expected = 5;

        // actual은 실제 계산 결과입니다.
        int actual = 2 + 3;

        // 두 값이 같으면 통과, 다르면 실패합니다.
        Assert.AreEqual(expected, actual);
    }
}
