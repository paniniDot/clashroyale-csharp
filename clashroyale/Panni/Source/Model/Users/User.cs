namespace Panni.Source.Model.Users;

/// <summary>
/// Users that play the game.
/// </summary>
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
    
    /// <summary>
    /// The nickname of the user.
    /// </summary>
    public string Name { get; }
    
    /// <summary>
    /// Current Xps owned by the user.
    /// </summary>
    public int CurrentXp { get; private set; }
    
    /// <summary>
    /// Current level of the user.
    /// </summary>
    public UserLevel CurrentLevel { get; private set; }
    
    /// <summary>
    /// The number of matches played by this user.
    /// </summary>
    public int Plays { get; private set; }
    
    /// <summary>
    /// The number of win achieved by this user.
    /// </summary>
    public int Wins { get; private set; }
    
    /// <summary>
    /// The number of towers destroyed by this user.
    /// </summary>
    public int DestroyedTowers { get; private set; }
    
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="name">The nickname of the user.</param>
    public User(string name)
    {
        this.Name = name;
        this.CurrentXp = 0;
        this.Plays = 0;
        this.Wins = 0;
        this.DestroyedTowers = 0;
        this.CurrentLevel = UserLevel.Lvl1;
    }

    /// <summary>
    /// Xps decrease in case of lose.
    /// </summary>
    public void DecreaseXps()
    {
        this.CurrentXp -= PointsPerDestroyedTower;
        if (this.CurrentXp < 0)
            this.CurrentXp = 0;
    }
    
    /// <summary>
    /// Increase the current xp user owns.
    /// </summary>
    /// <param name="destroyedTowers">How many towers the user has destroyed during the last match</param>
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
        var index = Array.IndexOf(enumArray, this.CurrentLevel) + 1;
        return enumArray.Length == index ? UserLevel.Lvl5 : enumArray[index];
    }

    private void LevelUp()
    {
        this.CurrentLevel = this.GetNextLevel();
        this.CheckCurrentXps();
    }

    /// <summary>
    /// Increase the number of wins.
    /// </summary>
    public void AddWin()
    {
        this.Wins++;
    }

    /// <summary>
    /// Increase the number of matches played.
    /// </summary>
    public void AddPlay()
    {
        this.Plays++;
    }

    /// <summary>
    /// Increment the number of total destroyed towers.
    /// </summary>
    /// <param name="towers">The number of destroyed towers during the last match.</param>
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
