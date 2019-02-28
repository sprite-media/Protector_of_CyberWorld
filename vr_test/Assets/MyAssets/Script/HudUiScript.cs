using UnityEngine;
using UnityEngine.UI;

public class HudUiScript : MonoBehaviour
{
    


    public Text numEnemiesLeft;

    private void Start()
    {
        UpdaateLeftEnemyText();
    }

    private void Update()
    {
        UpdaateLeftEnemyText();
    }

    public void UpdaateLeftEnemyText()
    {
        numEnemiesLeft.text = Base.numEnemiesLeft.ToString();
    }
}
