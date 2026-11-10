using UnityEngine;

// 선택한 Sprite가 사용하는 메시 UV 좌표를 Console에 보여 줍니다.
[RequireComponent(typeof(SpriteRenderer))]
public class SpriteUVViewer : MonoBehaviour
{
    private void Start()
    {
        // 같은 오브젝트의 SpriteRenderer를 가져옵니다.
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null || spriteRenderer.sprite == null)
        {
            Debug.LogWarning("SpriteRenderer 또는 Sprite가 없습니다.");
            return;
        }

        // 정점 수만큼 UV 좌표를 확인합니다. 패킹되면 값이 바뀔 수 있습니다.
        Vector2[] coordinates = spriteRenderer.sprite.uv;
        for (int i = 0; i < coordinates.Length; i++)
        {
            Debug.Log("UV " + i + ": " + coordinates[i]);
        }
    }
}
