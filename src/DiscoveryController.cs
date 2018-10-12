using SwinGameSDK;
using System;

// '' <summary>
// '' The battle phase is handled by the DiscoveryController.
// '' </summary>
public static class DiscoveryController
{
    // '' <summary>
    // '' Handles input during the discovery phase of the game.
    // '' </summary>
    // '' <remarks>
    // '' Escape opens the game menu. Clicking the mouse will
    // '' attack a location.
    // '' </remarks>
    public static void HandleDiscoveryInput()
    {
        if (SwinGame.KeyTyped(KeyCode.EscapeKey))
        {
            GameController.AddNewState(GameState.ViewingGameMenu);
        }

        ///////////////// Changes Background to Menu background
        if (SwinGame.KeyTyped(KeyCode.KeyPad1))
        {
            GameController.AddNewState(GameState.Background1);
        }

        if (SwinGame.KeyTyped(KeyCode.KeyPad2))
        {
            GameController.AddNewState(GameState.Background2);
        }

        if (SwinGame.KeyTyped(KeyCode.KeyPad3))
        {
            GameController.AddNewState(GameState.Background3);
        }

        if (SwinGame.MouseClicked(MouseButton.LeftButton))
        {
            DoAttack();
        }

        Point2D mouse = SwinGame.MousePosition();
        ///////////////// Check when pressing "R" button on image it resets the human player and the computer player
        if (SwinGame.MouseClicked(MouseButton.LeftButton) && mouse.X > UtilityFunctions.FIELD_LEFT + 340 && mouse.Y > UtilityFunctions.FIELD_TOP - 50 && mouse.X < UtilityFunctions.FIELD_LEFT + 340 + 80 && mouse.Y < UtilityFunctions.FIELD_TOP - 50 + 46)
        {
            GameController.HumanPlayer.Reset();
            GameController.ComputerPlayer.Reset();
        }

        // Check when pressing "R" it reset the human player and the computer player
        if (SwinGame.KeyDown(KeyCode.RKey))
        {
            GameController.HumanPlayer.Reset();
            GameController.ComputerPlayer.Reset();
        }
    }

    // '' <summary>
    // '' Attack the location that the mouse if over.
    // '' </summary>
    private static void DoAttack()
    {
        // Calculate the row/col clicked
        Point2D mouse = SwinGame.MousePosition();
        int row = Convert.ToInt32(Math.Floor(((mouse.Y - UtilityFunctions.FIELD_TOP) / (UtilityFunctions.CELL_HEIGHT + UtilityFunctions.CELL_GAP))));
        int col = Convert.ToInt32(Math.Floor(((mouse.X - UtilityFunctions.FIELD_LEFT) / (UtilityFunctions.CELL_WIDTH + UtilityFunctions.CELL_GAP))));

        if (row >= 0 && row < GameController.HumanPlayer.EnemyGrid.Height)
        {
            if (col >= 0 && col < GameController.HumanPlayer.EnemyGrid.Width)
            {
                GameController.Attack(row, col);
            }
        }
    }

    // '' <summary>
    // '' Draws the game during the attack phase.
    // '' </summary>s
    public static void DrawDiscovery()
    {
        //drawing support
        const int SCORES_LEFT = 172;
        const int SHOTS_TOP = 157;
        const int HITS_TOP = 206;
        const int SPLASH_TOP = 256;

        // Check when "Left Shift" or "Right Shift" and "C" are pressed so it draws the field
        if ((SwinGame.KeyDown(KeyCode.LeftShiftKey) || SwinGame.KeyDown(KeyCode.RightShiftKey)) && SwinGame.KeyDown(KeyCode.CKey))
        {
            UtilityFunctions.DrawField(GameController.HumanPlayer.EnemyGrid, GameController.ComputerPlayer, true);
        }
        else
        {
            UtilityFunctions.DrawField(GameController.HumanPlayer.EnemyGrid, GameController.ComputerPlayer, false);
        }

        bool isHuman = (GameController.Game.Player == GameController.HumanPlayer);
        if (isHuman)
        {
            SwinGame.DrawText("Your Turn", Color.White, GameResources.GameFont("Courier"), UtilityFunctions.FIELD_LEFT, UtilityFunctions.FIELD_TOP - 30);
        }
        else
        {
            SwinGame.DrawText("AI Turn", Color.White, GameResources.GameFont("Courier"), UtilityFunctions.FIELD_LEFT, UtilityFunctions.FIELD_TOP - 30);
        }

        ///////////////// Draws the reset button
        SwinGame.DrawBitmap(GameResources.GameImage("ResetButton"), UtilityFunctions.FIELD_LEFT + 340, UtilityFunctions.FIELD_TOP - 50);

        UtilityFunctions.DrawSmallField(GameController.HumanPlayer.PlayerGrid, GameController.HumanPlayer);
        UtilityFunctions.DrawMessage();
        SwinGame.DrawText(GameController.HumanPlayer.Shots.ToString(), Color.White, GameResources.GameFont("Menu"), SCORES_LEFT, SHOTS_TOP);
        SwinGame.DrawText(GameController.HumanPlayer.Hits.ToString(), Color.White, GameResources.GameFont("Menu"), SCORES_LEFT, HITS_TOP);
        SwinGame.DrawText(GameController.HumanPlayer.Missed.ToString(), Color.White, GameResources.GameFont("Menu"), SCORES_LEFT, SPLASH_TOP);
    }
}