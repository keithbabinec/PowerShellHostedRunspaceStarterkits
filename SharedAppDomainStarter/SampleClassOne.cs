using System;

namespace SharedAppDomainStarter
{
    /// <summary>
    /// An example class object we can use inside the hosted runspace.
    /// </summary>
    public class SampleClassOne
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public SampleClassOne()
        {
            ID = Guid.NewGuid();
        }

        /// <summary>
        /// An ID property.
        /// </summary>
        public Guid ID { get; set; }
    }
}
