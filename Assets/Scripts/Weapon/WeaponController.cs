using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private WeaponBase[] weapons;

    // Update is called once per frame
    void Update()
    {
        foreach(WeaponBase weapon in weapons)
        {
            weapon.TryAttack();
        }
    }
}
