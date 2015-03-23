﻿#region Copyright
// 
// DotNetNuke® - http://www.dotnetnuke.com
// Copyright (c) 2002-2014
// by DotNetNuke Corporation
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated 
// documentation files (the "Software"), to deal in the Software without restriction, including without limitation 
// the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and 
// to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions 
// of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED 
// TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL 
// THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF 
// CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
// DEALINGS IN THE SOFTWARE.
#endregion

using System;
using DotNetNuke.Entities.Modules.Actions;
using DotNetNuke.Entities.Users;
using DotNetNuke.Services.Tokens;

namespace DotNetNuke.UI.Modules.Html5
{
    public class ModuleActionDto
    {
        public string ControlKey { get; set; }
        public string Title { get; set; }
    }

    public class ModuleActionsPropertyAccess : JsonPropertyAccess<ModuleActionDto>
    {
        private readonly ModuleActionCollection _moduleActions;
        private readonly ModuleInstanceContext _moduleContext;

        public ModuleActionsPropertyAccess(ModuleInstanceContext moduleContext, ModuleActionCollection moduleActions)
        {
            _moduleContext = moduleContext;
            _moduleActions = moduleActions;
        }

        protected override string ProcessToken(ModuleActionDto model, UserInfo accessingUser, Scope accessLevel)
        {
            var moduleAction = new ModuleAction(_moduleContext.GetNextActionID())
            {
                Title = model.Title,
                Url = _moduleContext.EditUrl(model.ControlKey)
            };
            _moduleActions.Add(moduleAction);

            return String.Empty;
        }
    }
}
