using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

public class MoveSystem : IEcsRunSystem
{
    private readonly EcsFilterInject<Inc<Move, Strafe, PlayerInput, PlayerModel>> _filter = default;

    private readonly EcsPoolInject<Move> _movePool = default;

    public void Run(IEcsSystems systems)
    {
        foreach(var entity in _filter.Value)
        {
            ref Move moveComponent = ref _movePool.Value.Get(entity);

            moveComponent.Rigidbody.velocity = new Vector3(moveComponent.Rigidbody.velocity.x, moveComponent.Rigidbody.velocity.y,
                moveComponent.DirectionMove.z * moveComponent.MoveSpeed * Time.deltaTime);
        }
    }
}
