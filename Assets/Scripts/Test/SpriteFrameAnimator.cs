using UnityEngine;

// 여러 Sprite를 정해진 간격으로 바꾸어 2D 프레임 애니메이션을 재생합니다.
[RequireComponent(typeof(SpriteRenderer))]
public class SpriteFrameAnimator : MonoBehaviour
{
    // Inspector에서 재생 순서대로 연결할 Sprite 목록입니다.
    [SerializeField] private Sprite[] frames;

    // 다음 이미지로 바뀌기까지 기다릴 시간(초)입니다.
    [SerializeField, Min(0.02f)] private float secondsPerFrame = 0.16f;

    // 같은 GameObject의 SpriteRenderer를 한 번만 찾아 보관합니다.
    private SpriteRenderer spriteRenderer;

    // 현재 보여 주는 Sprite가 배열의 몇 번째인지 기억합니다.
    private int frameIndex;

    // 마지막 이미지 변경 후 누적된 시간을 보관합니다.
    private float frameTimer;

    private void Awake()
    {
        // 매 프레임 GetComponent를 호출하지 않도록 컴포넌트를 저장합니다.
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        // 오브젝트를 다시 활성화할 때 첫 이미지부터 시작합니다.
        frameIndex = 0;
        frameTimer = 0f;
        ApplyFrame();
    }

    private void Update()
    {
        // 연결한 이미지가 없거나 한 장뿐이면 바꿀 프레임이 없습니다.
        if (frames == null || frames.Length <= 1)
            return;

        // 이번 프레임에 경과한 시간만 누적합니다.
        frameTimer += Time.deltaTime;

        // Inspector에서 실수로 0을 지정해도 무한 반복되지 않게 합니다.
        float interval = Mathf.Max(0.02f, secondsPerFrame);

        // 이미지가 바뀌었을 때만 Renderer에 새 Sprite를 전달합니다.
        bool didAdvance = false;

        // 한 번의 프레임 지연 동안 여러 이미지 간격이 지나도 순서를 유지합니다.
        while (frameTimer >= interval)
        {
            // 한 번의 이미지 표시 시간을 소비합니다.
            frameTimer -= interval;

            // 마지막 인덱스 다음에는 0번 인덱스로 되돌아갑니다.
            frameIndex = (frameIndex + 1) % frames.Length;
            didAdvance = true;
        }

        // 실제로 표시할 Sprite가 바뀐 경우에만 Renderer에 전달합니다.
        if (didAdvance)
            ApplyFrame();
    }

    private void ApplyFrame()
    {
        // Editor에서 프레임이 비어 있어도 예외가 발생하지 않게 합니다.
        if (spriteRenderer == null || frames == null || frames.Length == 0)
            return;

        // 배열의 현재 이미지 하나를 SpriteRenderer로 보여 줍니다.
        spriteRenderer.sprite = frames[frameIndex];
    }
}
