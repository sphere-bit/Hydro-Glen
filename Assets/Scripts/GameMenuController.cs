using UnityEngine;

public class GameMenuController : MonoBehaviour
{
    [SerializeField]
    private UIGameMenu _gameMenuUI;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Space))
        {
            if (_gameMenuUI.isActiveAndEnabled == false)
            {
                _gameMenuUI.gameObject.SetActive(true);
            }
            else
            {
                _gameMenuUI.gameObject.SetActive(false);
            }
        }
    }
}
