public abstract class Modifier
{
    public string Name { get; set; }

    public Modifier(string name)
    {
        Name = name;
    }
    
    public abstract void StartMod(Randomiser r);
    public abstract void UpdateMod(Randomiser r);
    public abstract void DestroyMod(Randomiser r);
}
