﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Gexp_DataWorker.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Gexp_DataWorker.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to http://localhost:5820/.
        /// </summary>
        internal static string connectionString {
            get {
                return ResourceManager.GetString("connectionString", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to gexp-db.
        /// </summary>
        internal static string dataBase {
            get {
                return ResourceManager.GetString("dataBase", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to admin.
        /// </summary>
        internal static string password {
            get {
                return ResourceManager.GetString("password", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to C:\Users\UserLB50\Documents\semantic_files\rdf4test.rdf.
        /// </summary>
        internal static string rdf4test {
            get {
                return ResourceManager.GetString("rdf4test", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to admin.
        /// </summary>
        internal static string username {
            get {
                return ResourceManager.GetString("username", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to c:\Users\UserLB50\Downloads\indicator hiv estimated prevalence% 15-49.xlsx.
        /// </summary>
        internal static string xmlFile4Test {
            get {
                return ResourceManager.GetString("xmlFile4Test", resourceCulture);
            }
        }
    }
}
