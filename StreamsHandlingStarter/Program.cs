using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StreamsHandlingStarter
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var scriptContents = new StringBuilder();
            scriptContents.AppendLine("Param($StrParam)");
            scriptContents.AppendLine("");
            scriptContents.AppendLine("Write-Output \"Starting script\"");
            scriptContents.AppendLine("Write-Output \"This is the value from the param: $StrParam\"");
            scriptContents.AppendLine("");
            scriptContents.AppendLine("Write-Output \"Here is some cmdlet output:\"");
            scriptContents.AppendLine("Get-Date");
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
                { "StrParam", "Hello from script" }
            };

            var hosted = new HostedRunspace();
            await hosted.RunScript(scriptContents.ToString(), scriptParameters);

            Console.WriteLine("Script execution completed. Press enter key to exit:");
            Console.Read();
        }
    }
}
