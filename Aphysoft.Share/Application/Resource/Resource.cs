﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Drawing;
using System.Drawing.Imaging;
using System.Web.SessionState;
using Aphysoft.Common;
using System.Resources;
using System.Text;

namespace Aphysoft.Share
{
    public sealed class Resource
    {
        #region Resource Provider

        #region Fields

        private static Dictionary<string, Resource> registeredResources = new Dictionary<string, Resource>();
        private static Dictionary<string, string> keyHashes = new Dictionary<string, string>();
        private static bool resourceLoaded = false;

        private static int commonResourceScriptIndex = 100;
        private static int commonResourceCSSIndex = 100;

        #endregion

        #region Const        

        public const string CommonResourceScript = "script_common";
        public const string CommonResourceCSS = "css_common";

        #endregion

        #region Internal Resources

        internal static void InternalResourceLoad()
        {
            if (Settings.DevelopmentMode)
            {
                Resource.Common(
                    Resource.Register("development_javascript", ResourceType.JavaScript, "~/Development/javascript.js").NoCache().NoMinify()
                );

                Content.Register("test",
                    new ContentPage[] {
                        new ContentPage("/test")
                    },
                    new ContentPackage(
                        Resource.Register("test", ResourceType.JavaScript, "~/Development/test.js"),
                        null));
            }
            
            //
            // css
            //
            if (Settings.EnableUI)
            {
                Resource.Register("css_ui", ResourceType.CSS, Resources.Css.ResourceManager, "ui");
                Resource.Group(CommonResourceCSS, "css_ui", 1);
            }

            //
            // scripts
            //
            Resource.Register("script_jquery", "jquery", ResourceType.JavaScript, Resources.Scripts.ResourceManager, "jquery").NoMinify();
            Resource.Group(CommonResourceScript, "script_jquery", 1);

            Resource.Register("script_modernizr", ResourceType.JavaScript, Resources.Scripts.ResourceManager, "modernizr").NoMinify();
            Resource.Group(CommonResourceScript, "script_modernizr", 2);

            Resource.Register("script_libs", ResourceType.JavaScript, Resources.Scripts.ResourceManager, "libs");
            Resource.Group(CommonResourceScript, "script_libs", 3);

            if (Settings.THREE)
            {
                Resource.Register("script_three", ResourceType.JavaScript, Resources.Scripts.ResourceManager, "three").NoMinify();
                Resource.Group(CommonResourceScript, "script_three", 4);
            }

            if (Settings.Raphael)
            {
                Resource.Register("script_raphael", ResourceType.JavaScript, Resources.Scripts.ResourceManager, "raphael").NoMinify();
                Resource.Group(CommonResourceScript, "script_raphael", 5);
            }

            Resource.Register("script_share", ResourceType.JavaScript, Resources.Scripts.ResourceManager, "share").NoMinify();
            Resource.Group(CommonResourceScript, "script_share", 10);

            if (Settings.EnableUI)
            {                
                Resource.Register("script_ui", ResourceType.JavaScript, Resources.Scripts.ResourceManager, "ui");
                Resource.Group(CommonResourceScript, "script_ui", 11);

                Resource.Register("xhr_content_provider", ResourceType.JSON, Content.Begin, Content.End);
            }

            // Images
            Resource.Register("image_shortcuticon", ResourceType.PNG, Resources.Images.ResourceManager, "shortcuticon");

            // XHR 
            Resource.Register("xhr_stream", ResourceType.Text, Provider.StreamBeginProcessRequest, Provider.StreamEndProcessRequest)
                .NoBufferOutput().AllowOrigin("http://" + Settings.PageDomain).AllowCredentials();
            Resource.Register("xhr_provider", ResourceType.JSON, Provider.ProviderBeginProcessRequest, Provider.ProviderEndProcessRequest);

            // Service
            //Resource.Register("service", ResourceType.JSON, Provider.ServiceBegin)

            if (Settings.EnableUI)
            {
                if (Settings.FontHeadings == Settings.fontHeadingsDefault)
                {
                    WebFont.Register("avenir85", "Avenir", null, WebFontWeight.Normal,
                        Resource.Register("font_avenir85_ttf", ResourceType.TTF, Resources.Fonts.ResourceManager, "avenir85_ttf"),
                        Resource.Register("font_avenir85_woff", ResourceType.WOFF, Resources.Fonts.ResourceManager, "avenir85_woff"),
                        null);
                }

                if (Settings.FontBody == Settings.fontBodyDefault)
                {
                    WebFont.Register("segoeuil", "Segoe UI", "Segoe UI Light", WebFontWeight.Weight200,
                        Resource.Register("font_segoeuil_ttf", ResourceType.TTF, Resources.Fonts.ResourceManager, "segoeuil_ttf"),
                        Resource.Register("font_segoeuil_woff", ResourceType.WOFF, Resources.Fonts.ResourceManager, "segoeuil_woff"),
                        null);
                    WebFont.Register("segoeuisl", "Segoe UI", "Segoe UI SemiLight", WebFontWeight.Weight300,
                        Resource.Register("font_segoeuis_ttf", ResourceType.TTF, Resources.Fonts.ResourceManager, "segoeuisl_ttf"),
                        Resource.Register("font_segoeuis_woff", ResourceType.WOFF, Resources.Fonts.ResourceManager, "segoeuisl_woff"),
                        null);
                    WebFont.Register("segoeui", "Segoe UI", null, WebFontWeight.Normal,
                        Resource.Register("font_segoeui_ttf", ResourceType.TTF, Resources.Fonts.ResourceManager, "segoeui_ttf"),
                        Resource.Register("font_segoeui_woff", ResourceType.WOFF, Resources.Fonts.ResourceManager, "segoeui_woff"),
                        null);
                    WebFont.Register("seguisb", "Segoe UI", "Segoe UI SemiBold", WebFontWeight.Weight600,
                        Resource.Register("font_seguisb_ttf", ResourceType.TTF, Resources.Fonts.ResourceManager, "seguisb_ttf"),
                        Resource.Register("font_seguisb_woff", ResourceType.WOFF, Resources.Fonts.ResourceManager, "seguisb_woff"),
                        null);
                    WebFont.Register("segoeuib", "Segoe UI", null, WebFontWeight.Bold,
                        Resource.Register("font_segoeuib_ttf", ResourceType.TTF, Resources.Fonts.ResourceManager, "segoeuib_ttf"),
                        Resource.Register("font_segoeuib_woff", ResourceType.WOFF, Resources.Fonts.ResourceManager, "segoeuib_woff"),
                        null);
                }

                WebFont.Register("keepcalmm", "Keep Calm", null, WebFontWeight.Normal,
                    Resource.Register("font_keepcalmm_ttf", ResourceType.TTF, Resources.Fonts.ResourceManager, "keepcalmm_ttf"),
                    Resource.Register("font_keepcalmm_woff", ResourceType.WOFF, Resources.Fonts.ResourceManager, "keepcalmm_woff"),
                    null);
            }
        }

        #endregion

        #region Internal Methods
        
        internal static void Init()
        {
            if (!resourceLoaded)
            {
                resourceLoaded = true;

                Resource.InternalResourceLoad();
            }
        }

        internal static IAsyncResult Begin(HttpContext context, AsyncCallback cb, object extraData)
        {


            return null;
        }

        internal static void End(IAsyncResult result)
        {

        }

        #endregion

        #region Methods

        public static Resource Get(string key)
        {
            Resource con = null;

            if (keyHashes.ContainsKey(key))
            {
                string akey = keyHashes[key];

                if (registeredResources.ContainsKey(akey))
                    con = registeredResources[akey];
            }
            else
            {
                // search by name, key = name
                if (registeredResources.ContainsKey(key))
                {
                    con = registeredResources[key];
                }
            }

            return con;
        }

        public static string GetPath(string key)
        {
            if (registeredResources.ContainsKey(key))
            {
                Resource resource = registeredResources[key];
                
                string fileHash;

                if (resource.BeginHandler == null)
                {
                    string fileBeenUpdatedSignature = null;
                    resource.ResourceCheck();

                    if (resource.groupSources != null) // group resource
                    {
                        resource.ResourceCheck();

                        string[] igus = new string[resource.groupSources.Count];

                        int i = 0;
                        foreach (KeyValuePair<int, ResourceGroupEntry> kvp in resource.groupSources)
                        {
                            ResourceGroupEntry gentry = kvp.Value;

                            string signature = gentry.Signature;

                            igus[i++] = signature;
                        }
                        
                        fileBeenUpdatedSignature = string.Join("", igus);
                    }
                    else
                    {
                        fileBeenUpdatedSignature = resource.MD5;
                    }

                    fileHash = Hasher.Basic(StringHelper.Create(resource.Key, fileBeenUpdatedSignature));
                }
                else fileHash = "res";

                string path = string.Format("/{0}/{1}/{2}{3}", 
                    Settings.ResourceProviderPath, 
                    resource.RealName ? resource.Key : resource.KeyHash, 
                    fileHash,
                    resource.FileExtension);

                return path;
            }
            else
                return string.Empty;
        }

        #endregion
        
        #region Register Resources

        /// <summary>
        /// Adds JavaScript or CSS resource as common in page resource.
        /// </summary>
        public static void Common(Resource resource)
        {
            if (resource != null && resource.BeginHandler == null)
            {
                if (resource.ResourceType == ResourceType.JavaScript)
                    Resource.Group(CommonResourceScript, resource.Key, commonResourceScriptIndex++);
                else if (resource.ResourceType == ResourceType.CSS)
                    Resource.Group(CommonResourceCSS, resource.Key, commonResourceCSSIndex++);
            }
        }

        internal static Resource Group(string groupKey, string key, int position)
        {
            Resource resource = Resource.Get(key);

            if (resource != null && resource.BeginHandler == null)
            {
                Resource groupResource = Resource.Register(groupKey, resource.ResourceType);

                if (groupResource.groupSources == null)
                {
                    groupResource.groupSources = new SortedList<int, ResourceGroupEntry>();
                }

                if (!groupResource.groupSources.ContainsKey(position))
                {
                    ResourceGroupEntry entry = new ResourceGroupEntry(resource, "-");

                    groupResource.groupSources.Add(position, entry);

                    resource.groupResource = groupResource;

                    return groupResource;
                }

                return null;
            }
            else
                return null;
        }

        private static Resource Register(string key, string keyHash, ResourceType resourceType)
        {
            Resource resource;

            if (!registeredResources.ContainsKey(key))
            {
                resource = new Resource(key, keyHash, resourceType);

                lock (registeredResources)
                {
                    if (!registeredResources.ContainsKey(key))
                    {
                        keyHashes.Add(resource.KeyHash, key);
                        registeredResources.Add(key, resource);
                    }
                }
            }
            else
                resource = registeredResources[key];            

            return resource;
        }

        private static Resource Register(string key, ResourceType resourceType)
        {
            return Resource.Register(key, null, resourceType);
        }

        public static Resource Register(string key, string keyHash, ResourceType resourceType, ResourceBeginProcessRequest beginHandler, ResourceEndProcessRequest endHandler)
        {
            Resource resource = Resource.Register(key, keyHash, resourceType);

            resource.SetHandler(beginHandler, endHandler);

            return resource;
        }

        public static Resource Register(string key, ResourceType resourceType, ResourceBeginProcessRequest beginHandler, ResourceEndProcessRequest endHandler)
        {
            return Resource.Register(key, null, resourceType, beginHandler, endHandler);
        }

        public static Resource Register(string key, string keyHash, ResourceType resourceType, string rootRelativePath)
        {  
            string physicalPath = Path.PhysicalPath(rootRelativePath);            

            FileInfo info = new FileInfo(physicalPath);

            if (!info.Exists)
                return null;
            else
            {
                Resource resource = Resource.Register(key, keyHash, resourceType);

                resource.OriginalFilePath = physicalPath;
                resource.LastModified = info.LastWriteTime;
                resource.SetData(File.ReadAllBytes(physicalPath));

                return resource;
            }
        }

        public static Resource Register(string key, ResourceType resourceType, string rootRelativePath)
        {
            return Resource.Register(key, null, resourceType, rootRelativePath);
        }

        public static Resource Register(string key, string keyHash, ResourceType resourceType, ResourceManager resourceManager, string objectName)
        {
            Resource resource = Resource.Register(key, keyHash, resourceType);

            object obj = resourceManager.GetObject(objectName);
            if (obj.GetType() == typeof(string)) resource.SetData((string)obj);
            else if (obj.GetType() == typeof(Bitmap))
            {
                Bitmap b = (Bitmap)obj;

                using (MemoryStream ms = new MemoryStream())
                {
                    if (resourceType == ResourceType.PNG)
                        b.Save(ms, ImageFormat.Png);
                    else if (resourceType == ResourceType.JPEG)
                        b.Save(ms, ImageFormat.Jpeg);
                    resource.SetData(ms.ToArray());
                }

            }
            else resource.SetData((Byte[])obj);

            return resource;
        }
        
        public static Resource Register(string key, ResourceType resourceType, ResourceManager resourceManager, string objectName)
        {
            return Resource.Register(key, null, resourceType, resourceManager, objectName);
        }

        #endregion

        #endregion

        #region Resource

        #region Fields

        private string key;

        public string Key
        {
            get { return key; }
        }

        private string keyHash;

        public string KeyHash
        {
            get { return keyHash; }
        }

        private ResourceType resourceType;

        public ResourceType ResourceType
        {
            get { return resourceType; }
            set { resourceType = value; }
        }

        private string fileExtension;

        public string FileExtension
        {
            get 
            {
                if (fileExtension == null)
                {
                    switch (resourceType)
                    {
                        case ResourceType.CSS: fileExtension = ".css"; break;
                        case ResourceType.HTML: fileExtension = ".html"; break;
                        case ResourceType.JavaScript: fileExtension = ".js"; break;
                        case ResourceType.JPEG: fileExtension = ".jpg"; break;
                        case ResourceType.JSON: fileExtension = ".json"; break;
                        case ResourceType.Text: fileExtension = ""; break;
                        case ResourceType.PNG: fileExtension = ".png"; break;
                        case ResourceType.TTF: fileExtension = ".ttf"; break;
                        case ResourceType.WOFF: fileExtension = ".woff"; break;
                        default: fileExtension = ".bin"; break;
                    }
                }

                return fileExtension; 
            }
            set
            {
                string ival = value;
                if (ival.Length > 1 && !ival.StartsWith("."))
                    fileExtension = "." + ival;
                else if (ival == "." || ival == null)
                    fileExtension = null;
                else
                    fileExtension = ival;
            }
        }

        public string MimeType
        {
            get
            {
                string mimeType;

                switch (resourceType)
                {
                    case ResourceType.CSS: mimeType = "text/css"; break;
                    case ResourceType.HTML: mimeType = "text/html"; break;
                    case ResourceType.JavaScript: mimeType = "application/javascript"; break;
                    case ResourceType.JPEG: mimeType = "image/jpeg"; break;
                    case ResourceType.JSON: mimeType = "application/json"; break;
                    case ResourceType.Text: mimeType = "text/plain"; break;
                    case ResourceType.PNG: mimeType = "image/png"; break;
                    case ResourceType.TTF: mimeType = "application/x-font-ttf"; break;
                    case ResourceType.WOFF: mimeType = "application/x-font-woff"; break;
                    default: mimeType = "application/octet-stream"; break;
                }

                return mimeType;
            }
        }

        internal Byte[] data;

        public Byte[] Data
        {
            get 
            {
                if (beginHandler != null)
                    return null;

                if (groupSources != null)
                {
                    ResourceCheck();

                    if (data == null)
                    {
                        int sdatal = 0;
                        List<byte[]> sdatas = new List<byte[]>();


                        foreach (KeyValuePair<int, ResourceGroupEntry> kvp in groupSources)
                        {
                            ResourceGroupEntry gentry = kvp.Value;

                            Resource source = gentry.Resource;

                            byte[] sdata = source.Data;
                            int sdatacl = sdata.Length;
                            sdatal += sdatacl;
                            sdatas.Add(sdata);

                            // Resource type = javascript fix
                            if (ResourceType == ResourceType.JavaScript)
                            {
                                // Prevent "(intermediate value)() is not function"
                                if (sdatacl > 0)
                                {
                                    if (Buffer.GetByte(sdata, sdatacl - 1) == (byte)41) // if last byte is ')'
                                    {
                                        sdatal += 1;
                                        sdatas.Add(new byte[] { 59 }); // add ';'
                                    }
                                }

                                sdatal += 2;
                                sdatas.Add(new byte[] { 13, 10 });
                            }
                        }

                        if (sdatal > 0)
                        {
                            byte[] cdata = new byte[sdatal];
                            int offset = 0;
                            foreach (byte[] sdatai in sdatas)
                            {
                                Buffer.BlockCopy(sdatai, 0, cdata, offset, sdatai.Length);
                                offset += sdatai.Length;
                            }
                            data = cdata;
                        }
                    }

                    return data;
                }
                else
                {
                    if (!string.IsNullOrEmpty(this.OriginalFilePath))
                    {
                        FileInfo info = new FileInfo(this.OriginalFilePath);

                        if (info.LastWriteTime > this.LastModified)
                        {
                            // newer file
                            this.LastModified = info.LastWriteTime;
                            this.SetData(File.ReadAllBytes(this.OriginalFilePath));
                        }
                    }

                    if (data == null)
                    {
                        if (Minify)
                        {
                            if (resourceType == ResourceType.JavaScript)
                            {
                                var minifier = new Microsoft.Ajax.Utilities.Minifier();
                                data = Encoding.UTF8.GetBytes(minifier.MinifyJavaScript(Encoding.UTF8.GetString(originalData)));
                            }
                            else if (resourceType == ResourceType.CSS)
                            {
                                var minifier = new Microsoft.Ajax.Utilities.Minifier();
                                data = Encoding.UTF8.GetBytes(minifier.MinifyStyleSheet(Encoding.UTF8.GetString(originalData)));
                            }
                            else
                                data = originalData;
                        }
                        else data = originalData;

                        if (groupResource != null)
                        {

                            foreach (KeyValuePair<int, ResourceGroupEntry> kvp in groupResource.groupSources)
                            {
                                ResourceGroupEntry gentry = kvp.Value;
                                Resource r = gentry.Resource;

                                if (r == this)
                                {
                                    gentry.Signature = this.MD5;
                                    break;
                                }
                            }

                            groupResource.data = null;
                        }
                    }
                    return data;
                }
            }
        }

        private Byte[] originalData;

        public Byte[] OriginalData
        {
            get { return originalData; }
        }
        
        private string md5;

        public string MD5
        {
            get { return md5; }
        }

        private ResourceBeginProcessRequest beginHandler = null;

        internal ResourceBeginProcessRequest BeginHandler
        {
            get { return beginHandler; }
        }

        private ResourceEndProcessRequest endHandler = null;

        internal ResourceEndProcessRequest EndHandler
        {
            get { return endHandler; }
        }
        
        private bool compressed = true;

        public bool Compressed
        {
            get { return compressed; }
            set 
            { 
                compressed = value;
                if (compressed == true) bufferOutput = true;
            }
        }

        private bool cache = true;

        public bool Cache
        {
            get { return cache; }
            set { cache = value; }
        }

        private bool minify = true;

        public bool Minify
        {
            get { return minify; }
            set 
            { 
                minify = value;
                data = null;
                if (minify == true) bufferOutput = true;
            }
        }

        private bool bufferOutput = true;

        public bool BufferOutput
        {
            get { return bufferOutput; }
            set 
            { 
                bufferOutput = value;
                if (bufferOutput == false)
                {
                    compressed = false;
                    minify = false;
                }
            }
        }

        private bool realName = false;

        public bool RealName
        {
            get { return realName; }
            set { realName = value; }
        }

        private string accessControlAllowOrigin = null;

        public string AccessControlAllowOrigin
        {
            get { return accessControlAllowOrigin; }
            set { accessControlAllowOrigin = value; }
        }

        private bool accessControlAllowCredentials = false;

        public bool AccessControlAllowCredentials
        {
            get { return accessControlAllowCredentials; }
            set { accessControlAllowCredentials = value; }
        }

        private string originalFilePath;

        internal string OriginalFilePath
        {
            get { return originalFilePath; }
            set { originalFilePath = value; }
        }

        private DateTime lastModified = DateTime.MinValue;

        internal DateTime LastModified
        {
            get { return lastModified; }
            set { lastModified = value; }
        }

        internal SortedList<int, ResourceGroupEntry> groupSources = null;

        //internal List<Resource> groupSources = null;

        //internal Dictionary<int, Resource> groupSourcesIndexer = null;

        internal Resource groupResource = null;

        //internal List<string> groupUpdateSignature = null;

        #endregion

        #region Constructors

        public Resource(string key, string keyHash, ResourceType resourceType)
        {
            this.key = key;
            this.resourceType = resourceType;

            if (keyHash == null)
                this.keyHash = Hasher.Basic(key).Substring(0, 5);
            else
                this.keyHash = keyHash;
        }

        #endregion

        #region Method

        public void SetData(Byte[] data)
        {
            // trim BOM
            byte[] utf16BE = new byte[] { 254, 255 };
            byte[] utf16LE = new byte[] { 255, 254 };
            byte[] utf8 = new byte[] { 239, 187, 191 };

            byte[] endData = null;
            if (data.Length >= 3)
            {
                if (data[0] == utf8[0] && data[1] == utf8[1] && data[2] == utf8[2])
                {
                    endData = new byte[data.Length - 3];
                    Buffer.BlockCopy(data, 3, endData, 0, data.Length - 3);
                }
            }
            if (data.Length >= 2)
            {
                if ((data[0] == utf16BE[0] && data[1] == utf16BE[1]) ||
                    (data[0] == utf16LE[0] && data[1] == utf16LE[1]))
                {
                    endData = new byte[data.Length - 2];
                    Buffer.BlockCopy(data, 2, endData, 0, data.Length - 2);
                }
            }
            if (endData == null)
                endData = data;

            this.originalData = endData;
            this.md5 = Hasher.MD5(endData);
            this.data = null;
        }

        public void SetData(string data)
        {
            SetData(System.Text.Encoding.UTF8.GetBytes(data));
        }

        public void SetHandler(ResourceBeginProcessRequest beginHandler, ResourceEndProcessRequest endHandler)
        {
            this.beginHandler = beginHandler;
            this.endHandler = endHandler;

            this.originalData = null;
            this.data = null;
            this.md5 = null;
        }

        internal void ResourceCheck()
        {    
            if (groupSources != null)
            {
                // check all sources for modification
                foreach (KeyValuePair<int, ResourceGroupEntry> kvp in groupSources)
                {
                    ResourceGroupEntry gentry = kvp.Value;
                    Resource csource = gentry.Resource;
                    byte[] csdata = csource.Data; // this will trigger data modification
                }
            }
            else
            {
                byte[] idata = Data;
            }
        }

        public Resource NoCompressed()
        {
            this.Compressed = false;
            return this;
        }
        public Resource NoCache()
        {
            this.Cache = false;
            return this;
        }
        public Resource NoMinify()
        {
            this.Minify = false;
            return this;
        }

        public Resource NoBufferOutput()
        {
            this.BufferOutput = false;
            return this;
        }

        public Resource AllowOrigin(string corsString)
        {
            this.AccessControlAllowOrigin = corsString;
            return this;
        }

        public Resource AllowCredentials()
        {
            this.AccessControlAllowCredentials = true;
            return this;
        }

        public Resource SetFileExtension(string fileExtension)
        {
            this.FileExtension = fileExtension;
            return this;
        }

        public string GetString()
        {
            if (beginHandler == null)
            {
                return System.Text.Encoding.UTF8.GetString(Data);
            }
            else return null;
        }

        #endregion

        #endregion
    }

    internal class ResourceGroupEntry
    {
        #region Fields

        private Resource resource;

        public Resource Resource
        {
            get { return resource; }
            set { resource = value; }
        }

        private string signature;

        public string Signature
        {
            get { return signature; }
            set { signature = value; }
        }

        #endregion

        #region Constructors

        public ResourceGroupEntry(Resource resource, string signature)
        {
            this.resource = resource;
            this.signature = signature;
        }

        #endregion
    }

    public class ResourceOutput
    {
        #region Fields

        private Byte[] data;

        internal Byte[] Data
        {
            get { return data; }
        }

        private string md5;

        public string MD5
        {
            get { return md5; }
        }

        #endregion

        #region Constructor

        public ResourceOutput()
        {
        }

        #endregion

        #region Methods

        public Byte[] GetData(Resource resource)
        {
            byte[] outputData;

            if (resource.Minify)
            {
                if (resource.ResourceType == ResourceType.JavaScript)
                {
                    var minifier = new Microsoft.Ajax.Utilities.Minifier();
                    outputData = Encoding.UTF8.GetBytes(minifier.MinifyJavaScript(Encoding.UTF8.GetString(data)));
                }
                else if (resource.ResourceType == ResourceType.CSS)
                {
                    var minifier = new Microsoft.Ajax.Utilities.Minifier();
                    outputData = Encoding.UTF8.GetBytes(minifier.MinifyStyleSheet(Encoding.UTF8.GetString(data)));
                }
                else
                    outputData = data;
            }
            else
                outputData = data;

            //ShareService.Service.Event("output data : " + outputData.Length);

            return outputData;
        }

        public void Clear()
        {
            this.data = null;
            this.md5 = null;
        }

        public void Write(string data)
        {
            Write(System.Text.Encoding.UTF8.GetBytes(data));
        }

        public void Write(Byte[] input)
        {
            if (this.data == null)
            {
                this.data = input;
            }
            else
            {
                Byte[] combined = new Byte[this.data.Length + input.Length];

                System.Buffer.BlockCopy(this.data, 0, combined, 0, this.data.Length);
                System.Buffer.BlockCopy(input, 0, combined, this.data.Length, input.Length);

                this.data = combined;
            }
            this.md5 = Hasher.MD5(this.data);
        }

        #endregion
    }

    public delegate void ResourceBeginProcessRequest(ResourceAsyncResult result);

    public delegate void ResourceEndProcessRequest(ResourceAsyncResult result);

    public enum ResourceType
    {
        JavaScript,
        JSON,
        Text,
        CSS,
        HTML,
        JPEG,
        PNG,
        TTF,
        WOFF
    }

    public class ResourceAsyncResult : AsyncResult
    {
        #region Fields

        private ResourceOutput resourceOutput;

        public ResourceOutput ResourceOutput
        {
            get { return resourceOutput; }
            set { resourceOutput = value; }
        }

        private Resource resource;

        public Resource Resource
        {
            get { return resource; }
            set { resource = value; }
        }

        #endregion

        #region Constructors

        public ResourceAsyncResult(HttpContext context, AsyncCallback callback, object asyncState) : base(context, callback, asyncState)
        {
        }

        #endregion
    }
}