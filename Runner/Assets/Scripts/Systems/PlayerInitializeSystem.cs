using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

public class PlayerInitializeSystem : IEcsInitSystem
{
    public void Init(IEcsSystems systems)
    {
        var world = systems.GetWorld();
        var playerEntity = world.NewEntity();

        ref var playerModelComponent = ref world.GetPool<PlayerModel>().Add(playerEntity);

        var player = GameObject.FindGameObjectWithTag("Player");

        playerModelComponent.Model = player;
        playerModelComponent.Animator = player.GetComponent<Animator>();

        ref var moveComponent = ref world.GetPool<Move>().Add(playerEntity);
        moveComponent.MoveSpeed = 330f;
        moveComponent.DirectionMove = Vector3.forward;
        moveComponent.Transform = player.transform;
        moveComponent.Rigidbody = player.GetComponent<Rigidbody>();

        ref var inputComponent = ref world.GetPool<PlayerInput>().Add(playerEntity);

        ref var strafeComponent = ref world.GetPool<Strafe>().Add(playerEntity);
        strafeComponent.LineDistance = 2.5f;
        strafeComponent.LineChangeSpeed = 10f;
        strafeComponent.Line = 1;

        var bonus = player.GetComponent<BonusTake>();
        bonus.Initialize(world);
        var finish = player.GetComponent<FinishTrigger>();
        finish.Initialize(world);
    }

}
