﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mindstep.EasterEgg.Engine.Game;

namespace Mindstep.EasterEgg.Engine
{
    public class ScriptFactory
    {
        Dictionary<string, Type> scriptLibrary = new Dictionary<string, Type>();

        public Script GetScript(string key)
        {
            ValidateScriptType(key, "Script");

            if (!scriptLibrary.ContainsKey(key))
            {
                scriptLibrary.Add(key, Type.GetType(key));
                    
            }

            return (Script)scriptLibrary[key].GetConstructor(Type.EmptyTypes).Invoke(null);
        }

        public ScriptBlock GetBlockScript(string key, GameBlock block, BlockAction action)
        {
            ValidateScriptType(key, "ScriptBlock");

            ScriptBlock script = (ScriptBlock)GetScript(key);
            script.Prepare(block, action);
            return script;
        }

        private bool ValidateScriptType(string key, string type)
        {
            if (!key.StartsWith(type))
            {
                throw new ArgumentException("Script name must begin with \"" + type + "\"");
            }

            return true;
        }
    }
}