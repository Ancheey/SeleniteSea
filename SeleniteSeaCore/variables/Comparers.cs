using System;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Threading.Tasks;

namespace SeleniteSeaCore.variables
{
    public static class Comparers
    {
        public static Dictionary<string, SSValueComparerType> ComparerRegistry { get; private set; } = [];
        public static void Register<T>() where T : SSValueComparerType
        {
            try
            {
                SSValueComparerType? a = (SSValueComparerType?)Activator.CreateInstance(typeof(T));
                if (a is null)
                {
                    Debug.Log(StatusCode.Error, $"Failed comparer registration for {typeof(T).Name}. Lacking a constructor", null);
                    return;
                }
                ComparerRegistry.Add(typeof(T).ToString(), a);
            }
            catch (Exception e) 
            {
                Debug.Log(StatusCode.Error, $"Failed comparer registration for {typeof(T).Name}. {e.ToString()}", null);
            }
            
        }
        static Comparers()
        {
            Register<SSValueComparerTypeEqualValue>();
            Register<SSValueComparerTypeNotEqualValue>();
            Register<SSValueComparerTypeShorterThan>();
            Register<SSValueComparerTypeShorterOrEqual>();
            Register<SSValueComparerTypeEqualLength>();
            Register<SSValueComparerTypeLongerThan>();
            Register<SSValueComparerTypeLongerOrEqual>();
            Register<SSValueComparerTypeNumericLessThan>();
            Register<SSValueComparerTypeNumericMoreThan>();
            Register<SSValueComparerTypeNumericLessOrEqual>();
            Register<SSValueComparerTypeNumericGreaterOrEqual>();
            Register<SSValueComparerTypeVariableExists>();
        }
    }
    public enum CompareResults
    {
        True,
        False,
        NaN,
        NeV //Not enough Values
    }
    public abstract class SSValueComparerType
    {
        /// <summary>
        /// Text of the compariso. @1 @2 etc. will be replaced with values
        /// </summary>
        public abstract string Text { get; }
        public override string ToString() => Text;
        /// <summary>
        /// Amount of values needed for comparison
        /// </summary>
        public abstract int ComparedValues { get; }
        public abstract CompareResults Compare(Dictionary<string, SSValue> RuntimeVars, params SSValue[] args);
    }
    public class SSValueComparerTypeEqualValue : SSValueComparerType
    {
        public override string Text => "{A} has equal to {B}";

        public override int ComparedValues => 2;

        public override CompareResults Compare(Dictionary<string, SSValue> RuntimeVars, params SSValue[] args)
        {
            if (args.Length < ComparedValues)
                return CompareResults.NeV;
            return (args[0].GetInterpolatedValue(RuntimeVars) == args[1].GetInterpolatedValue(RuntimeVars)) ? CompareResults.True : CompareResults.False;
        }
    }
    public class SSValueComparerTypeNotEqualValue : SSValueComparerType
    {
        public override string Text => "{A} is not equal to {B}";

        public override int ComparedValues => 2;

        public override CompareResults Compare(Dictionary<string, SSValue> RuntimeVars, params SSValue[] args)
        {
            if (args.Length < ComparedValues)
                return CompareResults.NeV;
            return (args[0].GetInterpolatedValue(RuntimeVars) != args[1].GetInterpolatedValue(RuntimeVars)) ? CompareResults.True : CompareResults.False;
        }
    }
    public class SSValueComparerTypeShorterThan : SSValueComparerType
    {
        public override string Text => "{A} is shorter than {B}";

        public override int ComparedValues => 2;

        public override CompareResults Compare(Dictionary<string, SSValue> RuntimeVars, params SSValue[] args)
        {
            if (args.Length < ComparedValues)
                return CompareResults.NeV;
            return (args[0].GetInterpolatedValue(RuntimeVars).Length < args[1].GetInterpolatedValue(RuntimeVars).Length) ? CompareResults.True : CompareResults.False;
        }
    }
    public class SSValueComparerTypeShorterOrEqual : SSValueComparerType
    {
        public override string Text => "{A} is shorter or equal length to {B}";

        public override int ComparedValues => 2;

        public override CompareResults Compare(Dictionary<string, SSValue> RuntimeVars, params SSValue[] args)
        {
            if (args.Length < ComparedValues)
                return CompareResults.NeV;
            return (args[0].GetInterpolatedValue(RuntimeVars).Length <= args[1].GetInterpolatedValue(RuntimeVars).Length) ? CompareResults.True : CompareResults.False;
        }
    }
    public class SSValueComparerTypeEqualLength : SSValueComparerType
    {
        public override string Text => "{A} is equal length to {B}";

        public override int ComparedValues => 2;

        public override CompareResults Compare(Dictionary<string, SSValue> RuntimeVars, params SSValue[] args)
        {
            if (args.Length < ComparedValues)
                return CompareResults.NeV;
            return (args[0].GetInterpolatedValue(RuntimeVars).Length == args[1].GetInterpolatedValue(RuntimeVars).Length) ? CompareResults.True : CompareResults.False;
        }
    }
    public class SSValueComparerTypeLongerThan : SSValueComparerType
    {
        public override string Text => "{A} is longer to {B}";

        public override int ComparedValues => 2;

        public override CompareResults Compare(Dictionary<string, SSValue> RuntimeVars, params SSValue[] args)
        {
            if (args.Length < ComparedValues)
                return CompareResults.NeV;
            return (args[0].GetInterpolatedValue(RuntimeVars).Length > args[1].GetInterpolatedValue(RuntimeVars).Length) ? CompareResults.True : CompareResults.False;
        }
    }
    public class SSValueComparerTypeLongerOrEqual : SSValueComparerType
    {
        public override string Text => "{A} is longer or equal length to {B}";

        public override int ComparedValues => 2;

        public override CompareResults Compare(Dictionary<string, SSValue> RuntimeVars, params SSValue[] args)
        {
            if (args.Length < ComparedValues)
                return CompareResults.NeV;
            return (args[0].GetInterpolatedValue(RuntimeVars).Length >= args[1].GetInterpolatedValue(RuntimeVars).Length) ? CompareResults.True : CompareResults.False;
        }
    }
    public class SSValueComparerTypeNumericLessThan : SSValueComparerType
    {
        public override string Text => "{A} is less than {B}";

        public override int ComparedValues => 2;

        public override CompareResults Compare(Dictionary<string, SSValue> RuntimeVars, params SSValue[] args)
        {
            if (args.Length < ComparedValues)
                return CompareResults.NeV;
            if (args[0].TryParseNumber(RuntimeVars, out double a1) && args[1].TryParseNumber(RuntimeVars, out double b1))
                return (a1 < b1) ? CompareResults.True : CompareResults.False;
            return CompareResults.NaN;
        }
    }
    public class SSValueComparerTypeNumericMoreThan : SSValueComparerType
    {
        public override string Text => "{A} is greater than {B}";

        public override int ComparedValues => 2;

        public override CompareResults Compare(Dictionary<string, SSValue> RuntimeVars, params SSValue[] args)
        {
            if (args.Length < ComparedValues)
                return CompareResults.NeV;
            if (args[0].TryParseNumber(RuntimeVars, out double a1) && args[1].TryParseNumber(RuntimeVars, out double b1))
                return (a1 > b1) ? CompareResults.True : CompareResults.False;
            return CompareResults.NaN;
        }
    }
    public class SSValueComparerTypeNumericLessOrEqual : SSValueComparerType
    {
        public override string Text => "{A} is less or equal to {B}";

        public override int ComparedValues => 2;

        public override CompareResults Compare(Dictionary<string, SSValue> RuntimeVars, params SSValue[] args)
        {
            if (args.Length < ComparedValues)
                return CompareResults.NeV;
            if (args[0].TryParseNumber(RuntimeVars, out double a1) && args[1].TryParseNumber(RuntimeVars, out double b1))
                return (a1 <= b1) ? CompareResults.True : CompareResults.False;
            return CompareResults.NaN;
        }
    }
    public class SSValueComparerTypeNumericGreaterOrEqual : SSValueComparerType
    {
        public override string Text => "{A} is greater or equal to {B}";

        public override int ComparedValues => 2;

        public override CompareResults Compare(Dictionary<string, SSValue> RuntimeVars, params SSValue[] args)
        {
            if (args.Length < ComparedValues)
                return CompareResults.NeV;
            if (args[0].TryParseNumber(RuntimeVars, out double a1) && args[1].TryParseNumber(RuntimeVars, out double b1))
                return (a1 >= b1) ? CompareResults.True : CompareResults.False;
            return CompareResults.NaN;
        }
    }
    public class SSValueComparerTypeVariableExists : SSValueComparerType
    {
        public override string Text => "Variable of name {A} exists";

        public override int ComparedValues => 1;

        public override CompareResults Compare(Dictionary<string, SSValue> RuntimeVars, params SSValue[] args)
        {
            if (args.Length < ComparedValues)
                return CompareResults.NeV;
            if (RuntimeVars.ContainsKey(args[0].GetInterpolatedValue(RuntimeVars)))
                return CompareResults.True;
            return CompareResults.False;
        }
    }
}

