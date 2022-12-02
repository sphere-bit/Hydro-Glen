using UnityEngine;

public class Cucumber : ItemCollectible
{
    public void Collect()
    {
        Destroy(gameObject);
        Debug.Log("Collected cucumber");
    }
}
