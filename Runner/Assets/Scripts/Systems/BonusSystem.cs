using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

public class BonusSystem : IEcsRunSystem
{
    private readonly EcsFilterInject<Inc<Bonus>> _filter = default;

    private readonly EcsPoolInject<Bonus> _bonusPool = default;

    public void Run(IEcsSystems systems)
    {
        foreach(var entity in _filter.Value)
        {
            ref var bonusComponent = ref _bonusPool.Value.Get(entity);

            BonusCounter bonusCounter = systems.GetShared<BonusCounter>();
            bonusCounter.AddBonus(bonusComponent.BonusValue);

            _bonusPool.Value.Del(entity);
        }
    }
}
