using UnityEngine;
using UnityEngine.EventSystems;
using Debug = System.Diagnostics.Debug;

namespace Polyperfect.Common
{
    public abstract class BaseInstantiatingCursor : PolyMono, IPointerEnterHandler, IPointerExitHandler
    {
        public GameObject CursorPrefab;
        protected GameObject instantiated;
        protected Transform forward;

        protected void Start()
        {
            Debug.Assert(Camera.main != null, "Camera.main is not null");
            forward = Camera.main.transform;
        }

        public virtual void OnPointerEnter(PointerEventData eventData)
        {
            InteractiveCursor.CursorUpdate += UpdateCursorPosition;
            instantiated = Instantiate(CursorPrefab);
        }

        public virtual void OnPointerExit(PointerEventData eventData)
        {
            InteractiveCursor.CursorUpdate -= UpdateCursorPosition;
            Destroy(instantiated);
        }

        protected abstract void UpdateCursorPosition(RaycastResult obj);

    }
}