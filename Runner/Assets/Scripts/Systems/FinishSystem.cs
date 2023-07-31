using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
public class FinishSystem : IEcsRunSystem
{
    private readonly EcsFilterInject<Inc<Finish>> _filterFinish = default;
    private readonly EcsFilterInject<Inc<Move, Strafe, PlayerInput, PlayerModel>> _filterPlayer = default;

    private readonly EcsPoolInject<Finish> _finishPool = default;

    private readonly EcsPoolInject<Move> _movePool = default;
    private readonly EcsPoolInject<Strafe> _strafePool = default;
    private readonly EcsPoolInject<PlayerModel> _playerModelPool = default;

    public void Run(IEcsSystems systems)
    {
        foreach(var entityFinish in _filterFinish.Value)
        {
            ref var finishComponent = ref _finishPool.Value.Get(entityFinish);

            if (finishComponent.IsFinish)
            {
                foreach(var entityPlayer in _filterPlayer.Value)
                {
                    _movePool.Value.Del(entityPlayer);
                    _strafePool.Value.Del(entityPlayer);

                    ref var playerModel = ref _playerModelPool.Value.Get(entityPlayer);
                    playerModel.Animator.CrossFade("Idle", 1f);
                }
            }
        }
    }
}

