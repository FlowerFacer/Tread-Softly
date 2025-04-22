using UnityEngine;

public class StarManager : MonoBehaviour
{
    public static StarManager Instance;

    public int starCount = 0;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keeps it across scenes
        }
        else
        {
            Destroy(gameObject); // Prevents duplicate managers
        }
    }

    public void ResetStars()
    {
        starCount = 0;
    }

    public void AddStar()
    {
        starCount++;
    }

    public int GetStarCount()
    {
        return starCount;
    }
}
