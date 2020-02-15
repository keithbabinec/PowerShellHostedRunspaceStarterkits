using System;

namespace SharedAppDomainStarter
{
    /// <summary>
    /// An example static class to use in the hosted runspace.
    /// </summary>
    public static class SampleClassTwo
    {
        /// <summary>
        /// A static method that returns the current date.
        /// </summary>
        /// <returns></returns>
        public static DateTime GetTheDate()
        {
            return DateTime.Now;
        }
    }
}
