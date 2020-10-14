using System.Linq;

namespace EventSourcingCQRS.Helpers
{
    public class ModelHelper
    {
        public static string ToStringHelper(object obj)
        {
            return "{" + string.Join("}\n{", obj.GetType()
                                .GetProperties()
                                .Select(prop => prop.Name + " : " + prop.GetValue(obj).ToString()) + "}");
        }

    }
}