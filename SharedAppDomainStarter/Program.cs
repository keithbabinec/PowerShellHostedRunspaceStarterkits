using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SharedAppDomainStarter
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine($"Inside Calling Application. Current AppDomain: ID={AppDomain.CurrentDomain.Id}, Name={AppDomain.CurrentDomain.FriendlyName}");

            var scriptContents = new StringBuilder();
            scriptContents.AppendLine("Param($InputObject)");
            scriptContents.AppendLine("");
            scriptContents.AppendLine("Write-Output \"Inside Running Script. Current AppDomain: ID=$([System.AppDomain]::CurrentDomain.Id), Name=$([System.AppDomain]::CurrentDomain.FriendlyName)\"");
            scriptContents.AppendLine("");
            scriptContents.AppendLine("Write-Output \"Checking the value of the .ID property on the input object passed to the script:\"");
            scriptContents.AppendLine("$InputObject.ID");
            scriptContents.AppendLine("");
            scriptContents.AppendLine("Write-Output \"Calling a static method on a class outside the script to return the date:\"");
            scriptContents.AppendLine("[SharedAppDomainStarter.SampleClassTwo]::GetTheDate()");
            scriptContents.AppendLine("");
            scriptContents.AppendLine("Write-Output \"Creating a new instance of a class outside the script, saving a property, and returning it in the output.\"");
            scriptContents.AppendLine("$result = New-Object -TypeName SharedAppDomainStarter.SampleClassThree");
            scriptContents.AppendLine("$result.ScriptCompletedSuccessfully = $true");
            scriptContents.AppendLine("Write-Output $result");

            var scriptParameters = new Dictionary<string, object>()
            {
                { "InputObject", new SampleClassOne() }
            };

            var hosted = new HostedRunspace();
            await hosted.RunScript(scriptContents.ToString(), scriptParameters);

            Console.WriteLine("Script execution completed. Press enter key to exit:");
            Console.Read();
        }
    }
}
