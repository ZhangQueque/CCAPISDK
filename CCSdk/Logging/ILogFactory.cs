/***************************************************************************************
 * Author:CC
 * 
 *日志工厂类.
 * 
 * DateTime:2017-06-27
 * ************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CCSdk.Logging
{
    /// <summary>
    /// LogFactory Interface
    /// </summary>
    public interface ILogFactory
    {
        /// <summary>
        /// Gets the log by name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        ILog GetLog(string name);


        /// <summary>
        /// Gets the log from the specific repository.
        /// </summary>
        /// <param name="repositoryName">Name of the repository.</param>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        ILog GetLog(string repositoryName, string name);
    }
}
