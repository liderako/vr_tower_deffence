using UnityEngine;

namespace Source.Scripts.Core.Interfaces
{
    public abstract class BaseComponent : MonoBehaviour
    {
         protected void InitComponentInGameObject<T>(out T t, bool logErrors = true)
        {
            if (TryGetComponent(out T tComponent))
            {
                t = tComponent;
            }
            else
            {
                t = default(T);
                if (logErrors)
                {
                    Debug.LogError("I can't find component " + typeof(T) + " " + gameObject.name);
                }
            }
        }
        
        protected void InitComponentInParentGameObject<T>(out T t, bool logErrors = true)
        {
            if (transform.parent.gameObject.TryGetComponent(out T tComponent))
            {
                t = tComponent;
            }
            else
            {
                t = default(T);
                if (logErrors)
                {
                    Debug.LogError("I can't find component " + typeof(T) + " " + gameObject.name);
                }
            }
        }
        
        protected void InitComponentInParentChildGameObject<T>(out T t, bool logErrors = true)
        {
            var tComponent = transform.parent.gameObject.GetComponentInChildren<T>();
            if (tComponent != null)
            {
                t = tComponent;
            }
            else
            {
                t = default(T);
                if (logErrors)
                {
                    Debug.LogError("I can't find component " + typeof(T) + " " + gameObject.name);
                }
            }
        }
        
        protected void InitComponentInChildGameObject<T>(out T t, bool logErrors = true)
        {
            var tComponent = GetComponentInChildren<T>();
            if (tComponent != null)
            {
                t = tComponent;
            }
            else
            {
                t = default(T);
                if (logErrors)
                {
                    Debug.LogError("I can't find component " + typeof(T) + " " + gameObject.name);
                }
            }
        }
    }
}