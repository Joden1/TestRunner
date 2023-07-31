using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrafeSystem : IEcsRunSystem
{
    private readonly EcsFilterInject<Inc<Move, Strafe, PlayerInput, PlayerModel>> _filter = default;

    private readonly EcsPoolInject<PlayerModel> _modelPool = default;
    private readonly EcsPoolInject<Strafe> _strafePool = default;
    private readonly EcsPoolInject<PlayerInput> _inputPool = default;
    private readonly EcsPoolInject<Move> _movePool = default;

    private float _pointStart;
    private float _pointFinish;
    private bool _isStrafing = false;

    private MonoBehaviour _context;

    public StrafeSystem(MonoBehaviour context)
    {
        _context = context;
    }
    public void Run(IEcsSystems systems)
    {
        foreach (var entity in _filter.Value)
        {
            ref PlayerModel modelComponent = ref _modelPool.Value.Get(entity);
            ref Strafe strafeComponent = ref _strafePool.Value.Get(entity);
            ref PlayerInput inputComponent = ref _inputPool.Value.Get(entity);
            ref Move moveComponent = ref _movePool.Value.Get(entity);

            if (moveComponent.Transform.position.x == _pointFinish)
            {
                if (inputComponent.DirectionInput.x < 0 && _pointFinish > -strafeComponent.LineDistance)
                {
                    MoveHorizontal(strafeComponent, modelComponent, moveComponent, -strafeComponent.LineChangeSpeed);
                }
                if (inputComponent.DirectionInput.x > 0 && _pointFinish < strafeComponent.LineDistance)
                {
                    MoveHorizontal(strafeComponent, modelComponent, moveComponent, strafeComponent.LineChangeSpeed);
                }
            }
        }
    }
    
    private void MoveHorizontal(Strafe strafeComponent, PlayerModel modelComponent, Move moveComponent, float direction)
    {
        modelComponent.Animator.applyRootMotion = false;

       _pointStart = _pointFinish;
        _pointFinish += Mathf.Sign(direction) * strafeComponent.LineDistance;

        modelComponent.Animator.SetFloat("Horizontal", direction > 0 ? 1 : -1);

        var _coroutine = MoveCoroutine(moveComponent, strafeComponent, modelComponent, direction);

        if (_isStrafing)
        {
            _context.StopAllCoroutines();
            _isStrafing = false;
        }

        _context.StartCoroutine(_coroutine);
    }

    private IEnumerator MoveCoroutine(Move moveComponent, Strafe strafeComponent, PlayerModel modelComponent, float vectorX)
    {
        _isStrafing = true;

        while (Mathf.Abs(_pointStart - moveComponent.Transform.position.x) < strafeComponent.LineDistance)
        {
            yield return new WaitForFixedUpdate();

            moveComponent.Rigidbody.velocity = new Vector3(vectorX, moveComponent.Rigidbody.velocity.y, moveComponent.Rigidbody.velocity.z);

            float x = Mathf.Clamp(moveComponent.Transform.position.x, Mathf.Min(_pointStart, _pointFinish), Mathf.Max(_pointStart, _pointFinish));
            moveComponent.Transform.position = new Vector3(x, moveComponent.Transform.position.y, moveComponent.Transform.position.z);
        }

        moveComponent.Rigidbody.velocity = new Vector3(0, moveComponent.Transform.position.y, moveComponent.Transform.position.z);
        moveComponent.Transform.position = new Vector3(_pointFinish, moveComponent.Transform.position.y, moveComponent.Transform.position.z);

        modelComponent.Animator.SetFloat("Horizontal", 0);

        _isStrafing = false;
    }
}
