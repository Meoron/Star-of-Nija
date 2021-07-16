using System.Runtime.InteropServices;
using UnityEngine;
using Random = UnityEngine.Random;

public class LineProjectileCollisionBehaviour : MonoBehaviour
{
    public GameObject EffectOnHit;
    public GameObject EffectOnHitObject;
    public GameObject ParticlesScale;
    public GameObject GoLight;
    public bool IsCenterLightPosition;
    public LineRenderer[] LineRenderers;

    private EffectSettings effectSettings;
    private Transform t, tLight, tEffectOnHit, tParticleScale;
    private RaycastHit2D hit;
    private RaycastHit2D oldRaycastHit;
    private Vector3 endPoint;
    private bool isInitializedOnStart;
    private bool frameDroped;
    private ParticleSystem[] effectOnHitParticles;
    private EffectSettings effectSettingsInstance;

    void GetEffectSettingsComponent(Transform tr)
    {
        var parent = tr.parent;
        if (parent != null)
        {
            effectSettings = parent.GetComponentInChildren<EffectSettings>();
            if (effectSettings == null)
                GetEffectSettingsComponent(parent.transform);
        }
    }

    // Use this for initialization
    private void Start()
    {
        t = transform;
        if (EffectOnHit != null)
        {
            tEffectOnHit = EffectOnHit.transform;
            effectOnHitParticles = EffectOnHit.GetComponentsInChildren<ParticleSystem>();
        }
        if (ParticlesScale != null) tParticleScale = ParticlesScale.transform;
        GetEffectSettingsComponent(t);
        if (effectSettings == null)
            Debug.Log("Prefab root or children have not script \"PrefabSettings\"");
        if (GoLight != null) tLight = GoLight.transform;
        InitializeDefault();
        isInitializedOnStart = true;
    }

    void OnEnable()
    {
        if (isInitializedOnStart) InitializeDefault();
    }

    void OnDisable()
    {
        CollisionLeave();
    }

    private void InitializeDefault()
    {
        hit = new RaycastHit2D();
        frameDroped = false;
    }

    private void Update()
    {
        if (!frameDroped)
        {
            frameDroped = true;
            return;
        }

        endPoint = t.position + t.forward * effectSettings.MoveDistance;
        RaycastHit2D raycastHit;

        raycastHit = Physics2D.Raycast(t.position, t.forward, effectSettings.MoveDistance + 1, effectSettings.LayerMask);
        if (raycastHit)
        {
            hit = raycastHit;
            endPoint = raycastHit.point;
            if (oldRaycastHit.collider != hit.collider)
            {
                CollisionLeave();
                oldRaycastHit = hit;
                CollisionEnter();

                if (EffectOnHit != null)
                {
                    foreach (var effectOnHitParticle in effectOnHitParticles)
                    {
                        effectOnHitParticle.Play();
                    }
                }
            }
            Vector2 direction = t.forward;
            if (EffectOnHit != null) tEffectOnHit.position = hit.point - direction * effectSettings.ColliderRadius;
        }
        else
        {
            endPoint = t.position + t.forward * effectSettings.MoveDistance;
            if (EffectOnHit != null) foreach (var effectOnHitParticle in effectOnHitParticles)
                {
                    effectOnHitParticle.Stop();
                }
        }

        if (EffectOnHit != null) tEffectOnHit.LookAt(hit.point + hit.normal);

        if (IsCenterLightPosition && GoLight != null)
            tLight.position = (t.position + endPoint) / 2;

        foreach (var additionalLineRenderer in LineRenderers)
        {
            additionalLineRenderer.SetPosition(0, endPoint);
            additionalLineRenderer.SetPosition(1, t.position);
        }

        if (ParticlesScale != null)
        {
            var distance = Vector3.Distance(t.position, endPoint) / 2;
            tParticleScale.localScale = new Vector3(distance, 1, 1);
        }
    }

    private void CollisionEnter()
    {
        if (EffectOnHitObject != null && hit.transform != null)
        {
            //var addMat = hit.transform.GetComponentInChildren<AddMaterialOnHit>();
            effectSettingsInstance = null;
            //if (addMat!=null)
            //effectSettingsInstance = addMat.gameObject.GetComponent<EffectSettings>();
            if (effectSettingsInstance != null)
                effectSettingsInstance.IsVisible = true;
            else
            {
                var hitGO = hit.transform;
                var renderer = hitGO.GetComponentInChildren<Renderer>();
                var effect = Instantiate(EffectOnHitObject) as GameObject;
                effect.transform.parent = renderer.transform;
                effect.transform.localPosition = Vector3.zero;
                effectSettingsInstance = effect.GetComponent<EffectSettings>();
            }
        }
        effectSettings.OnCollisionHandler(new CollisionInfo { Hit = hit });
    }

    void CollisionLeave()
    {
        if (effectSettingsInstance != null)
        {
            effectSettingsInstance.IsVisible = false;
        }
    }
}

