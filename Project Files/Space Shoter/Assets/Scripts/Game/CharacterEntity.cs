using UnityEngine;

public class CharacterEntity : MonoBehaviour
{
    private HealthController _health = null;

    public HealthController Health
    {
        get
        {
            return _health;
        }

        protected set
        {
            _health = value;
        }
    }
}
