using Microsoft.AspNetCore.Html;

namespace ObjToForm.Interfaces
{
    internal interface IObjectConvertService
    {
        IHtmlContent ConvertToForm(Type obj, string action = "", string method = "post");
    }
}
