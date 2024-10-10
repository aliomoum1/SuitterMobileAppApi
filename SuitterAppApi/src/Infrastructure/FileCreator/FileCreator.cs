using A2Z.WebApi.Infrastructure.Creator;
using SuitterAppApi.Application.Common.Interfaces;
using SuitterAppApi.Infrastructure.Persistence.Context;
using SuitterAppApi.Infrastructure.Persistence.Initialization;
using Microsoft.Extensions.Logging;
using System.Globalization;
using System.Reflection;

namespace SuitterAppApi.Infrastructure.FileCreator;

public class FileCreator : ICreator
{
    private const string ApplicationDir = "../../src/Core/Application/";
    private const string HostDir = "../../src/Host/";

    private readonly ISerializerService _serializerService;
    private readonly ApplicationDbContext _db;
    private readonly ILogger<FileCreator> _logger;
    static List<VirtualProp> virtualProp = new List<VirtualProp>();
    static List<VirtualProp> NormaProp = new List<VirtualProp>();
    static List<VirtualProp> All = new List<VirtualProp>();

    public FileCreator(ISerializerService serializerService, ILogger<FileCreator> logger, ApplicationDbContext db)
    {
        _serializerService = serializerService;
        _logger = logger;
        _db = db;
    }

    public async Task InitializeAsync(CancellationToken cancellationToken)
    {

        _logger.LogInformation("Started to creating file");
        var model = _db.Model.GetEntityTypes().Select(t => t.ClrType).ToList();
        foreach (var item in model)
        {
            var NameSpace = item.Namespace;
            var FatherNameSpace = item.Namespace.Substring(item.Namespace.LastIndexOf(".") + 1);
            string Folder = "";
            var EntityName = item.Name;
            if (EntityName == "AppVersion")
            {
                Console.WriteLine("For " + EntityName);
                if (Console.ReadLine() != "yes")
                {
                    break;
                }

                foreach (var prop in item.GetProperties())
                {


                    bool isNul = false;
                    if (prop.GetGetMethod().IsVirtual & !prop.GetGetMethod().IsAbstract)
                    {

                        GetPropparites(prop, ref isNul, "toVirtual");

                    }
                    if (!prop.GetGetMethod().IsVirtual & !prop.GetGetMethod().IsAbstract)
                    {
                        GetPropparites(prop, ref isNul, "toNormal");

                    }
                    if (!prop.GetGetMethod().IsAbstract)
                    {
                        GetPropparites(prop, ref isNul, "all");
                    }

                }
                var changes = new List<ListOfChange>();

                string ctor = string.Empty;
                string propS = string.Empty;
                string allprop = string.Empty;
                string replaceImage = string.Empty;
                foreach (var propNa in NormaProp)
                {

                    if (propNa.Name.Contains("Image"))
                    {
                        propS += "public FileUploadRequest? " + propNa.Name + " { get; set; }\n";
                        replaceImage += "string " + propNa.Name + "Path" + "= await _file.UploadAsync<" + EntityName + ">(request." + propNa.Name + ", FileType.Image, cancellationToken);\n";

                        ctor += propNa.Name + "Path , ";
                        continue;
                    }
                    ctor += "request." + propNa.Name + ", ";
                    ctor.Remove(ctor.LastIndexOf(",") - 2);

                    if (propNa.IsNulable)
                    {
                        propS += "\tpublic " + propNa.type.ToLower() + "? " + propNa.Name + " { get; set; }\n";

                    }
                    else
                    {
                        string type = (propNa.type.ToLower() == "boolean") ? "bool" : propNa.type.ToLower();
                        propS += "\tpublic " + type + " " + propNa.Name + " { get; set; }\n";

                    }

                }
                string DapperConnectBuilder = string.Empty;

                foreach (var propNa in All)
                {
                    if (propNa.Name == "DomainEvents")
                    {
                        continue;

                    }


                    DapperConnectBuilder += propNa.Name + " = " + EntityName[0].ToString().ToLower() + EntityName.Remove(0, 1) + "." + propNa.Name + ",\n";
                    if (propNa.Name == "Id")
                    {
                        continue;
                    }

                    if (propNa.Name.Contains("Id"))
                    {
                        propNa.Name = propNa.Name.Replace("Id", "Name");
                    }
                    if (propNa.Name.Contains("Image"))
                    {
                        if (propNa.IsNulable)
                        {
                            allprop += "public string? " + propNa.Name + " { get; set; }\n";

                        }
                        allprop += "public string? " + propNa.Name + " { get; set; }\n";

                        continue;
                    }

                    if (propNa.IsNulable)
                    {
                        string type = (propNa.type.ToLower() == "guid") ? "Guid" : propNa.type.ToLower();
                        if (type == "datetime")
                        {
                            type = "DateTime";
                            allprop += "\tpublic " + type + "? " + propNa.Name + " { get; set; }\n";

                        }
                        else
                        {
                            allprop += "\tpublic " + type + "? " + propNa.Name + " { get; set; }\n";

                        }

                    }
                    else
                    {

                        string type = (propNa.type.ToLower() == "boolean") ? "bool" : propNa.type.ToLower();

                        if (type == "bool")
                        {
                            allprop += "\tpublic " + type + " " + propNa.Name + " { get; set; }\n";

                        }
                        else
                        {
                            type = (propNa.type.ToLower() == "guid") ? "Guid" : propNa.type.ToLower();
                            if (propNa.type.ToLower() == "datetime")
                            {
                                type = "DateTime";
                            }
                            allprop += "\tpublic " + type + " " + propNa.Name + " { get; set; } = default!;\n";

                        }


                    }

                }
                changes.Add(new ListOfChange() { Name = "replaceImage", Text = replaceImage });
                changes.Add(new ListOfChange() { Name = "ctor", Text = ctor });
                changes.Add(new ListOfChange() { Name = "prop", Text = propS });
                changes.Add(new ListOfChange() { Name = "Folder", Text = FatherNameSpace });
                changes.Add(new ListOfChange() { Name = "AllProp", Text = allprop });

                CreateFile("SuitterAppApi.Infrastructure.FileCreator.ApplicationTemplate.CreateNameRequest.cs", ApplicationDir + FatherNameSpace + "/" + EntityName + "s/", "CreateNameRequest.cs".Replace("Name", EntityName), EntityName, changes);


                changes = new List<ListOfChange>();

                changes.Add(new ListOfChange() { Name = "Folder", Text = FatherNameSpace });
                CreateFile("SuitterAppApi.Infrastructure.FileCreator.ApplicationTemplate.CreateNameRequestValidator.cs", ApplicationDir + FatherNameSpace + "/" + EntityName + "s/", "CreateNameRequestValidator.cs".Replace("Name", EntityName), EntityName, changes);
                changes = new List<ListOfChange>();

                changes.Add(new ListOfChange() { Name = "Folder", Text = FatherNameSpace });
                CreateFile("SuitterAppApi.Infrastructure.FileCreator.ApplicationTemplate.DeleteNameRequest.cs", ApplicationDir + FatherNameSpace + "/" + EntityName + "s/", "DeleteNameRequest.cs".Replace("Name", EntityName), EntityName, changes);
                changes = new List<ListOfChange>();

                changes.Add(new ListOfChange() { Name = "Folder", Text = FatherNameSpace });
                CreateFile("SuitterAppApi.Infrastructure.FileCreator.ApplicationTemplate.ExportNamesRequest.cs", ApplicationDir + FatherNameSpace + "/" + EntityName + "s/", "ExportNamesRequest.cs".Replace("Name", EntityName), EntityName, changes);
                changes = new List<ListOfChange>();

                changes.Add(new ListOfChange() { Name = "prop", Text = allprop });
                changes.Add(new ListOfChange() { Name = "Folder", Text = FatherNameSpace });
                CreateFile("SuitterAppApi.Infrastructure.FileCreator.ApplicationTemplate.NameExportDto.cs", ApplicationDir + FatherNameSpace + "/" + EntityName + "s/", "NameExportDto.cs".Replace("Name", EntityName), EntityName, changes);
                changes = new List<ListOfChange>();


                changes.Add(new ListOfChange() { Name = "propall", Text = allprop });
                changes.Add(new ListOfChange() { Name = "Folder", Text = FatherNameSpace });
                CreateFile("SuitterAppApi.Infrastructure.FileCreator.ApplicationTemplate.NameDetailsDto.cs", ApplicationDir + FatherNameSpace + "/" + EntityName + "s/", "NameDetailsDto.cs".Replace("Name", EntityName), EntityName, changes);
                changes = new List<ListOfChange>();


                changes.Add(new ListOfChange() { Name = "Folder", Text = FatherNameSpace });
                CreateFile("SuitterAppApi.Infrastructure.FileCreator.ApplicationTemplate.NameByIdSpec.cs", ApplicationDir + FatherNameSpace + "/" + EntityName + "s/", "NameByIdSpec.cs".Replace("Name", EntityName), EntityName, changes);
                changes = new List<ListOfChange>();



                changes.Add(new ListOfChange() { Name = "Folder", Text = FatherNameSpace });
                CreateFile("SuitterAppApi.Infrastructure.FileCreator.ApplicationTemplate.GetNameRequest.cs", ApplicationDir + FatherNameSpace + "/" + EntityName + "s/", "GetNameRequest.cs".Replace("Name", EntityName), EntityName, changes);
                changes = new List<ListOfChange>();


                changes.Add(new ListOfChange() { Name = "prop", Text = allprop + "public Guid Id { get; set; }" });
                changes.Add(new ListOfChange() { Name = "Folder", Text = FatherNameSpace });
                CreateFile("SuitterAppApi.Infrastructure.FileCreator.ApplicationTemplate.NameDto.cs", ApplicationDir + FatherNameSpace + "/" + EntityName + "s/", "NameDto.cs".Replace("Name", EntityName), EntityName, changes);
                changes = new List<ListOfChange>();

                changes.Add(new ListOfChange() { Name = "Folder", Text = FatherNameSpace });

                changes.Add(new ListOfChange() { Name = "DapperChange", Text = DapperConnectBuilder });
                CreateFile("SuitterAppApi.Infrastructure.FileCreator.ApplicationTemplate.GetNameViaDapperRequest.cs", ApplicationDir + FatherNameSpace + "/" + EntityName + "s/", "GetNameViaDapperRequest.cs".Replace("Name", EntityName), EntityName, changes);
                changes = new List<ListOfChange>();
                string ImageBuilder = string.Empty;
                string ImageRemoveAndAdder = string.Empty;
                foreach (var image in All.Where(x => x.Name.Contains("Image")))
                {
                    ImageRemoveAndAdder += @"if (request.Delete" + image.Name + @")
                                                {
                                                    string? current" + image.Name + @"Path = " + EntityName[0].ToString().ToLower() + EntityName.Remove(0, 1) + @"." + image.Name + @";
                                                    if (!string.IsNullOrEmpty(current" + image.Name + @"Path ))
                                                    {
                                                        string root = Directory.GetCurrentDirectory();
                                                        _file.Remove(Path.Combine(root, current" + image.Name + @"Path));
                                                    }
                                                    " + EntityName[0].ToString().ToLower() + EntityName.Remove(0, 1) + @"=" + EntityName[0].ToString().ToLower() + EntityName.Remove(0, 1) + @".Clear" + image.Name + @"();
                                                }
                                                string? " + image.Name + @"Path = request." + image.Name + @" is not null
                                                    ? await _file.UploadAsync<" + EntityName + @">( request." + image.Name + @", FileType.Image, cancellationToken)
                                                    : null;";
                    ImageBuilder += "public bool Delete" + image.Name + " { get; set; } = false;\n";

                }
                changes.Add(new ListOfChange() { Name = "imChange", Text = ImageRemoveAndAdder });
                changes.Add(new ListOfChange() { Name = "zazazaz", Text = ctor });
                changes.Add(new ListOfChange() { Name = "prop", Text = propS + "public Guid Id { get; set; }" + ImageBuilder });
                changes.Add(new ListOfChange() { Name = "Folder", Text = FatherNameSpace });
                CreateFile("SuitterAppApi.Infrastructure.FileCreator.ApplicationTemplate.UpdateNameRequest.cs", ApplicationDir + FatherNameSpace + "/" + EntityName + "s/", "UpdateNameRequest.cs".Replace("Name", EntityName), EntityName, changes);
                changes = new List<ListOfChange>();


                changes.Add(new ListOfChange() { Name = "Folder", Text = FatherNameSpace });
                CreateFile("SuitterAppApi.Infrastructure.FileCreator.ApplicationTemplate.UpdateNameRequestValidator.cs", ApplicationDir + FatherNameSpace + "/" + EntityName + "s/", "UpdateNameRequestValidator.cs".Replace("Name", EntityName), EntityName, changes);
                changes = new List<ListOfChange>();




                changes.Add(new ListOfChange() { Name = "prop", Text = propS });
                changes.Add(new ListOfChange() { Name = "Folder", Text = FatherNameSpace });
                CreateFile("SuitterAppApi.Infrastructure.FileCreator.ApplicationTemplate.NamesBySearchRequestSpec.cs", ApplicationDir + FatherNameSpace + "/" + EntityName + "s/", "NamesBySearchRequestSpec.cs".Replace("Name", EntityName), EntityName, changes);
                changes = new List<ListOfChange>();




                changes.Add(new ListOfChange() { Name = "prop", Text = propS });
                changes.Add(new ListOfChange() { Name = "Folder", Text = FatherNameSpace });
                CreateFile("SuitterAppApi.Infrastructure.FileCreator.ApplicationTemplate.SearchNamesRequest.cs", ApplicationDir + FatherNameSpace + "/" + EntityName + "s/", "SearchNamesRequest.cs".Replace("Name", EntityName), EntityName, changes);
                changes = new List<ListOfChange>();

                string restaBuilder = string.Empty;
                string retaBuilder = string.Empty;
                restaBuilder += "\r\n\r\n        new(\"View " + EntityName + "s\", FSHAction.View, FSHResource." + EntityName + "s, IsBasic: true),\r\n        new(\"Search " + EntityName + "s\", FSHAction.Search, FSHResource." + EntityName + "s, IsBasic: true),\r\n        new(\"Create " + EntityName + "s\", FSHAction.Create, FSHResource." + EntityName + "s),\r\n        new(\"Update " + EntityName + "s\", FSHAction.Update, FSHResource." + EntityName + "s),\r\n        new(\"Delete " + EntityName + "s\", FSHAction.Delete, FSHResource." + EntityName + "s),\r\n        new(\"Export " + EntityName + "s\", FSHAction.Export, FSHResource." + EntityName + "s),\n\t/*resta*/";
                retaBuilder += "public const string " + EntityName + "s = nameof(" + EntityName + "s);\n\t/*reta*/";

                string template = string.Empty;
                StreamReader sr = new StreamReader("../../src/Core/Shared/Authorization/FSHPermissions.cs");
                template = sr.ReadToEnd();
                sr.Close();
                template = template.Replace("/*reta*/", retaBuilder);
                template = template.Replace("/*resta*/", restaBuilder);



                using (TextWriter writer = File.CreateText("../../src/Core/Shared/Authorization/FSHPermissions.cs"))
                {
                    writer.WriteLine(template);
                }

                changes.Add(new ListOfChange() { Name = "prop", Text = propS });
                changes.Add(new ListOfChange() { Name = "Folder", Text = FatherNameSpace });
                CreateFile("SuitterAppApi.Infrastructure.FileCreator.ApplicationTemplate.NamesController.cs", HostDir + "Controllers/" + FatherNameSpace + "/", "NamesController.cs".Replace("Name", EntityName), EntityName, changes);
                changes = new List<ListOfChange>();


                virtualProp = new List<VirtualProp>();
                NormaProp = new List<VirtualProp>();
                All = new List<VirtualProp>();
            }

        }
    }
    private static void GetPropparites(PropertyInfo prop, ref bool isNul, string to)
    {
        string name = prop.Name.ToString().Split(" ")[0];
        string propType = prop.PropertyType.ToString().Split(".")[1];
        var typeff = prop.PropertyType;

        var ds = prop.PropertyType;
        //if (propType != "DateTime")
        //{
        //    propType = name.ToString().Split(".")[1].ToLower();
        //}

        if (ds.ToString().Contains("Nullable"))
        {

            propType = ds.ToString().Split("System")[2].Replace(".", string.Empty).Replace("[", string.Empty).Replace("]", string.Empty).Normalize().ToLower().TrimEnd(new[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' });

            isNul = true;

        }
        switch (to)
        {
            case "toVirtual":
                virtualProp.Add(new VirtualProp() { Name = prop.Name, NameSpace = propType, IsNulable = isNul, type = propType });

                break;
            case "toNormal":
                NormaProp.Add(new VirtualProp() { Name = prop.Name, NameSpace = propType, IsNulable = isNul, type = propType });
                break;
            case "all":
                All.Add(new VirtualProp() { Name = prop.Name, NameSpace = propType, IsNulable = isNul, type = propType });
                break;

            default:
                break;
        }
    }

    public static void CreateFile(string TemplatePath, string outputFilePath, string filename, string EntityName, List<ListOfChange> ListOfChage)
    {
        try
        {
            var nams = Assembly.GetExecutingAssembly().GetManifestResourceNames();

            var assembly = Assembly.GetExecutingAssembly();
            string template = string.Empty;
            // Read resource from assembly

            using (var stream = assembly.GetManifestResourceStream(TemplatePath))
            using (var reader = new StreamReader(stream))
            {
                template = reader.ReadToEnd();
                template = template.Replace("Change22", EntityName, false, CultureInfo.InvariantCulture);
                template = template.Replace("change22", char.ToLower(EntityName[0]) + EntityName.Substring(1), false, CultureInfo.InvariantCulture);
                foreach (var item in ListOfChage)
                {
                    template = template.Replace(item.Name, item.Text, false, CultureInfo.InvariantCulture);
                }
            }

            // Write data in the new text file
            var filePath = outputFilePath;
            DirectoryInfo di = Directory.CreateDirectory(filePath);

            using (TextWriter writer = File.CreateText(filePath + filename))
            {
                writer.WriteLine(template);
            }

        }
        catch (Exception)
        {

            throw;
        }

    }

}

public class ListOfChange
{
    public string Name { get; set; }
    public string Text { get; set; }

}