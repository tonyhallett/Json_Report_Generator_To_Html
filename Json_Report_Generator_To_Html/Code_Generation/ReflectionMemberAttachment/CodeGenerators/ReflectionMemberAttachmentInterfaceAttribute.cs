using Reinforced.Typings.Attributes;

namespace Json_Report_Generator_To_Html.Code_Generation
{
    public class ReflectionMemberAttachmentInterfaceAttribute : TsInterfaceAttribute
    {
        public ReflectionMemberAttachmentInterfaceAttribute()
        {
            CodeGeneratorType = typeof(ReflectionMemberAttachmentInterfaceCodeGenerator);
        }
    }

}
