using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DefaultRunspaceStarter
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var scriptContents = new StringBuilder();
            scriptContents.AppendLine("Param($StrParam, $IntParam)");
            scriptContents.AppendLine("");
            scriptContents.AppendLine("Write-Output \"Starting script\"");
            scriptContents.AppendLine("Write-Output \"This is the value from the first param: $StrParam\"");
            scriptContents.AppendLine("Write-Output \"This is the value from the second param: $IntParam\"");
            scriptContents.AppendLine("");
            scriptContents.AppendLine("Write-Output \"Here is some cmdlet output:\"");
            scriptContents.AppendLine("Get-Date");
            scriptContents.AppendLine("Get-Childitem -File | Select-Object -First 3");
            scriptContents.AppendLine("Get-Service | Select-Object -First 3");
            scriptContents.AppendLine("");

            var scriptParameters = new Dictionary<string, object>()
            {
                { "StrParam", "Hello from script" },
                { "IntParam", 7 }
            };

            var hosted = new HostedRunspace();
            await hosted.RunScript(scriptContents.ToString(), scriptParameters);

            Console.WriteLine("Script execution completed. Press enter key to exit:");
            Console.Read();
        }
    }
}
