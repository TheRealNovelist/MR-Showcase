using UnityEngine;


public abstract class Singleton<T> : MonoBehaviour
    where T : Component
{
    private static T _instance;
    public static T Instance {
        get {
            if (_instance == null) {
                var objs = FindObjectsOfType (typeof(T)) as T[];
                if (objs.Length > 0)
                    _instance = objs[0];
                if (objs.Length > 1) {
                    Debug.LogError ($"[Singleton: {nameof(T)}]: There is more than one " + typeof(T).Name + " in the scene.");
                }
                if (_instance == null) {
                    GameObject obj = new GameObject
                    {
                        name = typeof(T).Name,
                    };
                    _instance = obj.AddComponent<T>();
                }
            }
            return _instance;
        }
    }
}

public abstract class SingletonPersistent<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance { get; private set; }
	
    public virtual void Awake()
    {
        if (!Application.isPlaying)
        {
            return;
        }
        if (Instance == null) {
            Instance = this as T;
            DontDestroyOnLoad (this);
        } else {
            Debug.LogError($"[SingletonPersistent: {gameObject.name}]: There is already an instance of type in the scene");
            Destroy (gameObject);
        }
    }
}