using UnityEngine; // test

public class PlayerSound : MonoBehaviour
{

	[SerializeField] private AudioClip[] shootingLoop;
	[SerializeField] private AudioClip[] shootStart;
	[SerializeField] private AudioClip[] jump;
	[SerializeField] private AudioClip[] land;
	[SerializeField] private AudioClip[] projectileHit;
	public AudioSource a_shootingLoop;
	private AudioSource a_soundeffects;
	private bool wasShooting;

	private PlayerShooting ps;
	private PlayerInput pi;
	private PlayerMovement pm;
	
	
	// Player starts the music for some reason.
	void Awake ()
	{
		ps = GetComponent<PlayerShooting>();
		pi = GetComponent<PlayerInput>();
		pm = GetComponent<PlayerMovement>();
		a_shootingLoop = gameObject.AddComponent<AudioSource>();
		a_soundeffects = gameObject.AddComponent<AudioSource>();
		pm.OnJump += (s, e) => { if(a_soundeffects != null) a_soundeffects.PlayOneShot(GetRandomClip(jump)); };
		pm.OnLand += (s, e) => { if(a_soundeffects != null) a_soundeffects.PlayOneShot(GetRandomClip(land)); };
		Projectile.OnProjectileHit += (v) => { if(a_shootingLoop != null) a_shootingLoop.PlayOneShot(GetRandomClip(projectileHit)); };

		if(!MenuButtons.CrazyMode) MusicManager.Instance.PlayNormalMusic();
	}
	
	void Update ()
	{
		ShootSound();
	}

	public void StopSounds()
	{
		a_shootingLoop.Stop();
	}
	
	void ShootSound()
	{
		if (Time.timeScale == 0) return;
		
		if (!wasShooting && ps.shooting)
		{
			a_soundeffects.PlayOneShot(GetRandomClip(shootStart));
		}

		if (ps.shooting && !a_shootingLoop.isPlaying)
		{
			a_shootingLoop.clip = GetRandomClip(shootingLoop);
			a_shootingLoop.Play();
		}
		else if (!ps.shooting && a_shootingLoop.isPlaying)
		{
			a_shootingLoop.Stop();
		}
		
		// was shooting
		wasShooting = ps.shooting;
	}

	AudioClip GetRandomClip(AudioClip[] a)
	{
		return a[Random.Range(0, a.Length)];
	}
}
