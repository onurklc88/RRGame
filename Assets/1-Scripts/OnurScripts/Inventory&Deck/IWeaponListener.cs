
public interface  IWeaponListener 
{
    int CurrentChargeCount { get; set; }
    void OnWeaponChargeLoaded(bool isCharged);
}
