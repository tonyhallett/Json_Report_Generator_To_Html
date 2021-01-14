using Reinforced.Typings;
using Reinforced.Typings.Ast.TypeNames;
using System;

namespace Json_Report_Generator_To_Html.Code_Generation.ReflectionMemberAttachment
{
    internal static class TypeHelper
    {
        internal static Type[] _GetGenericArguments(this Type t)
        {
#if NETCORE
            return t.GetTypeInfo().GetGenericArguments();
#else
            return t.GetGenericArguments();
#endif
        }
    }
    internal class NullableTypeChanger : SimpleTypeChanger
    {
        private bool writtenNullable;

        

        protected override RtTypeName ChangeType(Type type, RtTypeName rtTypeName)
        {
            // logic to go in SimpleTypeChanger
            switch (rtTypeName)
            {
                case RtAsyncType rtAsync:
                    /*
                        need to know internals - See Typeresolver.ResolveTypeNameInner
                    */
                    break;
                case RtSimpleTypeName rtSimple:
                    //constructor overload with generic arguments but no calls
                    break;


                //children
                case RtTuple rtTuple:
                    //var genericArguments = type._GetGenericArguments();
                    // todo this going to need the TypeResolver
                    break;
                case RtArrayType rtArray:
                case RtDelegateType rtDelegate:
                case RtDictionaryType rtDictionary:
                    break;
            }

            if (type.IsNullable())
            {
                Log("Changing type");
                if (!writtenNullable)
                {
                    CompilationUnitsManager.InsertRawCompilationUnitsAtStart("type Nullable <T> = T | null");
                    writtenNullable = true;
                }

                return new RtSimpleTypeName($"Nullable<{rtTypeName}>"); 
            }
            return null;
        }

        protected void Log(string message)
        {
            ExportContext.Warnings.Add(new Reinforced.Typings.Exceptions.RtWarning(0, null, message));
        }


    }

}
