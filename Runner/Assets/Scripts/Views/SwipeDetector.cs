using Leopotam.EcsLite;
using UnityEngine.EventSystems;
using UnityEngine;

public class SwipeDetector : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Vector3 Direction { get; private set; }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Direction = eventData.delta.x > 0 ? Vector3.right : Vector3.left;
    }

    public void OnDrag(PointerEventData eventData)
    {

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Direction = Vector3.zero;
    }
}
