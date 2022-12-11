using UnityEngine;
using UnityEngine.UI;

public class MouseCursor : MonoBehaviour
{
    [SerializeField] private Image mouseCursor;
    // Start is called before the first frame update
    void Start()
    {
        mouseCursor.transform.position = Input.mousePosition;
        // hide windows cursor
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        mouseCursor.transform.position = Input.mousePosition;
        Cursor.visible = false;
    }
}
