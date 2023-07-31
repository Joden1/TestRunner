using Leopotam.EcsLite;
using UnityEngine;

public class FinishTrigger : MonoBehaviour
{
    [SerializeField] private GameObject _panelFinish;
    private EcsWorld _world;

    public void Initialize(EcsWorld world)
    {
        _world = world;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Finish")
        {
            ref var finishComponents = ref _world.GetPool<Finish>().Add(_world.NewEntity());
            finishComponents.IsFinish = true;
            _panelFinish.SetActive(true);

            Destroy(other.gameObject);
        }
    }
}
