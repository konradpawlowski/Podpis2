//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Podpis2.res {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Podpis {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Podpis() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Albit.Podpis.res.Podpis", typeof(Podpis).Assembly);
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
        ///   Looks up a localized string similar to &lt;ds:Signature xmlns:ds=&quot;http://www.w3.org/2000/09/xmldsig#&quot; Id=&quot;Signature-0&quot;&gt;
        ///    &lt;ds:SignedInfo xmlns:ds=&quot;http://www.w3.org/2000/09/xmldsig#&quot; Id=&quot;SignedInfo-0&quot;&gt;
        ///        &lt;ds:CanonicalizationMethod Algorithm=&quot;http://www.w3.org/TR/2001/REC-xml-c14n-20010315&quot;&gt;&lt;/ds:CanonicalizationMethod&gt;
        ///        &lt;ds:SignatureMethod Algorithm=&quot;http://www.w3.org/2000/09/xmldsig#rsa-sha1&quot;&gt;&lt;/ds:SignatureMethod&gt;
        ///        &lt;ds:Reference Id=&quot;SignedProperties-Reference0&quot; Type=&quot;http://uri.etsi.org/01903#SignedProperties&quot; URI=&quot;#Signed [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string DetachedSignedDocumentTemplate {
            get {
                return ResourceManager.GetString("DetachedSignedDocumentTemplate", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;ds:Signature xmlns:ds=&quot;http://www.w3.org/2000/09/xmldsig#&quot; Id=&quot;Signature-0&quot;&gt;
        ///  &lt;ds:SignedInfo Id=&quot;SignedInfo-0&quot;&gt;
        ///    &lt;ds:CanonicalizationMethod Algorithm=&quot;http://www.w3.org/TR/2001/REC-xml-c14n-20010315&quot;&gt;&lt;/ds:CanonicalizationMethod&gt;
        ///    &lt;ds:SignatureMethod Algorithm=&quot;http://www.w3.org/2000/09/xmldsig#rsa-sha1&quot;&gt;&lt;/ds:SignatureMethod&gt;
        ///    &lt;ds:Reference Id=&quot;SignedProperties-Reference0&quot; URI=&quot;#SignedProperties-0&quot; Type=&quot;http://uri.etsi.org/01903#SignedProperties&quot;&gt;
        ///      &lt;ds:DigestMethod Algorithm=&quot;http://www [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string SignedDocumentTemplate {
            get {
                return ResourceManager.GetString("SignedDocumentTemplate", resourceCulture);
            }
        }
    }
}
