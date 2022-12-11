using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Energy : MonoBehaviour
{
    [SerializeField]
    private Image energyBar;
    [SerializeField]
    private float currEnergy = 100;
    [SerializeField]
    private float maxEnergy = 100;

    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {
        if (currEnergy <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            depleteEnergy(10);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            restoreEnergy(10);
        }
    }

    public void depleteEnergy(float damage)
    {
        currEnergy -= damage;
        energyBar.fillAmount = currEnergy / maxEnergy;
    }

    public void restoreEnergy(float restore)
    {
        currEnergy += restore;
        currEnergy = Mathf.Clamp(currEnergy, 0, maxEnergy);
        energyBar.fillAmount = currEnergy / maxEnergy;
    }
}
