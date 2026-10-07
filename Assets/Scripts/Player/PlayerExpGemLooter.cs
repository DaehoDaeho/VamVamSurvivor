using UnityEngine;

public class PlayerExpGemLooter : MonoBehaviour
{
    [SerializeField] private LayerMask expGemLayer;
    [SerializeField] private float detectRange = 3.0f;

    // Update is called once per frame
    void Update()
    {
        Collider2D[] gems = Physics2D.OverlapCircleAll(transform.position, detectRange, expGemLayer);

        for(int i=0; i<gems.Length; i++)
        {
            ExpGem expGem = gems[i].GetComponent<ExpGem>();
            if (expGem != null)
            {
                expGem.StartMoveToPlayer(transform);
            }
        }
    }
}
