using Leopotam.EcsLite;
using UnityEngine;

public class BonusTake : MonoBehaviour
{
    private EcsWorld _world;
    public void Initialize(EcsWorld world)
    {
        _world = world;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bonus") 
        {
            ref var bonusComponent = ref _world.GetPool<Bonus>().Add(_world.NewEntity());
            bonusComponent.BonusValue = 1;

            Destroy(other.gameObject);
        }
    }
}
