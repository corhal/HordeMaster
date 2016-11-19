
public interface IShootable {

	void Shoot(float inaccuracy, int shotsAtOnce, float range, int damagePerShot);

	void StopShooting ();
}
