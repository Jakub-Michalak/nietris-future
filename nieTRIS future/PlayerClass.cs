
using nieTRIS_future;
using System;

public class Player
{
    private Gamemode gamemode { get; set; }
    private string theme { get; set; }
    private string audioPack { get; set; }
    private string rotationControls { get; set; }
    private int startingLevel { get; set; }

    private int lineClears { get; set; } = 0;
    private int tetrisClears { get; set; } = 0;

    private int endlessHighScore { get; set; } = 0;
    private int ultraHighScore { get; set; } = 0;
    private double sprintBestTime { get; set; } = 0;

    public void SetGamemode(Gamemode gamemode)
    {
        this.gamemode = gamemode;
    }

    public Gamemode GetGamemode()
    {
        return gamemode;
    }

    public void SetTheme(string theme)
    {
        this.theme = theme;
    }

    public string GetTheme()
    {
        return theme;
    }

    public void SetAudioPack(string audioPack)
    {
        this.audioPack = audioPack;
    }

    public string GetAudioPack()
    {
        return audioPack;
    }

    public void SetRotationControls(string rotationControls)
    {
        this.rotationControls = rotationControls;
    }

    public string GetRotationControls()
    {
        return rotationControls;
    }

    public void SetStartingLevel(int startingLevel)
    {
        this.startingLevel = startingLevel;
    }

    public int GetStartingLevel()
    {
        return startingLevel;
    }

    public void addLineClears(int newClears)
    {
        lineClears = lineClears + newClears;
    }

    public int getLineClears()
    {
        return lineClears;
    }

    public void addTetrisClears(int newTetrises)
    {
        tetrisClears = tetrisClears + newTetrises;
    }

    public int getTetrisClears()
    {
        return tetrisClears;
    }

    public void endlessSubmitScore(int score)
    {
        if (score > endlessHighScore) endlessHighScore = score;
    }

    public int getEndlessScore()
    {
        return endlessHighScore;
    }

    public void ultraSubmitScore(int score)
    {
        if (score > ultraHighScore) ultraHighScore = score;
    }

    public int getUltraScore()
    {
        return ultraHighScore;
    }

    public void sprintSubmitTime(double time)
    {
        if (sprintBestTime == 0) sprintBestTime = time;
        else if (time < sprintBestTime) sprintBestTime = time;
    }

    public double getSprintTime()
    {
        return sprintBestTime;
    }
}