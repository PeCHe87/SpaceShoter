using UnityEngine;

public class PushBackComponent : MonoBehaviour
{
    public System.Action OnCollision;

    [SerializeField] private float _forcePushBack = 1;
    [SerializeField] private string[] _avoidingTags = null;

    private Rigidbody _rigidbody = null;
    private int _tagsAmount = 0;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _tagsAmount = _avoidingTags.Length;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (IsAvoidedTag(collision.gameObject.tag))
            return;

        int contactCount = collision.contactCount;

        if (contactCount > 0)
        {
            // Stops the current movement
            _rigidbody.velocity = Vector3.zero;

            ContactPoint point = collision.contacts[0];

            // Calculate direction between point of collision and object
            Vector3 dir = point.point - transform.position;
            
            // Gets the opposite direction, inverts it
            dir = -dir.normalized;

            // Add force based on direction of impact inverted and push back force
            _rigidbody.AddForce(dir * _forcePushBack, ForceMode.Impulse);

            if (OnCollision != null)
                OnCollision();
        }
    }

    private bool IsAvoidedTag(string tagToCheck)
    {
        for (int i = 0; i < _tagsAmount; i++)
        {
            if (_avoidingTags[i].Equals(tagToCheck))
                return true;
        }

        return false;
    }
}
