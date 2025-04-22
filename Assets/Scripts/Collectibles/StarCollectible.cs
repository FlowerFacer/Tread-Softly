using UnityEngine;

public class StarCollectible : MonoBehaviour
{
    public AudioClip collectSound;
    public GameObject collectEffect;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.root.CompareTag("Player"))
        {
            Debug.Log("Entered Trigger with:" + other.name);

            if (collectSound)
            {
                AudioSource.PlayClipAtPoint(collectSound, transform.position);
            }

            if (collectEffect)
            {
                Instantiate(collectEffect, transform.position, Quaternion.identity);
            }

            if (StarManager.Instance != null)
            {
                StarManager.Instance.AddStar();
            }

            // Add Score - added here later for game manager

            Debug.Log("Stars Collected: " + StarManager.Instance.GetStarCount().ToString());

            // Destorys the star
            Destroy(gameObject);
        }
    }
}
