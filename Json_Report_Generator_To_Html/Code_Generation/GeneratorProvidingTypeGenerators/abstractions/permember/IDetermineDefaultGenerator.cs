using System.Reflection;

namespace Json_Report_Generator_To_Html.Code_Generation
{
    #region IDetermineDefaultGenerator
    internal interface IDetermineDefaultGenerator
    {
        bool MemberHasDefaultGenerator<TMember>(TMember member) where TMember : MemberInfo;
        bool ParameterHasDefaultGenerator(ParameterInfo parameter);
    }

#endregion
}
