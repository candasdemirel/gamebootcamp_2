using UnityEngine;

public class DropItemController : MonoBehaviour , IReset
{
    [SerializeField] DropItemModel behaviour;

    private int _damage;
    private int _score;
    private Vector3 _velocity;
    private Vector3 _angularVelocity;

    private Rigidbody _rigidbody;

    private void OnEnable()
    {
        EventManager.PauseStateEvent += EventManager_PauseStateEvent;
    }

    private void OnDisable()
    {
        EventManager.PauseStateEvent -= EventManager_PauseStateEvent;
    }


    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();

        if (behaviour is DamageObject damageObject)
        {
            _damage = (behaviour.dropType == PoolObjectType.bomb) ? -damageObject.damage : damageObject.damage;
        }
        else if (behaviour is ScoreObject scoreObject)
        {
            _score = scoreObject.score;
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if (behaviour is DamageObject damageObject)
            {
                EventManager.TriggerDamageEventResult(_damage);
            }
            else if (behaviour is ScoreObject scoreObject)
            {
                EventManager.TriggerScoreEventResult(_score);
            }

        }

        Reset();
        ObjectPooler.Instance.PoolDestroy(behaviour.dropType , this.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "SafeArea")
        {
            Reset();
            ObjectPooler.Instance.PoolDestroy(behaviour.dropType, this.gameObject);
        }
    }

    void EventManager_PauseStateEvent(bool isPaused)
    {
        if (isPaused)
        {
            _velocity = _rigidbody.velocity;
            _angularVelocity = _rigidbody.angularVelocity;
            _rigidbody.useGravity = false;
            Reset();
        }
        else
        {
            _rigidbody.velocity = _velocity;
             _rigidbody.angularVelocity = _angularVelocity;
            _rigidbody.useGravity = true;
        }
    }


    public void Reset()
    {
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
    }
}

[System.Serializable]
class DropItemModel1
{
    public PoolObjectType dropType;
    public int damage;
    public int score;
}

