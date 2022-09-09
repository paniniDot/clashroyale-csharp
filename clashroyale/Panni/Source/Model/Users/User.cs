namespace Panni.Source.Model.Users;

public class User
{
    private static readonly IDictionary<UserLevel, int> XpsPerLevel = new Dictionary<UserLevel, int>()
    {
        {UserLevel.Lvl1, 0},
        {UserLevel.Lvl2, 100},
        {UserLevel.Lvl3, 300},
        {UserLevel.Lvl4, 600},
        {UserLevel.Lvl5, 1000}
    };
    private const int PointsPerDestroyedTower = 5;
    
    public string Name { get; }
    public int CurrentXp { get; private set; }
    public UserLevel CurrentLevel { get; private set; }
    public int Plays { get; private set; }
    public int Wins { get; private set; }
    public int DestroyedTowers { get; private set; }
    
    public User(string name)
    {
        this.Name = name;
        this.CurrentXp = 0;
        this.Plays = 0;
        this.Wins = 0;
        this.DestroyedTowers = 0;
        this.CurrentLevel = UserLevel.Lvl1;
    }

    public void DecreaseXps()
    {
        this.CurrentXp -= PointsPerDestroyedTower;
        if (this.CurrentXp < 0)
            this.CurrentXp = 0;
    }
    
    public void AwardXps(int destroyedTowers)
    {
        this.CurrentXp += destroyedTowers * PointsPerDestroyedTower;
        this.CheckCurrentXps();
    }

    private void CheckCurrentXps()
    {
        if (this.CurrentXp < User.XpsPerLevel[this.GetNextLevel()])
            return;
        this.CurrentXp -= User.XpsPerLevel[this.GetNextLevel()];
        this.LevelUp();
    }

    private UserLevel GetNextLevel()
    {
        var enumArray = Enum.GetValues<UserLevel>();
        var index = Array.IndexOf<UserLevel>(enumArray, this.CurrentLevel) + 1;
        return enumArray.Length == index ? UserLevel.Lvl5 : enumArray[index];
    }

    private void LevelUp()
    {
        this.CurrentLevel = this.GetNextLevel();
        this.CheckCurrentXps();
    }

    public void AddWin()
    {
        this.Wins++;
    }

    public void AddPlay()
    {
        this.Plays++;
    }

    public void AddDestroyedTowers(int towers)
    {
        this.DestroyedTowers += towers;
    }

    private bool Equals(User other)
    {
        return Name == other.Name 
               && CurrentXp == other.CurrentXp 
               && CurrentLevel == other.CurrentLevel 
               && Plays == other.Plays 
               && Wins == other.Wins 
               && DestroyedTowers == other.DestroyedTowers;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        return obj.GetType() == this.GetType() && Equals((User) obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Name, CurrentXp, (int) CurrentLevel, Plays, Wins, DestroyedTowers);
    }
}
