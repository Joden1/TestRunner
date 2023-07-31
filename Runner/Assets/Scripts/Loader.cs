using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;
using UnityEngine.UI;

public class Loader : MonoBehaviour
{
    private EcsWorld _world;
    private EcsSystems _updateSystems;
    private EcsSystems _fixedUpdateSystems;

    private void Start()
    {
        BonusCounter bonusCounter = new BonusCounter(GameObject.Find("BonusCounter").GetComponent<Text>());

        _world = new EcsWorld();
        _updateSystems = new EcsSystems(_world, bonusCounter);
        _fixedUpdateSystems = new EcsSystems(_world);

        _updateSystems
            .Add(new PlayerInitializeSystem())
            .Add(new FinishSystem())
            .Add(new InputSystem())
            .Add(new StrafeSystem(this))
            .Add(new BonusSystem())
            .Inject();

        _fixedUpdateSystems
            .Add(new MoveSystem())
            .Inject();
        

        _updateSystems.Init();
        _fixedUpdateSystems.Init();
    }

    private void Update()
    {
        _updateSystems?.Run();
    }

    private void FixedUpdate()
    {
        _fixedUpdateSystems?.Run();
    }

    private void OnDestroy()
    {
        _updateSystems.Destroy();
        _fixedUpdateSystems.Destroy();
        _world.Destroy();
    }
}
