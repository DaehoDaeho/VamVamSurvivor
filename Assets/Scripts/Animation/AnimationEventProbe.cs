using UnityEngine;

public class AnimationEventProbe : MonoBehaviour
{
    [SerializeField] private Animator animator;
    
    public void BeginAttack()
    {
        animator.SetTrigger("Swing");
    }

    public void OnHitFrame()
    {
        Debug.Log("애니메이션 이벤트 호출!!!");
    }
    
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F) == true)
        {
            BeginAttack();
        }
    }
}
