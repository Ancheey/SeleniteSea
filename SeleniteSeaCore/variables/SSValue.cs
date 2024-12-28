using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniteSeaCore.variables
{
    public class SSValue(string data)
    {
        public string Data { get; set; } = data;
        public bool TryParseNumber(Dictionary<string, SSValue> RuntimeVars,out double value) => double.TryParse(GetInterpolatedValue(RuntimeVars), out value);
        
        /// <summary>
        /// This method checks for references to other variables and replaces the values
        /// Reference to another value is shown as "@reference@" in the Value of another variable
        /// If it returns values with more references, it means the reference depth has reached the Debug.REFERENCE_DEPTH limit
        /// </summary>
        /// <param name="RuntimeVars">Variables used within the runtime</param>
        /// <returns>Interpolated value</returns>
        public string GetInterpolatedValue(Dictionary<string,SSValue> RuntimeVars)
        {
            int loopcounter = 0; //Here to prevent looping statements
            string valWithReferences = Data;
            int pointersCount = 1; //start so we can check;
            while (pointersCount > 0 && loopcounter < Debug.REFERENCE_DEPTH)
            {
                //This loops till it doesn't find any more references in the text or has reached the limit
                pointersCount = 0;
                foreach (var var in RuntimeVars)
                    if (valWithReferences.Contains($"{{{var.Key}}}"))
                    {
                        pointersCount++;
                        valWithReferences = valWithReferences.Replace($"{{{var.Key}}}", var.Value.Data);//inserting raw data. Don't want recursion in here
                    }
                //Infinite pointer loop safety measure
                loopcounter++;
            }
            return valWithReferences;
        }
        /// <summary>
        /// This method checks for references to other variables and replaces the values
        /// Reference to another value is shown as "@reference@" in the Value of another variable
        /// If it returns values with more references, it means the reference depth has reached the Debug.REFERENCE_DEPTH limit
        /// </summary>
        /// <param name="value">value to interpolate</param>
        /// <param name="RuntimeVars">Variables used within the runtime</param>
        /// <returns></returns>
        public static string InterpolateValue(string value, Dictionary<string, SSValue> RuntimeVars)
        {
            int loopcounter = 0; //Here to prevent looping statements
            string valWithReferences = value;
            int pointersCount = 1; //start so we can check;
            while (pointersCount > 0 && loopcounter < Debug.REFERENCE_DEPTH)
            {
                //This loops till it doesn't find any more references in the text or has reached the limit
                pointersCount = 0;
                foreach (var var in RuntimeVars)
                    if (valWithReferences.Contains($"{{{var.Key}}}"))
                    {
                        pointersCount++;
                        valWithReferences = valWithReferences.Replace($"{{{var.Key}}}", var.Value.Data);//inserting raw data. Don't want recursion in here
                    }
                //Infinite pointer loop safety measure
                loopcounter++;
            }
            return valWithReferences;
        }
        public override string ToString() => Data;
        public enum ValueType
        {
            Boolean,
            Equation,
            Text
        }
    }
}
