using System;

public class Player
{
    private string theme { get; set; }
    private string audioPack { get; set; }
    private string rotationControls { get; set; }


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
}