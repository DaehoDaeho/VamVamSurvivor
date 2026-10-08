using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerExperience : MonoBehaviour
{
    [SerializeField] private int startLevel = 1;
    [SerializeField] private int startExperienceToNexeLevel = 5;
    [SerializeField] private int experienceIncreasePerLevel = 5;
    [SerializeField] private Image expGauge;
    [SerializeField] private TMP_Text expText;
    [SerializeField] private TMP_Text levelText;

    private int currentLevel;
    private int currentExperience;
    private int experienceToNextLevel;

    private void Awake()
    {
        currentLevel = startLevel;
        currentExperience = 0;
        experienceToNextLevel = startExperienceToNexeLevel;

        UpdateExpUI();
        UpdateLevelUI();
    }

    public void AddExperience(int amount)
    {
        currentExperience += amount;

        Debug.Log("현재 경험치: " + currentExperience);

        // 레벨업 처리.
        CheckLevelup();
        UpdateExpUI();
        UpdateLevelUI();
    }

    void CheckLevelup()
    {
        while(currentExperience >= experienceToNextLevel)
        {
            currentExperience -= experienceToNextLevel;
            currentLevel++;
            experienceToNextLevel += experienceIncreasePerLevel;

            Debug.Log("현재 레벨: " + currentLevel);
        }
    }

    void UpdateExpUI()
    {
        if(expGauge != null)
        {
            expGauge.fillAmount = (float)currentExperience / (float)experienceToNextLevel;
        }

        if(expText != null)
        {
            expText.text = $"{currentExperience} / {experienceToNextLevel}";
        }
    }

    void UpdateLevelUI()
    {
        if(levelText != null)
        {
            levelText.text = $"Lv.{currentLevel}";
        }
    }    
}
