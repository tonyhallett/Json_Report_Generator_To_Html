using Reinforced.Typings;
using Reinforced.Typings.Ast;
using Reinforced.Typings.Ast.Dependency;
using Reinforced.Typings.Ast.TypeNames;
using System;
using System.Linq;

namespace Json_Report_Generator_To_Html.Code_Generation.ReflectionMemberAttachment
{
    public abstract class ReflectionGeneratedTypeVisitor : ReflectionGeneratedVisitorBase
    {
        protected readonly ExportContext exportContext;

        public ReflectionGeneratedTypeVisitor(ExportContext exportContext)
        {
            this.exportContext = exportContext;
        }

        protected ReflectionGeneratedTypeVisitor()
        {
        }

        #region visits to members
        public override void Visit(RtNamespace rtNamespace)
        {
            foreach (var rtCompilationUnit in rtNamespace.CompilationUnits.OrderBy(c => c is RtCompilationUnit ? ((RtCompilationUnit)c).Order : 0))
            {
                Visit(rtCompilationUnit);
            }
        }

        public override void Visit(RtInterface rtInterface)
        {
            foreach (var member in rtInterface.Members)
            {
                Visit(member);
            }
        }

        public override void Visit(RtClass rtClass)
        {
            foreach (var member in rtClass.Members)
            {
                Visit(member);
            }
        }

        #endregion

        #region todo visits to consider
        public override void Visit(RtArrayType node)
        {
            throw new NotImplementedException();
        }

        public override void Visit(RtAsyncType node)
        {
            throw new NotImplementedException();
        }

        
        public override void Visit(RtDelegateType node)
        {
            throw new NotImplementedException();
        }

        

        public override void Visit(RtDictionaryType node)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region not visited

        public override void Visit(RtArgument node)
        {

        }

        public override void VisitFile(ExportedFile file)
        {
            
        }

        public override void Visit(RtTuple node)
        {
            
        }

        public override void Visit(RtSimpleTypeName node)
        {
            
        }

        public override void Visit(RtReference node)
        {
            
        }

        public override void Visit(RtRaw node)
        {
            
        }

        public override void Visit(RtJsdocNode node)
        {
            
        }

        public override void Visit(RtImport node)
        {
            
        }

        public override void Visit(RtIdentifier node)
        {
            
        }

        public override void Visit(RtEnumValue node)
        {
            
        }

        public override void Visit(RtEnum node)
        {
            
        }
        public override void Visit(RtDecorator node)
        {

        }
        #endregion

        
        protected void Log(string message)
        {
            exportContext.Warnings.Add(new Reinforced.Typings.Exceptions.RtWarning(0, null, message));
        }

    }

}
