using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CustomRunspaceStarter
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var scriptContents = new StringBuilder();
            scriptContents.AppendLine("Param($StrParam, $IntParam)");
            scriptContents.AppendLine("");
            scriptContents.AppendLine("Write-Output \"Message from inside the running script\"");
            scriptContents.AppendLine("Write-Output \"This is the value from the first param: $StrParam\"");
            scriptContents.AppendLine("Write-Output \"This is the value from the second param: $IntParam\"");
            scriptContents.AppendLine("");
            scriptContents.AppendLine("Write-Output \"Here are the loaded modules in the script:\"");
            scriptContents.AppendLine("Get-Module");
            scriptContents.AppendLine("");
            scriptContents.AppendLine("# write some data to the info/warning streams");
            scriptContents.AppendLine("");
            scriptContents.AppendLine("Write-Host \"A message from write-host\"");
            scriptContents.AppendLine("Write-Information \"A message from write-information\"");
            scriptContents.AppendLine("");
            scriptContents.AppendLine("Write-Warning \"A message from write-warning\"");
            scriptContents.AppendLine("");
            scriptContents.AppendLine("# write a message to the error stream by throwing a non-terminating error");
            scriptContents.AppendLine("# note: terminating errors will stop the pipeline.");
            scriptContents.AppendLine("Get-ChildItem -Directory \"folder-doesnt-exist\"");
            scriptContents.AppendLine("");

            var scriptParameters = new Dictionary<string, object>()
            {
                { "StrParam", "Hello from script" },
                { "IntParam", 7 }
            };

            Console.WriteLine("Initializing runspace pool.");
            
            // The 'Az' module (bundle) is the Windows Azure PowerShell module that works on both PS 5.1 and PS Core.
            // For this example to work, the Az module should already be installed.
            
            var modulesToLoad = new string[] { "Az.Accounts", "Az.Compute" };
            
            var hosted = new HostedRunspace();
            hosted.InitializeRunspaces(2, 10, modulesToLoad);

            Console.WriteLine("Calling RunScript()");
            await hosted.RunScript(scriptContents.ToString(), scriptParameters);

            Console.WriteLine("Script execution completed. Press enter key to exit:");
            Console.Read();
        }
    }
}
