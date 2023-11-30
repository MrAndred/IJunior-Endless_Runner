using UnityEngine;

public class AidKit : MonoBehaviour
{
    [SerializeField] private float _healAmount = 10;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
        {
            player.Heal(_healAmount);
            gameObject.SetActive(false);
        }
    }
}
