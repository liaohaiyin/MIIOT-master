using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Services.Description;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.CodeDom;


namespace MIIOT.Trical
{
    public class WebServiceHelper
    {

        public static object InvokeWebService(string url, string classname, string methodname, object[] args)
        {
            string nameSpace = "client";
            System.Net.WebClient client = new System.Net.WebClient();

            //String url = System.Configuration.ConfigurationManager.ConnectionStrings["serviceAddress"].ConnectionString;//这个地址可以写在Config文件里面，这里取出来就行了.在原地址后面加上： ?WSDL   
            System.IO.Stream stream = client.OpenRead(url + "?WSDL");
            System.Web.Services.Description.ServiceDescription description = System.Web.Services.Description.ServiceDescription.Read(stream);

            System.Web.Services.Description.ServiceDescriptionImporter importer = new System.Web.Services.Description.ServiceDescriptionImporter();//创建客户端代理代理类。   

            importer.ProtocolName = "Soap"; //指定访问协议。   
            importer.Style = System.Web.Services.Description.ServiceDescriptionImportStyle.Client; //生成客户端代理。   
            importer.CodeGenerationOptions = System.Xml.Serialization.CodeGenerationOptions.GenerateProperties | System.Xml.Serialization.CodeGenerationOptions.GenerateNewAsync;

            importer.AddServiceDescription(description, null, null); //添加WSDL文档。   

            System.CodeDom.CodeNamespace nmspace = new System.CodeDom.CodeNamespace(); //命名空间   
            nmspace.Name = nameSpace;
            System.CodeDom.CodeCompileUnit unit = new System.CodeDom.CodeCompileUnit();
            unit.Namespaces.Add(nmspace);

            System.Web.Services.Description.ServiceDescriptionImportWarnings warning = importer.Import(nmspace, unit);
            System.CodeDom.Compiler.CodeDomProvider provider = System.CodeDom.Compiler.CodeDomProvider.CreateProvider("CSharp");

            System.CodeDom.Compiler.CompilerParameters parameter = new System.CodeDom.Compiler.CompilerParameters();
            parameter.GenerateExecutable = false;
            parameter.OutputAssembly = "TempClass.dll";//输出程序集的名称   
            parameter.ReferencedAssemblies.Add("System.dll");
            parameter.ReferencedAssemblies.Add("System.XML.dll");
            parameter.ReferencedAssemblies.Add("System.Web.Services.dll");
            parameter.ReferencedAssemblies.Add("System.Data.dll");

            System.CodeDom.Compiler.CompilerResults result = provider.CompileAssemblyFromDom(parameter, unit);
            if (result.Errors.HasErrors)
            {
                // 显示编译错误信息   
            }

            System.Reflection.Assembly asm = System.Reflection.Assembly.LoadFrom("TempClass.dll");//加载前面生成的程序集   
            Type t = asm.GetType(nameSpace + "." + classname);

            object o = Activator.CreateInstance(t);
            System.Reflection.MethodInfo method = t.GetMethod(methodname);//GetPersons是服务端的方法名称,你想调用服务端的什么方法都可以在这里改,最好封装一下   
            object item = method.Invoke(o, args);
            //注：method.Invoke(o, null)返回的是一个Object,如果你服务端返回的是DataSet,这里也是用(DataSet)method.Invoke(o, null)转一下就行了   
            return item;




            //string @namespace = "EbeitXmlWebService";
            //if (classname == null || classname == "")
            //{
            //    classname = WebServiceHelper.GetClassName(url);
            //}
            ////获取服务描述语言(WSDL)  
            //WebClient wc = new WebClient();
            //Stream stream = wc.OpenRead(url + "?WSDL");
            //ServiceDescription sd = ServiceDescription.Read(stream);
            //ServiceDescriptionImporter sdi = new ServiceDescriptionImporter();
            //sdi.AddServiceDescription(sd, "", "");
            //CodeNamespace cn = new CodeNamespace(@namespace);
            ////生成客户端代理类代码  
            //CodeCompileUnit ccu = new CodeCompileUnit();
            //ccu.Namespaces.Add(cn);
            //sdi.Import(cn, ccu);
            //CSharpCodeProvider csc = new CSharpCodeProvider();
            //ICodeCompiler icc = csc.CreateCompiler();
            ////设定编译器的参数  
            //CompilerParameters cplist = new CompilerParameters();
            //cplist.GenerateExecutable = false;
            //cplist.GenerateInMemory = true;
            //cplist.ReferencedAssemblies.Add("System.dll");
            //cplist.ReferencedAssemblies.Add("System.XML.dll");
            //cplist.ReferencedAssemblies.Add("System.Web.Services.dll");
            //cplist.ReferencedAssemblies.Add("System.Data.dll");
            ////编译代理类  
            //CompilerResults cr = icc.CompileAssemblyFromDom(cplist, ccu);
            //if (true == cr.Errors.HasErrors)
            //{
            //    System.Text.StringBuilder sb = new StringBuilder();
            //    foreach (CompilerError ce in cr.Errors)
            //    {
            //        sb.Append(ce.ToString());
            //        sb.Append(System.Environment.NewLine);
            //    }
            //    throw new Exception(sb.ToString());
            //}
            ////生成代理实例,并调用方法  
            //System.Reflection.Assembly assembly = cr.CompiledAssembly;
            //Type t = assembly.GetType(@namespace + "." + classname, true, true);
            //object obj = Activator.CreateInstance(t);
            //System.Reflection.MethodInfo mi = t.GetMethod(methodname);
            //return mi.Invoke(obj, args);
        }

        private static string GetClassName(string url)
        {
            string[] parts = url.Split('/');
            string[] pps = parts[parts.Length - 1].Split('.');
            return pps[0];
        }
    }
}
