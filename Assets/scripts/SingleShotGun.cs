using Mirror;
using UnityEngine;

public class SingleShotGun : Gun
{
    [SerializeField] private Camera cam;
    [SerializeField] private PlayerController player;

    [Client]
    public override void Use() {
        if (isLocalPlayer)
        Shoot();
    }

    [Command]
    void Shoot() {
        if (!isLocalPlayer) return;
                
            Debug.Log("Using gun" + ItemInfo.itemName);
            Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f));
            ray.origin = cam.transform.position;
            if (Physics.Raycast(ray, out RaycastHit hit)) {
                Debug.Log("We hit " + hit.collider.gameObject.name);
                hit.collider.gameObject.GetComponent<IDamageable>()?.TakeDamage(((GunInfo)ItemInfo).Damage);
                RpcShoot(hit.point, hit.normal);
            }
            //RpcShoot(hit.point, hit.normal);
    }

    [Command]
    void RpcShoot(Vector3 hitPosition, Vector3 hitNormal) {
        Collider[] colliders = Physics.OverlapSphere(hitPosition, 0.3f);
        if (colliders.Length != 0) {
            GameObject bulletImpactObj = Instantiate(bulletImpactPrefab, hitPosition + hitNormal * 0.0001f,
            Quaternion.LookRotation(hitNormal, Vector3.up) * bulletImpactPrefab.transform.rotation);
            GameObject MuscleFlashEffect = Instantiate(MuzzleFlash, GunTip.transform.position, ItemGameObject.transform.rotation);
            Destroy(bulletImpactObj, 10f);
            Destroy(MuscleFlashEffect, .5f);
            bulletImpactObj.transform.SetParent(colliders[0].transform);
        }
    }
}