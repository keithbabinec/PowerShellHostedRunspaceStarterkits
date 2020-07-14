using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Threading.Tasks;

namespace SharedAppDomainStarter
{
    /// <summary>
    /// Contains functionality for executing PowerShell scripts.
    /// </summary>
    public class HostedRunspace
    {
        /// <summary>
        /// Runs a PowerShell script with parameters and prints the resulting pipeline objects to the console output. 
        /// </summary>
        /// <param name="scriptContents">The script file contents.</param>
        /// <param name="scriptParameters">A dictionary of parameter names and parameter values.</param>
        public async Task RunScript(string scriptContents, Dictionary<string, object> scriptParameters)
        {
            // create a new hosted PowerShell instance using the default runspace.
            // wrap in a using statement to ensure resources are cleaned up.

            using (PowerShell ps = PowerShell.Create())
            {
                // specify the script code to run.
                ps.AddScript(scriptContents);

                // specify the parameters to pass into the script.
                ps.AddParameters(scriptParameters);

                // NOTE: this code sample is similar to the DefaultRunspace example because there isn't any special configuration
                // required to enable a shared appdomain. The shared appdomain can be leveraged in all runspace use cases.
                //
                // The main difference in the code for this project is the script code found in program.cs and the output object handling here
                // just demonstrates how to use it.
                
                // execute the script and await the result.
                var pipelineObjects = await ps.InvokeAsync().ConfigureAwait(false);

                // print the resulting pipeline objects to the console.
                foreach (var item in pipelineObjects)
                {
                    Console.WriteLine(item.BaseObject.ToString());

                    if (item.BaseObject.GetType() == typeof(SampleClassThree))
                    {
                        Console.WriteLine("We found the class object returned to the pipeline.");
                        Console.WriteLine("Here is an example of using that object when it is returned to the hosting process:");
                        Console.WriteLine($"SampleClassThree.ScriptCompletedSuccessfully={(item.BaseObject as SampleClassThree).ScriptCompletedSuccessfully}");
                    }
                }
            }
        }
    }
}
