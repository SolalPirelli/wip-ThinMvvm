﻿// Copyright (c) 2014-15 Solal Pirelli
// See License.txt file for more details

using System;

namespace ThinMvvm.Logging
{
    /// <summary>
    /// Marks Commands to indicate the logging parameter that will be passed to the logger when they get executed.
    /// </summary>
    [AttributeUsage( AttributeTargets.Property )]
    public sealed class LogParameterAttribute : Attribute
    {
        /// <summary>
        /// Refers to the parameter of the command inside a path.
        /// </summary>
        internal const string ParameterName = "$Param";

        /// <summary>
        /// The path separator for parameter paths.
        /// </summary>
        internal const char PathSeparator = '.';


        /// <summary>
        /// Gets the path to the parameter.
        /// Use <c>$Param</c> to refer to the command parameter itself.
        /// </summary>
        public string ParameterPath { get; private set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="LogParameterAttribute" /> class with the specified path.
        /// </summary>
        /// <param name="parameterPath">The parameter path.</param>
        public LogParameterAttribute( string parameterPath )
        {
            if ( parameterPath == null )
            {
                throw new ArgumentNullException( "parameterPath" );
            }
            if ( parameterPath.Trim() == string.Empty )
            {
                throw new ArgumentException( "parameterPath must not be empty or consist only of white space.", "parameterPath" );
            }

            ParameterPath = parameterPath;
        }
    }
}