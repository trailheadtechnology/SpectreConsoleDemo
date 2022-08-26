using Spectre.Console;

var computerInfo = new ComputerInfo();

AnsiConsole.Live(computerInfo.Table).Start(ctx =>
{
    do
    {
        while (!Console.KeyAvailable)
        {
            computerInfo.GenerateFormattedOutput();
            ctx?.Refresh();
            Thread.Sleep(1000);
        }

    } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
});