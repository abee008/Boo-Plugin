﻿using System;
using System.Collections.Generic;
using Boo.Lang.Compiler.Ast;
using Boo.Lang.Compiler.TypeSystem;
using Hill30.BooProject.LanguageService.Colorizer;
using Boo.Lang.Compiler.TypeSystem.Reflection;

namespace Hill30.BooProject.LanguageService.NodeMapping
{
    public class MappedTypeNode : MappedNode
    {
        private string format;
        private string quickInfoTip;

        public MappedTypeNode(BufferMap bufferMap, ClassDefinition node)
            : base(bufferMap, node, node.Name.Length)
        {
            format = Formats.BooType;
            quickInfoTip = null;
        }

        public MappedTypeNode(BufferMap bufferMap, SimpleTypeReference node)
            : base(bufferMap, node, node.Name.Length)
        {
        }

        public override string Format
        {
            get { return format; }
        }

        public override string QuickInfoTip
        {
            get { return quickInfoTip; }
        }

        internal protected override void Resolve()
        {
            var type = TypeSystemServices.GetType(Node);
            if (type != null)
            {
                format = Formats.BooType;
                var clrType = type as ExternalType;
                if (clrType != null)
                {
                    var prefix = "struct ";
                    if (clrType.ActualType.IsClass)
                        prefix = "class ";
                    if (clrType.ActualType.IsInterface)
                        prefix = "interface ";
                    if (clrType.ActualType.IsEnum)
                        prefix = "enumeration ";
                    quickInfoTip = prefix + clrType.ActualType.FullName;
                }
                else
                    quickInfoTip = "class " + type.FullName;
            }
        }

    }
}
