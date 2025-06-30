using UnityEngine;
using EzySlice;
using UnityEngine.InputSystem;

public class SliceObj : MonoBehaviour
{
    public Transform startSlicePoint;
    public Transform endSlicePoint;
    public VelocityEstimator velocityEstimator;
    public LayerMask sliceableLayer;

    public Material crossSectionMat;
    public float cutForce = 2000f;
    //https://www.youtube.com/watch?v=GQzW6ZJFQ94
    // Update is called once per frame
    void FixedUpdate()
    {
        bool hashit = Physics.Linecast(startSlicePoint.position, endSlicePoint.position,
            out RaycastHit hit, sliceableLayer);
        if (hashit)
        {
            GameObject target = hit.transform.gameObject;
            Slice(target);
        }

    }

    public void Slice(GameObject target)
    {
        Vector3 velocity = velocityEstimator.GetVelocityEstimate();
        Vector3 planomal = Vector3.Cross(endSlicePoint.position - startSlicePoint.position,velocity); 
        planomal.Normalize();

        SlicedHull hull = target.Slice(endSlicePoint.position, planomal);

        if (hull != null)
        {
            GameObject upperHull = hull.CreateUpperHull(target, crossSectionMat);
            SetupSlicedComponent(upperHull);
            GameObject lowerHull = hull.CreateLowerHull(target, crossSectionMat);
            SetupSlicedComponent(lowerHull);

            Destroy(target);
        }
    }

    public void SetupSlicedComponent(GameObject slicedObj)
    {
        Rigidbody rb = slicedObj.AddComponent<Rigidbody>();
        MeshCollider collider = slicedObj.AddComponent<MeshCollider>();
        collider.convex = true;
        rb.AddExplosionForce(cutForce, slicedObj.transform.position, 1);
    }
}
