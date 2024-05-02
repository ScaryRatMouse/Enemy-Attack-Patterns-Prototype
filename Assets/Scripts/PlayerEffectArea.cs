using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public abstract class PlayerEffectArea : MonoBehaviour
{
    protected abstract void OnPlayerEnter(GameObject player);
    protected abstract void OnPlayerExit(GameObject player);

    private BoxCollider2D _collider;

    void Awake()
    {
        _collider = GetComponent<BoxCollider2D>();
        _collider.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PhysicalCharacterController2D>() != null)
            OnPlayerEnter(collision.gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PhysicalCharacterController2D>() != null)
            OnPlayerExit(collision.gameObject);
    }
}
