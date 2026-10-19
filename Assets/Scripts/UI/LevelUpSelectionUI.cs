using UnityEngine;
using System.Collections.Generic;

public class LevelUpSelectionUI : MonoBehaviour
{
    [SerializeField] private PlayerExperience playerExperience;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private PlayerAutoAttack playerAutoAttack;

    [SerializeField] private GameObject selectionPanel;

    [SerializeField] private float moveSpeedIncrease = 0.5f;
    [SerializeField] private int healAmount = 2;
    [SerializeField] private int damageIncrease = 1;
    [SerializeField] private float attackIntervalDecrease = 0.1f;
    [SerializeField] private int projectileCountIncrease = 1;

    private int pendingSelections;
    private List<LevelUpData> datas = new List<LevelUpData>();

    private void Awake()
    {
        selectionPanel.SetActive(false);
        pendingSelections = 0;
    }

    /// <summary>
    /// 오브젝트가 활성화 될 때 유니티가 자동으로 호출하는 함수.
    /// </summary>
    private void OnEnable()
    {
        playerExperience.onLevelUp += ShowSelection;
    }

    private void OnDisable()
    {
        playerExperience.onLevelUp -= ShowSelection;
    }

    void ShowSelection()
    {
        pendingSelections++;
        selectionPanel.SetActive(true);
        Time.timeScale = 0.0f;
    }

    public void OnClickUpgrade(int index)
    {
        if(index < 0 || index >= datas.Count)
        {
            return;
        }

        LevelUpData data = datas[index];

        switch(data.levelUpType)
        {
            case LevelUpType.MoveSpeed:
                {

                }
                break;

            case LevelUpType.Heal:
                {

                }
                break;

            case LevelUpType.ProjectileDamage:
                {

                }
                break;

            case LevelUpType.AttackSpeed:
                {

                }
                break;

            case LevelUpType.ProjectileCount:
                {

                }
                break;
        }
    }

    public void ChooseMoveSpeed()
    {
        playerMovement.IncreaseMoveSpeed(moveSpeedIncrease);

        // 선택 UI 종료 처리.
        FinishSelection();
    }

    public void ChooseHeal()
    {
        playerHealth.Heal(healAmount);

        // 선택 UI 종료 처리.
        FinishSelection();
    }

    public void ChooseDamage()
    {
        playerAutoAttack.IncreaseDamage(damageIncrease);

        FinishSelection();
    }

    public void ChooseAttackSpeed()
    {
        playerAutoAttack.IncreaseAttackSpeed(attackIntervalDecrease);

        FinishSelection();
    }

    public void ChooseProjectileCount()
    {
        playerAutoAttack.IncreaseProjectileCount(projectileCountIncrease);

        FinishSelection();
    }

    void FinishSelection()
    {
        pendingSelections--;

        if(pendingSelections > 0)
        {
            return;
        }

        selectionPanel.SetActive(false);
        Time.timeScale = 1.0f;
    }
}
