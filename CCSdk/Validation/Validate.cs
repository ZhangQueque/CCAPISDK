/***************************************************************************************
 * Author:CC
 * 
 *验证参数类.
 * 
 * DateTime:2017-06-27
 * ************************************************************************************/
using System;

namespace CCSdk.Validation
{
	/// <summary>
	/// Helper methods for validating values
	/// </summary>
	public class Validate
	{
		/// <summary>
		/// Validate an integer value is between the specified values (exclusive of min/max)
		/// </summary>
		/// <param name="value">Value to validate</param>
		/// <param name="min">Exclusive minimum value</param>
		/// <param name="max">Exclusive maximum value</param>
		public static void IsBetween(int value, int min, int max) {
			if (value < min || value > max) {
				throw new ArgumentException(string.Format("Value ({0}) is not between {1} and {2}.", value, min, max));
			}
		}

		/// <summary>
		/// Validate a string length
		/// </summary>
		/// <param name="value">String to be validated</param>
		/// <param name="maxSize">Maximum length of the string</param>
		public static void IsValidLength(string value, int maxSize) {
			if (value == null)
				return;

			if (value.Length > maxSize) {
				throw new ArgumentException(string.Format("String is longer than max allowed size ({0}).", maxSize));
			}
		}
	}
}
