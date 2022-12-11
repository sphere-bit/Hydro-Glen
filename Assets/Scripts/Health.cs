using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField]
    private Image healthBar;
    [SerializeField]
    private float currHealth = 100;
    [SerializeField]
    private float maxHealth = 100;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (currHealth <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            playerDamage(10);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            playerHeal(10);
        }
    }

    public void playerDamage(float damage)
    {
        currHealth -= damage;
        healthBar.fillAmount = currHealth / maxHealth;
    }

    public void playerHeal(float restore)
    {
        currHealth += restore;
        currHealth = Mathf.Clamp(currHealth, 0, maxHealth);
        healthBar.fillAmount = currHealth / maxHealth;
    }
}
