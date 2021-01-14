using Reinforced.Typings;
using Reinforced.Typings.Ast;
using Reinforced.Typings.Visitors.TypeScript;
using System.IO;

namespace Json_Report_Generator_To_Html.Code_Generation.ReflectionMemberAttachment
{
    public class NullableTypingsExportVisitor : TypeScriptExportVisitor
    {
        private TypeChangingVisitor typeChangingVisitor;
        public NullableTypingsExportVisitor(TextWriter writer, ExportContext exportContext) : base(writer, exportContext)
        {
            typeChangingVisitor = new TypeChangingVisitor(exportContext, new NullableTypeChanger());
        }

        public override void Visit(RtNamespace rtNamespace)
        {
            typeChangingVisitor.Visit(rtNamespace);
            base.Visit(rtNamespace);
        }
    }

}
