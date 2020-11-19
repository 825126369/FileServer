using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Certificate;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;

namespace WebServer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            //services.AddAuthentication(
            //CertificateAuthenticationDefaults.AuthenticationScheme)
            //.AddCertificate().AddCertificateCache();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            //app.UseStaticFiles();

            //app.UseFileServer(new FileServerOptions()
            //{
            //    FileProvider = new PhysicalFileProvider(
            //                Path.Combine(Directory.GetCurrentDirectory(), @"StaticFiles")),
            //    RequestPath = new PathString("/StaticFiles"),
            //    EnableDirectoryBrowsing = true
            //});
            
            var dir = new DirectoryBrowserOptions();
            dir.FileProvider = new PhysicalFileProvider(@"/Users/xuke2020/Documents/Project/FileServer/WebServer/StaticFiles/");
            app.UseDirectoryBrowser(dir);

            //更改默认文件夹 (StaticFileOptions方法)
            var staticfile = new StaticFileOptions();
            staticfile.FileProvider = new PhysicalFileProvider(@"/Users/xuke2020/Documents/Project/FileServer/WebServer/StaticFiles/");//指定目录，这里指C盘，也可以是其他目录
            app.UseStaticFiles(staticfile);//使用默认文件夹wwwroot //手动设置MIME Type,或者设置一个默认值， 以解决某些文件MIME Type文件识别不到，出现404错误
            staticfile.ServeUnknownFileTypes = true;
            
            //staticfile.DefaultContentType = "application/octet-stream";
            //var provider = new FileExtensionContentTypeProvider();//使用一组默认映射创建新的提供程序
            //provider.Mappings.Add(".log", "text/plain");//手动设置对应MIME Type
            //staticfile.ContentTypeProvider = provider; //将文件映射到内容类型

            app.UseRouting();

            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
