using Reinforced.Typings.Attributes;

namespace Json_Report_Generator_To_Html.Code_Generation
{
    public class ReflectionMemberAttachmentClassAttribute : TsClassAttribute
    {
        public ReflectionMemberAttachmentClassAttribute()
        {
            CodeGeneratorType = typeof(ReflectionMemberAttachmentInterfaceCodeGenerator);
        }
    }

}
