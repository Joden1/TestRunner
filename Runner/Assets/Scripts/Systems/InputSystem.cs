using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

public class InputSystem : IEcsInitSystem, IEcsRunSystem
{
    private readonly EcsFilterInject<Inc<Move, Strafe, PlayerInput, PlayerModel>> _filter = default;

    private readonly EcsPoolInject<PlayerInput> _inputPool = default;

    private SwipeDetector _swipeDetector;
    public void Init(IEcsSystems systems)
    {
        _swipeDetector = GameObject.Find("SwipeDetector").GetComponent<SwipeDetector>();
    }

    public void Run(IEcsSystems systems)
    {
        foreach(var entity in _filter.Value)
        {
            ref var inputComponent = ref _inputPool.Value.Get(entity);

            inputComponent.DirectionInput = Vector3.zero;

            if (Input.GetMouseButton(0))
            {
                inputComponent.DirectionInput = _swipeDetector.Direction;
            }
        }
    }
}
