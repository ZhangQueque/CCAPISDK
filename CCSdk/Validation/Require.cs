/***************************************************************************************
 * Author:CC
 * 
 *验证参数.
 * 
 * DateTime:2017-06-27
 * ************************************************************************************/

using System;

namespace CCSdk.Validation
{
	/// <summary>
	/// Helper methods for validating required values
	/// </summary>
	public class Require
	{
		/// <summary>
		/// Require a parameter to not be null
		/// </summary>
		/// <param name="argumentName">Name of the parameter</param>
		/// <param name="argumentValue">Value of the parameter</param>
		public static void Argument(string argumentName, object argumentValue) {
			if (argumentValue == null) {
				throw new ArgumentException("Argument cannot be null.", argumentName);
			}
		}
	}
}
