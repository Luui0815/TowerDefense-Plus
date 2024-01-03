using Godot;

public partial class DefeatScreen : Node
{
    private const double knightWaitTime = 1.8;
    private const double archerWaitTime = 2.2;

    public override void _Ready()
    {
        AnimationPlayer knightPlayer = GetNode<AnimationPlayer>("Panel/FallenKnight/AnimationPlayer");
        AnimationPlayer archerPlayer = GetNode<AnimationPlayer>("Panel/FallenArcher/AnimationPlayer");
        AnimationPlayer wizardPlayer = GetNode<AnimationPlayer>("Panel/VictoriousWizard/AnimationPlayer");
        AnimationPlayer skeletonOnePlayer = GetNode<AnimationPlayer>("Panel/VictoriousSkeleton1/AnimationPlayer");
        AnimationPlayer skeletonTwoPlayer = GetNode<AnimationPlayer>("Panel/VictoriousSkeleton2/AnimationPlayer");
        Timer knightTimer = GetNode<Timer>("Panel/FallenKnight/TimerDeathKnight");
        Timer archerTimer = GetNode<Timer>("Panel/FallenArcher/TimerDeathArcher");

        knightPlayer.Play("AnimationsDefeatScreenKnight/KnightIdle");
        archerPlayer.Play("AnimationDefeatScreenArcher/ArcherIdle");
        wizardPlayer.Play("AnimationDefeatScreenWizard/WizardIdle");
        skeletonOnePlayer.Play("AnimationDefeatScreenSkeleton/Skeleton1Idle");
        skeletonTwoPlayer.Play("AnimationDefeatScreenSkeleton/Skeleton2Idle");

        knightTimer.WaitTime = knightWaitTime;
        knightTimer.Timeout += () => 
        {
            knightPlayer.Stop(false);
            knightPlayer.Play("AnimationsDefeatScreenKnight/KnightGoingDown");
            
            skeletonOnePlayer.Play("AnimationDefeatScreenSkeleton/Skeleton1Attack");
            skeletonOnePlayer.Queue("AnimationDefeatScreenSkeleton/Skeleton1Idle");
        };

        archerTimer.WaitTime = archerWaitTime;
        archerTimer.Timeout += () => 
        {
            archerPlayer.Stop(false);
            archerPlayer.Play("AnimationDefeatScreenArcher/ArcherGoingDown");
            
            skeletonTwoPlayer.Play("AnimationDefeatScreenSkeleton/Skeleton2Attack");
            skeletonTwoPlayer.Queue("AnimationDefeatScreenSkeleton/Skeleton2Idle");
        };
    }

    private void OnMenuButtonPressed()
    {
        GetTree().ChangeSceneToFile("res://scene/ui/MainMenu.tscn");
        GameLevel gameLevel = GetNode<GameLevel>("/root/Level");
        if (gameLevel != null)
        {
            gameLevel.QueueFree();
        }
        GetTree().Paused = false;
    }
}