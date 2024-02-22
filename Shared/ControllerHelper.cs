using CSE3902.Commands;
using CSE3902.Interfaces;
using Microsoft.Xna.Framework.Input;

namespace CSE3902.Shared;

public static class ControllerHelper
{
    public static void RegisterStandardCommands(IKeyboardController controller, Game1 game)
    {
        controller.RegisterCommand(Keys.W, new PlayerMoveUpCommand(game));
        controller.RegisterCommand(Keys.S, new PlayerMoveDownCommand(game));
        controller.RegisterCommand(Keys.D, new PlayerMoveRightCommand(game));
        controller.RegisterCommand(Keys.A, new PlayerMoveLeftCommand(game));
        controller.RegisterCommand(Keys.Z, new PlayerUseSwordCommand(game));
        controller.RegisterCommand(Keys.N, new PlayerUseSwordCommand(game));
        controller.RegisterCommand(Keys.E, new PlayerTakeDamageCommand(game));

        controller.RegisterCommand(Keys.ChatPadGreen, new CreateNaviCommand(game));
        controller.RegisterCommand(Keys.Up, new NaviMoveUpCommand(game));
        controller.RegisterCommand(Keys.Down, new NaviMoveDownCommand(game));
        controller.RegisterCommand(Keys.Right, new NaviMoveRightCommand(game));
        controller.RegisterCommand(Keys.Left, new NaviMoveLeftCommand(game));
        controller.RegisterCommand(Keys.OemSemicolon, new NaviControlCommand(game));

        controller.RegisterCommand(Keys.D1, new PlayerUseItemCommand(game));
        controller.RegisterCommand(Keys.Kana, new PlayerUseHadokenCommand(game));
        controller.RegisterCommand(Keys.NumPad1, new PlayerUseItemCommand(game));
        controller.RegisterCommand(Keys.Kanji, new AnnihilationCommand(game));
        controller.RegisterCommand(Keys.Q, new QuitCommand(game));
        controller.RegisterCommand(Keys.R, new ResetCommand(game));
        controller.RegisterCommand(Keys.P, new PauseGameCommand(game));
        controller.RegisterCommand(Keys.M, new MuteGameCommand(game));
        controller.RegisterCommand(Keys.C, new OpenCharacterSelect(game));
    }

    public static void RegisterHudCommands(IKeyboardController controller, Game1 game)
    {
        controller.RegisterCommand(Keys.Right, new RightMenuCommand(game));
        controller.RegisterCommand(Keys.D, new RightMenuCommand(game));
        controller.RegisterCommand(Keys.Left, new LeftMenuCommand(game));
        controller.RegisterCommand(Keys.A, new LeftMenuCommand(game));
        controller.RegisterCommand(Keys.Up, new UpMenuCommand(game));
        controller.RegisterCommand(Keys.W, new UpMenuCommand(game));
        controller.RegisterCommand(Keys.Down, new DownMenuCommand(game));
        controller.RegisterCommand(Keys.S, new DownMenuCommand(game));
        controller.RegisterCommand(Keys.Enter, new SelectGameOverOptionCommand(game));
    }

    public static void RegisterGamePadCommands(IGamePadController controller, Game1 game)
    {
        controller.RegisterCommand(Buttons.LeftThumbstickUp, new PlayerMoveUpCommand(game));
        controller.RegisterCommand(Buttons.LeftThumbstickDown, new PlayerMoveDownCommand(game));
        controller.RegisterCommand(Buttons.LeftThumbstickRight, new PlayerMoveRightCommand(game));
        controller.RegisterCommand(Buttons.LeftThumbstickLeft, new PlayerMoveLeftCommand(game));
        controller.RegisterCommand(Buttons.A, new PlayerUseSwordCommand(game));

        controller.RegisterCommand(Buttons.RightStick, new CreateNaviCommand(game));
        controller.RegisterCommand(Buttons.RightThumbstickUp, new NaviMoveUpCommand(game));
        controller.RegisterCommand(Buttons.RightThumbstickDown, new NaviMoveDownCommand(game));
        controller.RegisterCommand(Buttons.RightThumbstickRight, new NaviMoveRightCommand(game));
        controller.RegisterCommand(Buttons.RightThumbstickLeft, new NaviMoveLeftCommand(game));
        controller.RegisterCommand(Buttons.X, new NaviControlCommand(game));

        controller.RegisterCommand(Buttons.B, new PlayerUseItemCommand(game));
        controller.RegisterCommand(Buttons.LeftStick, new PlayerUseHadokenCommand(game));
        controller.RegisterCommand(Buttons.BigButton, new AnnihilationCommand(game));
        controller.RegisterCommand(Buttons.DPadDown, new QuitCommand(game));
        controller.RegisterCommand(Buttons.DPadRight, new ResetCommand(game));
        controller.RegisterCommand(Buttons.Start, new PauseGameCommand(game));
        controller.RegisterCommand(Buttons.DPadUp, new MuteGameCommand(game));
        controller.RegisterCommand(Buttons.DPadLeft, new OpenCharacterSelect(game));
    }

    public static void RegisterGamePadHudCommands(IGamePadController controller, Game1 game)
    {
        controller.RegisterCommand(Buttons.LeftThumbstickRight, new RightMenuCommand(game));
        controller.RegisterCommand(Buttons.LeftThumbstickLeft, new LeftMenuCommand(game));
        controller.RegisterCommand(Buttons.LeftThumbstickUp, new UpMenuCommand(game));
        controller.RegisterCommand(Buttons.LeftThumbstickDown, new DownMenuCommand(game));
        controller.RegisterCommand(Buttons.X, new SelectGameOverOptionCommand(game));
    }
}