﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Aphysoft.Share.Resources.Resources {
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
    internal class Scripts {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Scripts() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Aphysoft.Share.Resources.Resources.Scripts", typeof(Scripts).Assembly);
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
        ///   Looks up a localized string similar to /*! jQuery v3.1.1 | (c) jQuery Foundation | jquery.org/license */
        ///!function(a,b){&quot;use strict&quot;;&quot;object&quot;==typeof module&amp;&amp;&quot;object&quot;==typeof module.exports?module.exports=a.document?b(a,!0):function(a){if(!a.document)throw new Error(&quot;jQuery requires a window with a document&quot;);return b(a)}:b(a)}(&quot;undefined&quot;!=typeof window?window:this,function(a,b){&quot;use strict&quot;;var c=[],d=a.document,e=Object.getPrototypeOf,f=c.slice,g=c.concat,h=c.push,i=c.indexOf,j={},k=j.toString,l=j.hasOwnProperty,m=l.toString,n=m.call(Object) [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string jquery {
            get {
                return ResourceManager.GetString("jquery", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to /*! History.js jQuery Adapter (updated 6/1/2014) */
        ///(function (window, undefined) {
        ///    &quot;use strict&quot;;
        ///
        ///    // Localise Globals
        ///    var
        ///		History = window.History = window.History || {},
        ///		jQuery = window.jQuery;
        ///
        ///    // Check Existence
        ///    if (typeof History.Adapter !== &apos;undefined&apos;) {
        ///        throw new Error(&apos;History.js Adapter has already been loaded...&apos;);
        ///    }
        ///
        ///    // Add the Adapter
        ///    History.Adapter = {
        ///        /**
        ///		 * History.Adapter.bind(el,event,callback)
        ///		 * @param {Element|st [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string libs {
            get {
                return ResourceManager.GetString("libs", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to /*! modernizr 3.3.1 (Custom Build) | MIT *
        /// * http://modernizr.com/download/?-localstorage-performance-touchevents !*/
        ///!function(e,n,t){function r(e,n){return typeof e===n}function o(){var e,n,t,o,i,s,a;for(var f in h)if(h.hasOwnProperty(f)){if(e=[],n=h[f],n.name&amp;&amp;(e.push(n.name.toLowerCase()),n.options&amp;&amp;n.options.aliases&amp;&amp;n.options.aliases.length))for(t=0;t&lt;n.options.aliases.length;t++)e.push(n.options.aliases[t].toLowerCase());for(o=r(n.fn,&quot;function&quot;)?n.fn():n.fn,i=0;i&lt;e.length;i++)s=e[i],a=s.split(&quot;.&quot;),1 [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string modernizr {
            get {
                return ResourceManager.GetString("modernizr", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to // ┌────────────────────────────────────────────────────────────────────┐ \\
        ///// │ Raphaël 2.1.2 - JavaScript Vector Library                          │ \\
        ///// ├────────────────────────────────────────────────────────────────────┤ \\
        ///// │ Copyright © 2008-2012 Dmitry Baranovskiy (http://raphaeljs.com)    │ \\
        ///// │ Copyright © 2008-2012 Sencha Labs (http://sencha.com)              │ \\
        ///// ├────────────────────────────────────────────────────────────────────┤ \\
        ///// │ Licensed under the MIT (http://raphaelj [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string raphael {
            get {
                return ResourceManager.GetString("raphael", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to /*! Aphysoft.Share | share.aphysoft.com */
        ///(function (window, $) {
        ///    &quot;use strict&quot;;
        ///
        ///    window.debug = function (arg1) {
        ///        console.debug(arg1);
        ///    };
        ///    window.assert = function (arg1, arg2, arg3) {
        ///        console.assert(arg1, arg2, arg3)
        ///    };
        ///
        ///    function escapeRegExp(string) {
        ///        return string.replace(/([.*+?^=!:${}()|\[\]\/\\])/g, &quot;\\$1&quot;);
        ///    }
        ///
        ///    // jquery document object
        ///    $.window = $(window);
        ///
        ///    // shorthand type detection for jQuery
        ///    $.isObject = funct [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string share {
            get {
                return ResourceManager.GetString("share", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to // three.min.js - http://github.com/mrdoob/three.js
        ///&apos;use strict&apos;;var THREE=THREE||{REVISION:&quot;50&quot;};void 0===self.console&amp;&amp;(self.console={info:function(){},log:function(){},debug:function(){},warn:function(){},error:function(){}});void 0===self.Int32Array&amp;&amp;(self.Int32Array=Array,self.Float32Array=Array);
        ///(function(){for(var a=0,b=[&quot;ms&quot;,&quot;moz&quot;,&quot;webkit&quot;,&quot;o&quot;],c=0;c&lt;b.length&amp;&amp;!window.requestAnimationFrame;++c){window.requestAnimationFrame=window[b[c]+&quot;RequestAnimationFrame&quot;];window.cancelAnimationFrame=window[b[ [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string three {
            get {
                return ResourceManager.GetString("three", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to /*! Aphysoft.Share UI | share.aphysoft.com */
        ///(function ($) {
        ///    // use strict failed... :(
        ///
        ///    
        ///})(jQuery);
        ///.
        /// </summary>
        internal static string ui {
            get {
                return ResourceManager.GetString("ui", resourceCulture);
            }
        }
    }
}