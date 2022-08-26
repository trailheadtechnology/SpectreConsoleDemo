using Spectre.Console;
using System.Diagnostics;

public class ComputerInfo
{
    const string commandToExecute = @"wmic cpu get loadpercentage";
    readonly Process proc;
    public Table Table { get; private set; }

    public ComputerInfo()
    {
        // Create a table
        Table = new Table();

        // Add columns
        Table.AddColumn("Sensor");
        Table.AddColumn("Value");

        // Add rows
        Table.AddRow("CPU usage", "N/A");

        // Render the table to the console
        AnsiConsole.Write(Table);

        proc = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = "cmd",
                Arguments = @"/c " + commandToExecute,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                CreateNoWindow = true
            }
        };
    }

    public void GenerateFormattedOutput()
    {
        proc.Start();
        while (!proc.StandardOutput.EndOfStream)
        {
            string line = proc.StandardOutput.ReadLine();
            if (int.TryParse(line, out int loadPercentage))
            {
                Console.Clear();

                string color;
                switch (loadPercentage)
                {
                    case > 80:
                        color = "red";
                        break;
                    case > 30:
                        color = "yellow";
                        break;
                    default:
                        color = "green";
                        break;
                }

                Table.UpdateCell(0, 1, $"[{color}]{loadPercentage}[/]%");
            }
        }
    }
}
