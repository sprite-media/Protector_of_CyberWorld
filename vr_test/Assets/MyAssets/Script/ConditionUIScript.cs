using UnityEngine;
using UnityEngine.UI;

public class ConditionUIScript : MonoBehaviour
{
    [SerializeField] GameObject winUI;
    [SerializeField] GameObject loseUI;
    bool isWon = false;
    bool isLost = false;

    private void Update()
    {
        if(Base.numEnemiesLeft <= 0)
        {
            Win();
        }
        if(Base.HP <= 0.0f)
        {
            Lose();
        }
    }

    public void Win()
    {
        if (isWon) return;
        isWon = true;

        Debug.Log("Win Panel opened");
        winUI.SetActive(true);
        loseUI.SetActive(false);
    }

    public void Lose()
    {
        if (isLost) return;
        isLost = true;

        Debug.Log("Lose Panel opened");
        winUI.SetActive(false);
        loseUI.SetActive(true);
    }
}
