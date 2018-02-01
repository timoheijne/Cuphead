public abstract class Modifier
{
    public string Name { get; set; }
    
    public Modifier(string name)
    {
        Name = name;
    }

    public virtual void PlayMusic()
    {
        MusicManager.Instance.PlayNormalMusic();
    }

    public void InitMod(Randomiser r)
    {
        PlayMusic();
        StartMod(r);
    }
    
    public abstract void StartMod(Randomiser r);
    public abstract void UpdateMod(Randomiser r);
    public abstract void DestroyMod(Randomiser r);
}
